using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

public static class Directions
{
    private static List<Vector2> _eightDirections = new List<Vector2>
    {
        new Vector2(1, 0).normalized,
        new Vector2(1,-1).normalized,
        new Vector2(0, -1).normalized,
        new Vector2(-1, -1).normalized,
        new Vector2(-1, 0).normalized,
        new Vector2(-1, 1).normalized,
        new Vector2(0, 1).normalized,
        new Vector2(1, 1).normalized
    };
    public static ReadOnlyCollection<Vector2> EightDirections = _eightDirections.AsReadOnly();
}
