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
    public bool ending;

    public void Reset() {
        happiness = new Countdown(100);
        happiness.Elapse(75);
        score = 0;
        onFire = false;
        ending = false;
    }

    public void CompleteTask() {
        if (!happiness.IsStopped()) {
            score++;
        }
        happiness.Elapse(-1);
    }

    public void FailTask() {
        happiness.Elapse(15);
        failTaskEvent.Invoke();
    }
}
