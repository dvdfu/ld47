using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tentacle : MonoBehaviour {
    [SerializeField] Image image = null;
    [SerializeField] Sprite hoverSprite = null;
    [SerializeField] Sprite pressSprite = null;

    Vector3 position;
    
    void Update() {
        position = Vector3.Lerp(position, Camera.main.ScreenToWorldPoint(Input.mousePosition), 0.2f);
        position.y = Mathf.Min(position.y, 100);
        transform.position = position + new Vector3(250, -174);
        image.sprite = Input.GetMouseButton(0) ? pressSprite : hoverSprite;
    }
}
