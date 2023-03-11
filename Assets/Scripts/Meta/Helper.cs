using UnityEngine;

class Helper
{
    public static float rnd(float f, int d = 4) => Mathf.Round(f * Mathf.Pow(10, d)) / Mathf.Pow(10, d);
    public static Vector2 rndV(Vector2 v, int d = 4) => new Vector2(rnd(v.x), rnd(v.y));
    public static float GetJumpSpeed(float height) => Mathf.Sqrt(-2f * Physics.gravity.y * height);
}