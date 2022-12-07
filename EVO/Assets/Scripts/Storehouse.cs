using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Storage))]
public class Storehouse : Unit
{
    public static Storehouse Instance;
    private List<Order> _orderPriority = new List<Order>();

    private void Awake() 
    {
        Instance = this;
    }
    public override bool AttackOrder(Health target)
    {
        return false;
    }

    public override bool DeliverOrder(Mineshaft mineshaft)
    {
        return false;
    }

    public override bool FollowOrder(GameObject target)
    {
        return false;
    }

    public override bool ToggleElectricityOrder()
    {
        return false;
    }

    public override bool WalkOrder(Vector2 position)
    {
        return false;
    }

    public override List<Order> GetOrderPriority()
    {
        return _orderPriority;
    }
    
    public override void HandleDeath()
    {
        Destroy(gameObject);
    }
}
