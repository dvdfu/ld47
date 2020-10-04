using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhonePhoto : MonoBehaviour {
    [SerializeField] Phone phone = null;
    [SerializeField] RectTransform knob = null;
    [SerializeField] Image knobImage = null;

    float sliderValue;
    bool shouldSlide;

    public void OnPress() {
        if (shouldSlide) {
            shouldSlide = false;
        }
    }

    void Awake() {
        shouldSlide = true;
    }

    void Update() {
        if (shouldSlide) {
            sliderValue = Mathf.Sin(Time.time * Mathf.PI);
        }
    }

    void LateUpdate() {
        knob.anchoredPosition = Vector2.right * sliderValue * 60;
        if (IsValueCorrect()) {
            knobImage.color = Color.green;
        } else {
            knobImage.color = Color.red;
        }
    }

    bool IsValueCorrect() {
        return Mathf.Abs(sliderValue) < 0.2f;
    }
}
