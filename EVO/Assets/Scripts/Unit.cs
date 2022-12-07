using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
public abstract class Unit : MonoBehaviour, ITargetable
{
    [SerializeField] public bool IsBuilding;
    public abstract bool WalkOrder(Vector2 position);
    public abstract bool FollowOrder(GameObject target);
    public abstract bool AttackOrder(Health target);
    public abstract bool ToggleElectricityOrder();
    public abstract bool DeliverOrder(Mineshaft mineshaft);
    public abstract List<Order> GetOrderPriority();
    public abstract void HandleDeath();
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

}
