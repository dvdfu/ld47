using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Glyph : MonoBehaviour {
    [SerializeField] Image image = null;
    [SerializeField] Sprite glyph0 = null;
    [SerializeField] Sprite glyph1 = null;
    [SerializeField] Sprite glyph2 = null;
    [SerializeField] Sprite glyph3 = null;
    [SerializeField] Sprite glyph4 = null;
    [SerializeField] Sprite glyph5 = null;
    [SerializeField] Sprite glyph6 = null;
    [SerializeField] Sprite glyph7 = null;
    [SerializeField] Sprite glyph8 = null;
    [SerializeField] Sprite glyph9 = null;

    char glyph;

    public void SetGlyph(char c) {
        if (c == glyph) {
            return;
        }
        switch (c) {
            case '0': image.sprite = glyph0; break;
            case '1': image.sprite = glyph1; break;
            case '2': image.sprite = glyph2; break;
            case '3': image.sprite = glyph3; break;
            case '4': image.sprite = glyph4; break;
            case '5': image.sprite = glyph5; break;
            case '6': image.sprite = glyph6; break;
            case '7': image.sprite = glyph7; break;
            case '8': image.sprite = glyph8; break;
            case '9': image.sprite = glyph9; break;
        }
    }
}
