using UnityEngine;

public static class Easing {
    public static float Step(float t, float x) {
        if (t < x) return 0;
        return 1;
    }

    public static float Flyby(float t) {
        return t * (t * (4 * t - 6) + 3);
    }

    public static float QuadIn(float t) {
        return 1 - QuadOut(1 - t);
    }

    public static float QuadOut(float t) {
        return t * t;
    }

    public static float CubicIn(float t) {
        return 1 - CubicOut(1 - t);
    }

    public static float CubicOut(float t) {
        return t * t * t;
    }

    public static float CubicInOut(float t) {
        if (t < 0.5f) return 4 * t * t * t;
        return (t - 1) * (2 * t - 2) * (2 * t - 2) + 1;
    }

    public static float ElasticOut(float t) {
        float p = 0.3f;
        return Mathf.Pow(2, -10 * t) * Mathf.Sin((t - p / 4) * 2 * Mathf.PI / p) + 1;
    }
}