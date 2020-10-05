using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Phone : MonoBehaviour {
    public UnityEvent timeoutEvent = new UnityEvent();

    [SerializeField] GameData gameData = null;
    [SerializeField] RectTransform timerFill = null;
    [SerializeField] Image timerBar = null;
    [SerializeField] Text instructions = null;
    [SerializeField] ParticleSystem smoke = null;
    [SerializeField] GameObject crackedScreen = null;
    [SerializeField] float timeLimit = 10;
    [SerializeField] AudioClip igniteSound = null;
    [SerializeField] AudioClip glassSound = null;

    Countdown countdown;
    bool depleting;
    bool onFire;

    public void Burn() {
        if (!onFire) {
            onFire = true;
            StartCoroutine(BurnRoutine());
        }
    }

    public void FailTask() {
        if (onFire) {
            return;
        }
        countdown.Finish();
        depleting = false;
        StartCoroutine(ShowInstructionsRoutine());
        gameData.FailTask();
    }

    public void CompleteTask() {
        if (onFire) {
            return;
        }
        countdown.Reset(timeLimit);
        depleting = false;
        gameData.CompleteTask();
    }

    public void ResetCountdown(bool depleting = true) {
        if (onFire) {
            return;
        }
        this.depleting = depleting;
        countdown.Reset(timeLimit);
    }

    void Awake() {
        countdown = new Countdown(timeLimit);
    }

    void OnEnable() {
        StartCoroutine(ShowInstructionsRoutine());
    }

    void Update() {
        if (depleting && !onFire) {
            countdown.Elapse(Time.deltaTime);
            if (countdown.IsStopped()) {
                timeoutEvent.Invoke();
            }
        }
    }

    void LateUpdate() {
        if (countdown.IsStopped()) {
            float t = Mathf.Sin(Time.time * Mathf.PI * 4) / 2 + 0.5f;
            timerBar.color = Color.Lerp(Color.red, Color.black, t);
            if (!gameData.ending) {
                gameData.happiness.Elapse(Time.deltaTime * 2);
            }
        } else {
            timerBar.color = Color.black;
        }
        timerFill.sizeDelta = new Vector2(countdown.GetProgress() * 150, 10);
    }

    IEnumerator ShowInstructionsRoutine() {
        instructions.color = Color.white;
        yield return new WaitForSeconds(6);
        while (countdown.IsStopped() || onFire) {
            yield return null;
        }
        Color clear = new Color(1, 1, 1, 0);
        yield return Tween.StartRoutine(0.5f, (float progress) => {
            instructions.color = Color.Lerp(Color.white, clear, Easing.CubicOut(progress));
        });
    }

    IEnumerator BurnRoutine() {
        countdown.Finish();
        depleting = false;
        gameData.FailTask();
        smoke.Play();
        SoundManager.instance.Play(igniteSound);
        yield return new WaitForSeconds(5);
        smoke.Stop();
        crackedScreen.SetActive(true);
        SoundManager.instance.Play(glassSound);
    }
}
