using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
public abstract class EnemyUnit : MonoBehaviour, ITargetable
{
    protected abstract void Attack(Health target);
    private List<Order> _orderPriority = new List<Order>()
    {
        Order.Attack,
        Order.Follow
    };


    public GameObject GetGameObject()
    {
        return gameObject;
    }

    public List<Order> GetOrderPriority()
    {
        return _orderPriority;
    }
}
