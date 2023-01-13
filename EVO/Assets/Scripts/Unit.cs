using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Health))]
public abstract class Unit : MonoBehaviour, ITargetable, IWalking
{
    [SerializeField] public bool IsBuilding;

    public abstract bool WalkOrder(Vector2 position);
    public abstract bool FollowOrder(GameObject target);
    public abstract bool AttackOrder(Health target);
    public abstract bool ToggleElectricityOrder();
    public abstract bool DeliverOrder(Mineshaft mineshaft);
    public abstract List<Order> GetOrderPriority();
    public abstract void HandleDeath();
    public GameObject HighlightArea;
    
    public GameObject GetGameObject()
    {
        return gameObject;
    }

    public virtual void Start()
    {
        if(IsBuilding) SpawnerManager.Instance.PlayerUnits.Add(this);
    }

    public virtual void OnDestroy() 
    {
        if(IsBuilding) SpawnerManager.Instance.PlayerUnits.Remove(this);
    }
    [SerializeField] private float baseSpeed;
    [SerializeField] private float speedMultiplier;

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
