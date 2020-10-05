using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhoneFeed : MonoBehaviour {
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
            float height = Mathf.Lerp(60, refreshSize.y, Easing.CubicOut(progress));
            refresh.sizeDelta = new Vector2(refreshSize.x, height);
        });
        int count = Random.Range(1, 5);
        for (int i = 0; i < count; i++) {
            Instantiate(postPrefab, feed).transform.SetSiblingIndex(0);
            SoundManager.instance.Play(postSound);
            if (feed.transform.childCount > 7) {
                Destroy(feed.transform.GetChild(7).gameObject);
            }
            yield return new WaitForSeconds(0.1f);
        }
        isRefreshing = false;
        refreshText.text = "Refresh";
        refresh.GetComponent<Button>().interactable = true;
    }
}
