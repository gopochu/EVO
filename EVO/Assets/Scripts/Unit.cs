using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
public abstract class Unit : MonoBehaviour, ITargetable
{
    public abstract bool WalkOrder(Vector2 position);
    public abstract bool FollowOrder(GameObject target);
    public abstract bool AttackOrder(GameObject target);
    public abstract bool ToggleElectricityOrder();
    public abstract bool DeliverOrder(Mineshaft mineshaft);
    public abstract List<Order> GetOrderPriority();
    public GameObject GetGameObject()
    {
        return gameObject;
    }

}
