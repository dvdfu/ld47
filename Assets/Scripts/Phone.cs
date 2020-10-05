using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Phone : MonoBehaviour {
    public UnityEvent timeoutEvent = new UnityEvent();

    [SerializeField] RectTransform timerFill = null;
    [SerializeField] float timeLimit = 10;

    Countdown countdown;
    bool depleting;

    public void CompleteTask() {
        countdown.Reset(timeLimit);
        depleting = false;
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
                timeoutEvent.Invoke();
            }
        }
    }

    void LateUpdate() {
        timerFill.sizeDelta = new Vector2(countdown.GetProgress() * 150, 20);
    }
}
