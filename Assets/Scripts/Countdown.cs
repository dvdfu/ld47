using UnityEngine;

public class Countdown {
    float timeMax;
    float timeLeft;

    public Countdown(float timeMax) {
        Reset(timeMax);
    }

    public void Elapse(float dt) {
        timeLeft = Mathf.Clamp(timeLeft - dt, 0, timeMax);
    }

    public void Reset(float timeMax) {
        this.timeMax = timeMax;
        timeLeft = timeMax;
    }

    public void Finish() {
        timeLeft = 0;
    }

    public bool IsStopped() {
        return timeLeft == 0;
    }

    public float GetProgress() {
        return timeLeft / timeMax;
    }

    public float GetRemaining() {
        return timeLeft;
    }
}