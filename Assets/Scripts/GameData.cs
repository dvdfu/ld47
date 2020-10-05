using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "GameData", menuName = "Data/GameData")]
public class GameData : ScriptableObject {
    public UnityEvent failTaskEvent = new UnityEvent();

    public Countdown happiness;
    public int score;
    public int postsLeft;
    public bool onFire;

    public void Reset() {
        happiness = new Countdown(100);
        happiness.Elapse(75);
        score = 0;
    }

    public void CompleteTask() {
        if (happiness.GetProgress() > 0.2f) {
            score++;
        }
        happiness.Elapse(-5);
    }

    public void FailTask() {
        happiness.Elapse(20);
        failTaskEvent.Invoke();
    }
}
