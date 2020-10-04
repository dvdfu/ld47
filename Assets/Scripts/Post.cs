using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Post : MonoBehaviour {
    [SerializeField] GameData gameData = null;
    [SerializeField] Text usernameUI = null;
    [SerializeField] Text likeCount = null;
    [SerializeField] RectTransform post = null;
    [SerializeField] RectTransform glyphContainer = null;
    [SerializeField] Image profilePicture = null;
    [SerializeField] Image heart = null;
    [SerializeField] Image verified = null;
    [SerializeField] GameObject glyphPrefab = null;
    [SerializeField] Sprite[] profileSprites = null;
    [SerializeField] Sprite profileCeleb = null;
    [SerializeField] Color redColor = Color.white;
    [SerializeField] Color celebColor = Color.white;

    string content;
    bool isLiked;
    int likes;

    public void GenerateTrending(char c) {
        int length = Random.Range(4, 8);
        content = "";
        likes = Random.Range(10000, 99999);
        verified.enabled = true;
        for (int i = 0; i < length; i++) {
            content += c;
            Glyph glyph = Instantiate(glyphPrefab, glyphContainer).GetComponent<Glyph>();
            glyph.SetGlyph(c);
        }
        profilePicture.sprite = profileCeleb;
        post.GetComponent<Image>().color = celebColor;
        StartCoroutine(ShowRoutine());
    }

    public void Generate(int length) {
        content = "";
        likes = Random.Range(0, 9);
        verified.enabled = false;
        for (int i = 0; i < length; i++) {
            char c = (char) Random.Range('0', '9');
            content += c;
            Glyph glyph = Instantiate(glyphPrefab, glyphContainer).GetComponent<Glyph>();
            glyph.SetGlyph(c);
        }
        profilePicture.sprite = profileSprites[Random.Range(0, profileSprites.Length)];
        StartCoroutine(ShowRoutine());
    }

    public bool HasChar(char c) {
        return content.IndexOf(c) >= 0;
    }

    public void Like() {
        if (!isLiked) {
            isLiked = true;
            likes++;
            heart.color = redColor;
            gameData.LikePost(content);
        }
    }

    public bool IsLiked() {
        return isLiked;
    }

    void LateUpdate() {
        likeCount.text = likes.ToString();
    }

    float GetHeight() {
        if (content.Length > 5) {
            return 140;
        }
        return 100;
    }

    IEnumerator ShowRoutine() {
        float width = post.sizeDelta.x;
        yield return Tween.StartRoutine(0.5f, (float progress) => {
            float height = GetHeight() * Easing.CubicIn(progress);
            post.sizeDelta = new Vector2(width, height);
        });
    }
}
