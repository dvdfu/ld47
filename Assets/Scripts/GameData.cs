using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = "Data/GameData")]
public class GameData : ScriptableObject {
    public int score;
    public char trending;

    public void LikePost(string content) {
        // int count = 0;
        // foreach (char c in content) {
        //     if (c == trending) {
        //         count++;
        //     }
        // }
        // if (count > 0) {
        if (content.IndexOf(trending) >= 0) {
            score += 1;
        } else if (score > 3) {
            score -= 3;
        } else {
            score = 0;
        }
    }

    public void Reset() {
        score = 0;
    }
}
