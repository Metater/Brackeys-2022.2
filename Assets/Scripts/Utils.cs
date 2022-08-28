using UnityEngine;

public static class Utils
{
    public static float AngleBetweenTwoPoints(Vector3 a, Vector3 b)
    {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }

    public static float Get(float x, Vector4 input)
    {
        float p = -(Mathf.Log10(input.z) / Mathf.Log10(input.y / input.x));
        return (input.x / Mathf.Pow(x, 1 / p)) + input.w;
    }
}