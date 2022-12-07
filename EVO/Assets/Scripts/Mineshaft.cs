using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Storage))]
public class Mineshaft : Unit
{
    [Header("Oil")]
    [SerializeField] private int _oilGain = 1;
    [SerializeField] private float _oilInterval = 1f;
    private float _oilTimer = 0;

    [Header("Metal")]
    [SerializeField] private int _metalGain = 1;
    [SerializeField] private float _metalInterval = 1f;
    private float _metalTimer = 0;
    private List<Order> _orderPriority =  new List<Order>()
        {
            Order.Deliver
        };

    [HideInInspector] public Storage Storehouse;

    private void Awake() 
    {
        Storehouse = GetComponent<Storage>();
    }

    private void Update() 
    {
        UpdateOil();
        UpdateMetal();
    }

    private void UpdateOil()
    {
        _oilTimer += Time.deltaTime;
        if(_oilTimer < _oilInterval) return;
        _oilTimer = 0;
        Storehouse.IncreaseOil(_oilGain);
    }

    private void UpdateMetal()
    {
        _metalTimer += Time.deltaTime;
        if(_metalTimer < _metalInterval) return;
        _metalTimer = 0;
        Storehouse.IncreaseMetal(_metalGain);
    }

    public override void HandleDeath()
    {
        Destroy(gameObject);
    }

    public override bool WalkOrder(Vector2 position)
    {
        return false;
    }

    public override bool FollowOrder(GameObject target)
    {
        return false;
    }

    public override bool AttackOrder(Health target)
    {
        return false;
    }

    public override bool ToggleElectricityOrder()
    {
        return false;
    }

    public override bool DeliverOrder(Mineshaft mineshaft)
    {
        return false;
    }

    public override List<Order> GetOrderPriority()
    {
       return _orderPriority;
    }
}
