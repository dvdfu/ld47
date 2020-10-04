using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = "Data/GameData")]
public class GameData : ScriptableObject {
    public Countdown happiness;

    public void Reset() {
        happiness = new Countdown(100);
        happiness.Elapse(50);
    }
}
