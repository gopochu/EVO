using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class AttackingUnit : Unit
{
    //[Header("Walk Options")]
    //[SerializeField] public float Speed;

    [Header("Attack Options")]
    [SerializeField] public int Damage;
    [SerializeField] public float DistanceToHit;
    [SerializeField] public float DistanceToStopHit;
    [SerializeField] public float AttackCooldown;

    [Header("Chase Options")]
    [SerializeField] public float DirectionUpdateFrequency;
    [SerializeField] public float ObstacleWeight;
    [SerializeField] public float MinObstacleDistance;
    [SerializeField] public float MaxObstacleDistance;
    [SerializeField] public LayerMask ObstacleLayer;
    [SerializeField] public LayerMask AllyLayer;

    public AttackingUnitAttackState AttackState = new AttackingUnitAttackState();
    public AttackingUnitChaseState ChaseState = new AttackingUnitChaseState();
    public AttackingUnitIdleState IdleState = new AttackingUnitIdleState();
    public AttackingUnitWalkState WalkState = new AttackingUnitWalkState();
    private AttackingUnitBaseState _currentState;

    [Header("Debug Info")]
    [SerializeField] public Health Target;
    [SerializeField] public Vector2 WalkDestination;
    [SerializeField] public float CurrentAttackCooldown;
    [HideInInspector] public Rigidbody2D Rigidbody2D;

    private List<Order> _orderPriority = new List<Order>()
    {
        Order.Follow
    };

    private void Awake() 
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public override void Start() 
    {
        base.Start();
        SwitchState(IdleState);
    }
    
    private void Update()
    {
        _currentState.UpdateState(this);
        UpdateTimer();
    }

    private void FixedUpdate() 
    {
        _currentState.FixedUpdateState(this);
    }

    public void SwitchState(AttackingUnitBaseState state)
    {
        _currentState?.ExitState(this);
        _currentState = state;
        state.EnterState(this);
    }

    private void UpdateTimer()
    {
        CurrentAttackCooldown = Mathf.Max(0, CurrentAttackCooldown - Time.deltaTime);
    }

    public override bool AttackOrder(Health target)
    {
        Target = target;
        SwitchState(AttackState);
        return true;
    }

    public override bool DeliverOrder(Mineshaft mineshaft)
    {
        return false;
    }

    public override bool FollowOrder(GameObject target)
    {
        return false;
    }

    public override bool WalkOrder(Vector2 position)
    {
        WalkDestination = position;
        SwitchState(WalkState);
        return true;
    }
    public override bool ToggleElectricityOrder()
    {
        return false;
    }

    public override List<Order> GetOrderPriority()
    {
        return _orderPriority;
    }

    public override void HandleDeath()
    {
        Destroy(this.gameObject);
    }

}
