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
    [SerializeField] Image redOverlay = null;
    [SerializeField] Image blackOverlay = null;
    [SerializeField] Text screenText = null;
    [SerializeField] AudioClip wrongSound = null;
    [SerializeField] CameraHelper cameraHelper = null;

    Tween tween;

    void Awake() {
        Application.targetFrameRate = 60;
        tween = new Tween(this);
        gameData.failTaskEvent.AddListener(OnFailTask);
        taskText.gameObject.SetActive(false);
        taskFeed.gameObject.SetActive(false);
        taskPhoto.gameObject.SetActive(false);
        taskCall.gameObject.SetActive(false);
    }

    void Start() {
        StartCoroutine(StartRoutine());
    }

    void OnDestroy() {
        gameData.failTaskEvent.RemoveListener(OnFailTask);
    }

    void OnFailTask() {
        SoundManager.instance.Play(wrongSound);
        cameraHelper.Shake();
        tween.Start(0.5f, (float progress) => {
            float amount = Mathf.Lerp(1, 0, Easing.CubicIn(progress));
            redOverlay.color = new Color(1, 0, 0, amount);
        });
    }

    IEnumerator StartRoutine() {
        blackOverlay.enabled = true;
        Color clearWhite = new Color(1, 1, 1, 0);
        Color clearBlack = new Color(0, 0, 0, 0);
        yield return Tween.StartRoutine(0.5f, (float progress) => {
            screenText.color = Color.Lerp(clearWhite, Color.white, progress);
        });
        yield return new WaitForSeconds(3);
        yield return Tween.StartRoutine(0.5f, (float progress) => {
            screenText.color = Color.Lerp(Color.white, clearWhite, progress);
        });
        yield return Tween.StartRoutine(1, (float progress) => {
            blackOverlay.color = Color.Lerp(Color.black, clearBlack, progress);
        });
        blackOverlay.enabled = false;
        StartCoroutine(ProgressRoutine());
    }

    IEnumerator ProgressRoutine() {
        taskPhoto.gameObject.SetActive(true);
        yield return Tween.StartRoutine(0.5f, (float progress) => {
            float x = Mathf.Lerp(1000, 0, Easing.CubicIn(progress));
            taskPhoto.anchoredPosition = new Vector2(x, -30);
        });
        while (gameData.score < 3) {
            yield return null;
        }

        taskFeed.gameObject.SetActive(true);
        yield return Tween.StartRoutine(0.5f, (float progress) => {
            float x = Mathf.Lerp(1000, 150, Easing.CubicIn(progress));
            taskFeed.anchoredPosition = new Vector2(x, -30);

            x = Mathf.Lerp(0, -150, Easing.CubicIn(progress));
            taskPhoto.anchoredPosition = new Vector2(x, -30);
        });
        while (gameData.score < 10) {
            yield return null;
        }

        taskCall.gameObject.SetActive(true);
        yield return Tween.StartRoutine(0.5f, (float progress) => {
            float x = Mathf.Lerp(1000, 250, Easing.CubicIn(progress));
            taskCall.anchoredPosition = new Vector2(x, -30);

            x = Mathf.Lerp(150, 0, Easing.CubicIn(progress));
            taskFeed.anchoredPosition = new Vector2(x, -30);

            x = Mathf.Lerp(-150, -250, Easing.CubicIn(progress));
            taskPhoto.anchoredPosition = new Vector2(x, -30);
        });
        while (gameData.score < 30) {
            yield return null;
        }

        taskText.gameObject.SetActive(true);
        yield return Tween.StartRoutine(0.5f, (float progress) => {
            float x = Mathf.Lerp(1000, 300, Easing.CubicIn(progress));
            taskText.anchoredPosition = new Vector2(x, -30);

            x = Mathf.Lerp(250, 100, Easing.CubicIn(progress));
            taskCall.anchoredPosition = new Vector2(x, -30);

            x = Mathf.Lerp(0, -100, Easing.CubicIn(progress));
            taskFeed.anchoredPosition = new Vector2(x, -30);

            x = Mathf.Lerp(-250, -300, Easing.CubicIn(progress));
            taskPhoto.anchoredPosition = new Vector2(x, -30);
        });
        while (gameData.score < 60) {
            yield return null;
        }

        gameData.onFire = true;
        taskPhoto.GetComponent<Phone>().Burn();
        yield return new WaitForSeconds(1 + Random.value * 3);
        taskFeed.GetComponent<Phone>().Burn();
        yield return new WaitForSeconds(1 + Random.value * 3);
        taskCall.GetComponent<Phone>().Burn();
        yield return new WaitForSeconds(1 + Random.value * 3);
        taskText.GetComponent<Phone>().Burn();
    }
}
