using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhonePhoto : MonoBehaviour {
    [SerializeField] Phone phone = null;
    [SerializeField] Image photo = null;
    [SerializeField] Sprite[] photos = null;
    [SerializeField] Slider slider1 = null;
    [SerializeField] Slider slider2 = null;
    [SerializeField] Image knob1 = null;
    [SerializeField] Image knob2 = null;
    [SerializeField] Button submitButton = null;
    [SerializeField] RectTransform successScreen = null;
    [SerializeField] Color mintColor = Color.white;
    [SerializeField] AudioClip correctSound = null;
    
    float value1;
    float value2;

    int photoNumber;

    public void OnSubmit() {
        StartCoroutine(SubmitRoutine());
    }

    public void Refresh() {
        value1 = slider1.value;
        value2 = slider2.value;

        knob1.color = value1 == 1 ? Color.white : mintColor;
        knob2.color = value2 == 1 ? Color.white : mintColor;

        photo.GetComponent<RectTransform>().eulerAngles = Vector3.forward * (1 - value1) * 30;
        photo.color = Color.Lerp(Color.grey, Color.white, Easing.CubicIn(value2));

        submitButton.interactable = AreValuesCorrect();
    }

    void Start() {
        NewPhoto();
    }

    void NewPhoto() {
        photo.sprite = photos[photoNumber % photos.Length];
        photoNumber++;
        slider1.interactable = true;
        slider2.interactable = true;
        submitButton.interactable = AreValuesCorrect();
        phone.ResetCountdown();
        Refresh();
    }

    bool AreValuesCorrect() {
        return value1 == 1 && value2 == 1;
    }

    IEnumerator SubmitRoutine() {
        submitButton.interactable = false;
        slider1.interactable = false;
        slider2.interactable = false;
        phone.CompleteTask();
        SoundManager.instance.Play(correctSound);
        successScreen.gameObject.SetActive(true);
        yield return Tween.StartRoutine(0.3f, (float progress) => {
            successScreen.anchoredPosition = Vector2.down * 300 * Easing.CubicOut(1 - progress);
        });
        yield return new WaitForSeconds(0.5f);
        NewPhoto();
        slider1.value = 0;
        slider2.value = 0;
        yield return Tween.StartRoutine(0.3f, (float progress) => {
            successScreen.anchoredPosition = Vector2.up * 300 * Easing.CubicOut(progress);
        });
        successScreen.gameObject.SetActive(false);
    }
}
