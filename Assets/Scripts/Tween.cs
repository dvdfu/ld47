using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tween {
    public delegate void OnUpdate(float progress);

    MonoBehaviour mb;
    Coroutine routine = null;

    public static IEnumerator StartRoutine(float duration, OnUpdate onUpdate) {
        float t = 0;
        while (t < duration) {
            onUpdate(t / duration);
            t += Time.deltaTime;
            yield return null;
        }
        onUpdate(1);
    }

    public Tween(MonoBehaviour mb) {
        this.mb = mb;
    }

    public void Start(float duration, OnUpdate onUpdate) {
        Stop();
        routine = mb.StartCoroutine(AnimateRoutine(duration, onUpdate));
    }

    public void Stop() {
        if (IsAnimating()) {
            mb.StopCoroutine(routine);
        }
    }

    public bool IsAnimating() {
        return routine != null;
    }

    IEnumerator AnimateRoutine(float duration, OnUpdate onUpdate) {
        float t = 0;
        while (t < duration) {
            onUpdate(t / duration);
            t += Time.deltaTime;
            yield return null;
        }
        Stop();
    }
}