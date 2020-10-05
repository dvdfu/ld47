using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour {
    [SerializeField] GameData gameData = null;
    [SerializeField] RectTransform happinessFill = null;
    [SerializeField] Text happinessText = null;
    [SerializeField] Image mood = null;
    [SerializeField] Sprite moodShock = null;
    [SerializeField] Sprite[] moodSprites = null;

    Countdown shock;

    void Awake() {
        gameData.failTaskEvent.AddListener(OnFailTask);
    }

    void Start() {
        shock = new Countdown(0.5f);
        shock.Finish();
        gameData.Reset();
    }

    void OnDestroy() {
        gameData.failTaskEvent.RemoveListener(OnFailTask);
    }

    void Update() {
        shock.Elapse(Time.deltaTime);
    }

    void LateUpdate() {
        float hp = gameData.happiness.GetProgress();
        float width = Mathf.Lerp(happinessFill.sizeDelta.x, hp * 400, 0.1f);
        happinessFill.sizeDelta = new Vector2(width, 40);

        if (shock.IsStopped()) {
            int i = Mathf.FloorToInt(hp * moodSprites.Length);
            i = Mathf.Clamp(i, 0, moodSprites.Length - 1);
            mood.sprite = moodSprites[i];
        } else {
            mood.sprite = moodShock;
        }

        happinessText.text = "Happiness " + Mathf.FloorToInt(hp * 100).ToString();
    }

    void OnFailTask() {
        shock.Reset(0.5f);
    }
}
