using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhoneFeed : MonoBehaviour {
    [SerializeField] GameData gameData = null;
    [SerializeField] Phone phone = null;
    [SerializeField] RectTransform feed = null;
    [SerializeField] RectTransform refresh = null;
    [SerializeField] Text refreshText = null;
    [SerializeField] GameObject postPrefab = null;
    [SerializeField] AudioClip refreshSound = null;
    [SerializeField] AudioClip postSound = null;

    bool isRefreshing;

    public void OnPressRefresh() {
        if (!isRefreshing) {
            isRefreshing = true;
            phone.CompleteTask();
            SoundManager.instance.Play(refreshSound);
            StartCoroutine(RefreshRoutine());
        }
    }

    void Start() {
        OnPressRefresh();
    }
    
    IEnumerator RefreshRoutine() {
        refreshText.text = "Loading...";
        refresh.GetComponent<Button>().interactable = false;
        Vector2 refreshSize = refresh.sizeDelta;
        yield return Tween.StartRoutine(0.2f, (float progress) => {
            float height = Mathf.Lerp(refreshSize.y, 60, Easing.CubicIn(progress));
            refresh.sizeDelta = new Vector2(refreshSize.x, height);
        });
        yield return new WaitForSeconds(0.6f);
        yield return Tween.StartRoutine(0.2f, (float progress) => {
            float height = Mathf.Lerp(60, 0, Easing.CubicOut(progress));
            refresh.sizeDelta = new Vector2(refreshSize.x, height);
        });
        phone.StartDepleting();
        int count = Random.Range(1, 5);
        gameData.postsLeft = count;
        for (int i = 0; i < count; i++) {
            Instantiate(postPrefab, feed).transform.SetSiblingIndex(0);
            SoundManager.instance.Play(postSound);
            if (feed.transform.childCount > 10) {
                Destroy(feed.transform.GetChild(10).gameObject);
            }
            yield return new WaitForSeconds(0.1f);
        }

        while (gameData.postsLeft > 0) {
            yield return null;
        }
        isRefreshing = false;
        refreshText.text = "Refresh";
        refresh.GetComponent<Button>().interactable = true;
        yield return Tween.StartRoutine(0.2f, (float progress) => {
            float height = Mathf.Lerp(0, 30, Easing.CubicIn(progress));
            refresh.sizeDelta = new Vector2(refreshSize.x, height);
        });
    }
}
