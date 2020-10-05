using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    [SerializeField] GameData gameData = null;
    [SerializeField] RectTransform taskText = null;
    [SerializeField] RectTransform taskFeed = null;
    [SerializeField] RectTransform taskPhoto = null;
    [SerializeField] RectTransform taskCall = null;

    void Start() {
        StartCoroutine(ProgressRoutine());
    }

    IEnumerator ProgressRoutine() {
        taskFeed.gameObject.SetActive(true);
        yield return Tween.StartRoutine(0.5f, (float progress) => {
            float x = Mathf.Lerp(1000, 0, Easing.CubicIn(progress));
            taskFeed.anchoredPosition = new Vector2(x, -60);
        });
        yield return new WaitForSeconds(1);

        taskCall.gameObject.SetActive(true);
        yield return Tween.StartRoutine(0.5f, (float progress) => {
            float x = Mathf.Lerp(1000, 150, Easing.CubicIn(progress));
            taskCall.anchoredPosition = new Vector2(x, -60);

            x = Mathf.Lerp(0, -150, Easing.CubicIn(progress));
            taskFeed.anchoredPosition = new Vector2(x, -60);
        });
        yield return new WaitForSeconds(1);

        taskPhoto.gameObject.SetActive(true);
        yield return Tween.StartRoutine(0.5f, (float progress) => {
            float x = Mathf.Lerp(1000, 250, Easing.CubicIn(progress));
            taskPhoto.anchoredPosition = new Vector2(x, -60);

            x = Mathf.Lerp(150, 0, Easing.CubicIn(progress));
            taskCall.anchoredPosition = new Vector2(x, -60);

            x = Mathf.Lerp(-150, -250, Easing.CubicIn(progress));
            taskFeed.anchoredPosition = new Vector2(x, -60);
        });
        yield return new WaitForSeconds(1);

        taskText.gameObject.SetActive(true);
        yield return Tween.StartRoutine(0.5f, (float progress) => {
            float x = Mathf.Lerp(1000, 300, Easing.CubicIn(progress));
            taskText.anchoredPosition = new Vector2(x, -60);

            x = Mathf.Lerp(250, 100, Easing.CubicIn(progress));
            taskPhoto.anchoredPosition = new Vector2(x, -60);

            x = Mathf.Lerp(0, -100, Easing.CubicIn(progress));
            taskCall.anchoredPosition = new Vector2(x, -60);

            x = Mathf.Lerp(-250, -300, Easing.CubicIn(progress));
            taskFeed.anchoredPosition = new Vector2(x, -60);
        });
    }
}
