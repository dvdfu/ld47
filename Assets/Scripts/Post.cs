using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Post : MonoBehaviour {
    [SerializeField] GameData gameData = null;
    [SerializeField] RectTransform container = null;
    [SerializeField] Image profile = null;
    [SerializeField] Image heart = null;
    [SerializeField] Text comment = null;
    [SerializeField] Sprite[] profileSprites = null;
    [SerializeField] Color mintColor = Color.white;

    bool isLiked;

    public void OnPress() {
        if (!isLiked) {
            isLiked = true;
            heart.color = mintColor;
            gameData.postsLeft--;
        }
    }

    void Awake() {
        int i = Random.Range(0, profileSprites.Length);
        profile.sprite = profileSprites[i];
        comment.text = GetRandomComment();
        StartCoroutine(ShowRoutine());
    }

    string GetRandomComment() {
        int i = Random.Range(0, 6);
        switch (i) {
            case 0: return "so good";
            case 1: return "pretty!";
            case 2: return "love it!!";
            case 3: return "the best";
            case 4: return "amazing!";
            case 5: return "omg wow!";
        }
        return "huh";
    }

    IEnumerator ShowRoutine() {
        Vector2 size = container.sizeDelta;
        yield return Tween.StartRoutine(0.3f, (float progress) => {
            container.sizeDelta = new Vector2(size.x, size.y * Easing.CubicIn(progress));
        });
    }
}
