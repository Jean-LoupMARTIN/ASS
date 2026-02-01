using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public static class MathExtension
{
    public static float Lerp(this Vector2 vector2, float t) => Mathf.Lerp(vector2.x, vector2.y, t);
}


