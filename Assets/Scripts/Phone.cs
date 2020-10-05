﻿using System.Collections;
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
    [SerializeField] float timeLimit = 10;

    Countdown countdown;
    bool depleting;

    public void FailTask() {
        countdown.Finish();
        depleting = false;
        StartCoroutine(ShowInstructionsRoutine());
        gameData.FailTask();
    }

    public void CompleteTask() {
        countdown.Reset(timeLimit);
        depleting = false;
        gameData.CompleteTask();
    }

    public void ResetCountdown(bool depleting = true) {
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
        if (depleting) {
            countdown.Elapse(Time.deltaTime);
            if (countdown.IsStopped()) {
                countdown.Reset(timeLimit);
                timeoutEvent.Invoke();
            }
        }
    }

    void LateUpdate() {
        if (countdown.IsStopped()) {
            float t = Mathf.Sin(Time.time * Mathf.PI * 4) / 2 + 0.5f;
            timerBar.color = Color.Lerp(Color.red, Color.black, t);
            gameData.happiness.Elapse(Time.deltaTime * 4);
        } else {
            timerBar.color = Color.black;
        }
        timerFill.sizeDelta = new Vector2(countdown.GetProgress() * 150, 10);
    }

    IEnumerator ShowInstructionsRoutine() {
        instructions.color = Color.white;
        yield return new WaitForSeconds(6);
        yield return Tween.StartRoutine(0.5f, (float progress) => {
            instructions.color = Color.Lerp(Color.white, Color.clear, Easing.CubicOut(progress));
        });
    }
}
