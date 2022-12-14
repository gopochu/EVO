using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Storage))]
public class Mule : Unit
{
    [Header("Targets")]
    [SerializeField] public Storehouse BaseStorehouse;
    [SerializeField] public Mineshaft Mineshaft;
    [SerializeField] public Unit MainTarget;
    
    [Header("Other")]
    [SerializeField] public float TimeToDeposit = 2f;
    [SerializeField] public float TimeToCollect = 3f;
    [SerializeField] public float Speed = 10f;

    public MuleBaseState DeliveringState = new MuleDeliveringState();
    public MuleBaseState CollectingState = new MuleCollectingState();
    public MuleBaseState DepositingState = new MuleDepositingState();
    public MuleBaseState IdleState = new MuleIdleState();
    public MuleBaseState WalkingState = new MuleWalkingState();
    private MuleBaseState _currentState;
    [HideInInspector] public Storage Storage;
    [HideInInspector] public Rigidbody2D Rigidbody2D;
    [HideInInspector] public Vector2 WalkDestination;

    private List<Order> _orderPriority = new List<Order>()
        {
            Order.Follow
        };

    private void Awake() 
    {
        Storage = GetComponent<Storage>();
        Rigidbody2D = GetComponent<Rigidbody2D>();
        _currentState = IdleState;
        BaseStorehouse = FindObjectOfType<Storehouse>();
    }

    public override void Start()
    {
        base.Start();
        BaseStorehouse.GetComponent<Health>().OnDeath.AddListener(ForceIdleState);
    }

    private void Update() 
    {
        _currentState.UpdateState(this);
    }

    private void FixedUpdate()
    {
        _currentState.FixedUpdateState(this);
    }

    public void SwitchState(MuleBaseState state)
    {
        _currentState = state;
        _currentState.EnterState(this);
    }

    public void ToggleTarget()
    {
        if(MainTarget == Mineshaft)
        {
            MainTarget = BaseStorehouse;
        }
        else if(MainTarget == BaseStorehouse)
        {
            MainTarget = Mineshaft;
        }
    }

    public override bool WalkOrder(Vector2 position)
    {
        WalkDestination = position;
        SwitchState(WalkingState);
        return true;
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
        Mineshaft = mineshaft;
        Mineshaft.GetComponent<Health>().OnDeath.AddListener(ForceIdleState);
        MainTarget = Mineshaft;
        SwitchState(DeliveringState);
        return true;
    }

    public override List<Order> GetOrderPriority()
    {
        return _orderPriority;
    }

    public override void HandleDeath()
    {
        Destroy(gameObject);
    }

    private void ForceIdleState()
    {
        SwitchState(IdleState);
    }
}
