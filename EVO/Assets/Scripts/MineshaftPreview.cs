using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineshaftPreview : UnitPreview
{
    [SerializeField] public LayerMask SourceMask;
    public override bool IsValidPlacement()
    {
        var point = Physics2D.OverlapCircle(transform.position, 0f, SourceMask);
        if(point == null) return false;
        return !point.GetComponent<Source>().IsOccupied;
    }
}
