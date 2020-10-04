using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Message : MonoBehaviour {
    [SerializeField] RectTransform bubble = null;
    [SerializeField] Text messageUI = null;
    
    public void Init(string message) {
        messageUI.text = message;
        StartCoroutine(ShowRoutine());
    }

    IEnumerator ShowRoutine() {
        float width = bubble.sizeDelta.x;
        yield return Tween.StartRoutine(0.3f, (float progress) => {
            float height = 30 * Easing.CubicIn(progress);
            bubble.sizeDelta = new Vector2(width, height);
        });
    }
}
