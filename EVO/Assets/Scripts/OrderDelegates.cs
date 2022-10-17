using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderDelegates
{
    public delegate void WalkOrderDelegate(Vector2 position);
    public delegate void FollowOrderDelegate(GameObject target);
    public delegate void AttackOrderDelegate(GameObject target);
    public delegate void ToggleElectricityOrderDelegate();
    public delegate void DeliverOrderDelegate(Mineshaft mineshaft);

}