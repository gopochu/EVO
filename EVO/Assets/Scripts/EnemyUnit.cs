using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Health))]
public abstract class EnemyUnit : MonoBehaviour, ITargetable, IWalking
{
    [SerializeField] public Health Target;
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
    
    [SerializeField] private float baseSpeed = 2;
    [SerializeField] private float speedMultiplier = 5;
    
    public float BaseSpeed
    {
        get => baseSpeed;
        set => baseSpeed = value;
    }
    public float SpeedMultiplier
    {
        get => speedMultiplier;
        set => speedMultiplier = value;
    }
}