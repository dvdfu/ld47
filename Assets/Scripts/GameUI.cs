using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour {
    [SerializeField] GameData gameData = null;
    [SerializeField] RectTransform happinessFill = null;
    [SerializeField] Image mood = null;
    [SerializeField] Sprite[] moodSprites = null;

    void Start() {
        gameData.Reset();
    }

    void LateUpdate() {
        float hp = gameData.happiness.GetProgress();
        float width = Mathf.Lerp(happinessFill.sizeDelta.x, hp * 400, 0.1f);
        happinessFill.sizeDelta = new Vector2(width, 50);
        int i = Mathf.FloorToInt(hp * moodSprites.Length);
        i = Mathf.Clamp(i, 0, moodSprites.Length - 1);
        mood.sprite = moodSprites[i];
    }
}
