using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour {
    [SerializeField] GameData gameData = null;
    [SerializeField] RectTransform happinessFill = null;

    void Start() {
        gameData.Reset();
    }

    void LateUpdate() {
        happinessFill.sizeDelta = new Vector2(gameData.happiness.GetProgress() * 800, 40);
    }
}
