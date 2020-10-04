using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    [SerializeField] GameData gameData = null;
    [SerializeField] RectTransform feedUI = null;
    [SerializeField] GameObject postPrefab = null;

    void Start() {
        gameData.Reset();
        StartCoroutine(GameRoutine());
    }

    void NewPost() {
        int length = Random.Range(2, 8);
        Post post = Instantiate(postPrefab, feedUI).GetComponent<Post>();
        post.transform.SetSiblingIndex(0);
        post.Generate(length);
        if (feedUI.transform.childCount > 5) {
            Destroy(feedUI.transform.GetChild(5).gameObject);
        }
    }

    void NewTrendingPost() {
        gameData.trending = (char) Random.Range('0', '9');
        Post post = Instantiate(postPrefab, feedUI).GetComponent<Post>();
        post.transform.SetSiblingIndex(0);
        post.GenerateTrending(gameData.trending);
        if (feedUI.transform.childCount > 5) {
            Destroy(feedUI.transform.GetChild(5).gameObject);
        }
    }

    IEnumerator GameRoutine() {
        while (true) {
            NewTrendingPost();
            yield return new WaitForSeconds(2);
            int posts = Random.Range(8, 20);
            for (int i = 0; i < posts; i++) {
                NewPost();
                yield return new WaitForSeconds(Mathf.Lerp(1.6f, 0.8f, gameData.score / 25f));
            }
            yield return new WaitForSeconds(1);
        }
    }
}
