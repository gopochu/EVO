using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    public List<Order> OrderPriority = new List<Order>();
    public abstract bool WalkOrder(Vector2 position);
    public abstract bool FollowOrder(GameObject target);
    public abstract bool AttackOrder(GameObject target);
    public abstract bool ToggleElectricityOrder();
    public abstract bool DeliverOrder(Mineshaft mineshaft);

}
