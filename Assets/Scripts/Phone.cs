using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Phone : MonoBehaviour {
    public UnityEvent timeoutEvent = new UnityEvent();

    [SerializeField] GameData gameData = null;
    [SerializeField] RectTransform timerFill = null;
    [SerializeField] float timeLimit = 10;

    Countdown countdown;
    bool depleting;

    public void FailTask() {
        countdown.Finish();
        depleting = false;
        gameData.FailTask();
    }

    public void CompleteTask() {
        countdown.Reset(timeLimit);
        depleting = false;
        gameData.CompleteTask();
    }

    public void StartDepleting() {
        depleting = true;
        countdown.Reset(timeLimit);
    }

    void Awake() {
        countdown = new Countdown(timeLimit);
    }

    void Update() {
        if (depleting) {
            countdown.Elapse(Time.deltaTime);
            if (countdown.IsStopped()) {
                countdown.Reset(timeLimit);
                gameData.FailTask();
                timeoutEvent.Invoke();
            }
        }
    }

    void LateUpdate() {
        timerFill.sizeDelta = new Vector2(countdown.GetProgress() * 150, 20);
    }
}
