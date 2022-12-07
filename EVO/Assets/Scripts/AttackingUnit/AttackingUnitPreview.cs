using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackingUnitPreview : UnitPreview
{
    public override bool IsValidPlacement()
    {
        var point = Physics2D.OverlapCircle(transform.position, 0f, Player.Instance.BuildArea);
        if(point == null)
        {
            return false;
        }
        return true;
    }
}
