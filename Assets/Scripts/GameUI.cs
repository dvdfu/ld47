using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour {
    [SerializeField] GameData gameData = null;
    [SerializeField] Text scoreUI = null;
    [SerializeField] Glyph trendingGlyph = null;

    float followerCount;

    void LateUpdate() {
        int realFollowers = 1 + Mathf.CeilToInt(Mathf.Pow(gameData.score, 2) / 3);
        followerCount = Mathf.Lerp(followerCount, realFollowers, 0.03f);
        scoreUI.text = Mathf.CeilToInt(followerCount).ToString() + " Followers";
        trendingGlyph.SetGlyph(gameData.trending);
    }
}
