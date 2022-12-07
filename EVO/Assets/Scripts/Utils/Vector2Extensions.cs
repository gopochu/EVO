using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Vector2Extensions
{
    public static Vector2 RotateVector(this Vector2 vector, float angle)
    {
        return Quaternion.Euler(0,0,angle) * vector;
    }
}
