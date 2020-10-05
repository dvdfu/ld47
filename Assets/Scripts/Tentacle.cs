using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tentacle : MonoBehaviour {
    [SerializeField] Image image = null;
    [SerializeField] Sprite hoverSprite = null;
    [SerializeField] Sprite pressSprite = null;
    [SerializeField] AudioClip pressSound = null;

    Vector3 position;
    
    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            // SoundManager.instance.Play(pressSound);
        }
        position = Vector3.Lerp(position, Camera.main.ScreenToWorldPoint(Input.mousePosition), 0.1f);
        transform.position = position + new Vector3(180, -90);
        image.sprite = Input.GetMouseButton(0) ? pressSprite : hoverSprite;
    }
}
