using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraHelper : MonoBehaviour {
    [SerializeField] GameData gameData = null;
    [SerializeField] new Camera camera = null;

    Tween tween;

    public void Shake() {
        tween.Start(0.5f, (float progress) => {
            float amount = Mathf.Lerp(1, 0, Easing.CubicIn(progress));
            float angle = Random.value * 360;
            Vector2 offset = MathUtils.PolarToCartesian(angle, amount * 20);
            camera.transform.localPosition = offset;
        });
    }

    void Awake() {
        tween = new Tween(this);
    }
}
