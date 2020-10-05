using UnityEngine;

public class MathUtils {
    public static Vector2 PolarToCartesian(float angle, float length = 1) {
        return PolarToCartesianRad(angle * Mathf.Deg2Rad, length);
    }

    public static Vector2 PolarToCartesianRad(float angleRad, float length = 1) {
        return new Vector2(Mathf.Cos(angleRad), Mathf.Sin(angleRad)) * length;
    }

    public static float VectorToAngle(Vector3 v) {
        return VectorToAngle((Vector2) v);
    }

    public static float VectorToAngle(Vector2 v) {
        return Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
    }

    public static float WrapWithin(float x, float lower, float upper) {
        Debug.Assert(upper > lower);
        return Mathf.Repeat(x - upper, upper - lower) + lower;
    }

    // A value between [-amplitude, amplitude] following a sine wave based on the period and current time
    public static float TimeSin(float time, float period, float amplitude = 1) {
        float t = (time / period) % 1;
        return Mathf.Sin(Mathf.PI * 2 * t) * amplitude;
    }

    public static float QuadArc(float time, float amplitude = 1) {
        float x = 2 * time - 1;
        return (1 - x * x) * amplitude;
    }

    public static float SnapTo(float x, float step) {
        return Mathf.Floor(x / step) * step;
    }

    public static float SnapToCeil(float x, float step) {
        return Mathf.Ceil(x / step) * step;
    }
}