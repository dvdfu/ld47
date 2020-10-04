using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tentacle : MonoBehaviour {
    [SerializeField] SpriteRenderer hand = null;
    [SerializeField] Sprite hoverSprite = null;
    [SerializeField] Sprite pressSprite = null;
    [SerializeField] ParticleSystem splat = null;
    [SerializeField] AudioClip pressSound = null;

    Vector3 position;
    
    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            splat.transform.position = transform.position;
            splat.Emit(1);
            SoundManager.instance.Play(pressSound);
            position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        if (!Input.GetMouseButton(0)) {
            position = Vector3.Lerp(position, Camera.main.ScreenToWorldPoint(Input.mousePosition), 0.1f);
        }
        hand.transform.position = position;
        hand.sprite = Input.GetMouseButton(0) ? pressSprite : hoverSprite;
    }
}
