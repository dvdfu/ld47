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
    
    int value1;
    int value2;

    int correctValue1;
    int correctValue2;
    int photoNumber;

    public void OnSubmit() {
        StartCoroutine(SubmitRoutine());
    }

    public void Refresh() {
        value1 = (int) slider1.value;
        value2 = (int) slider2.value;

        knob1.color = value1 == correctValue1 ? Color.white : mintColor;
        knob2.color = value2 == correctValue2 ? Color.white : mintColor;

        photo.GetComponent<RectTransform>().eulerAngles = Vector3.forward * (value1 - correctValue1) * 10;
        photo.color = Color.Lerp(Color.white, Color.black, Easing.CubicIn(Mathf.Abs(value2 - correctValue2) / 12f));

        submitButton.interactable = AreValuesCorrect();
    }

    void Start() {
        NewPhoto();
    }

    void NewPhoto() {
        photo.sprite = photos[photoNumber % photos.Length];
        photoNumber++;
        correctValue1 = (value1 + Random.Range(1, 6)) % 6;
        correctValue2 = (value2 + Random.Range(1, 6)) % 6;
        slider1.interactable = true;
        slider2.interactable = true;
        submitButton.interactable = AreValuesCorrect();
        phone.StartDepleting();
        Refresh();
    }

    bool AreValuesCorrect() {
        return value1 == correctValue1 && value2 == correctValue2;
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
        NewPhoto();
        yield return new WaitForSeconds(0.5f);
        yield return Tween.StartRoutine(0.3f, (float progress) => {
            successScreen.anchoredPosition = Vector2.up * 300 * Easing.CubicOut(progress);
        });
        successScreen.gameObject.SetActive(false);
    }
}
