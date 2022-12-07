using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UnitPreview : MonoBehaviour
{
    public GameObject Unit;
    public int OilCost;
    public int MetalCost;
    public abstract bool IsValidPlacement();
}
