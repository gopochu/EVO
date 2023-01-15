using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Storage))]
public class Mule : Unit
{
    [SerializeField] private bool _isInMenu;

    [Header("Targets")]
    [SerializeField] public Storehouse BaseStorehouse;
    [SerializeField] public Mineshaft Mineshaft;
    [SerializeField] public Unit MainTarget;
    
    [Header("Other")]
    [SerializeField] public float TimeToDeposit = 2f;
    [SerializeField] public float TimeToCollect = 3f;
    //[SerializeField] public float Speed = 10f;

    public MuleBaseState DeliveringState = new MuleDeliveringState();
    public MuleBaseState CollectingState = new MuleCollectingState();
    public MuleBaseState DepositingState = new MuleDepositingState();
    public MuleBaseState IdleState = new MuleIdleState();
    public MuleBaseState WalkingState = new MuleWalkingState();
    private MuleBaseState _currentState;
    [HideInInspector] public Storage Storage;
    [HideInInspector] public Rigidbody2D Rigidbody2D;
    [HideInInspector] public Vector2 WalkDestination;
    private Vector3 _newPos;
    private Vector3 _prevPos;
    private Vector3 _objVelocity;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    private List<Order> _orderPriority = new List<Order>()
        {
            Order.Follow
        };

    private void Awake() 
    {
        Storage = GetComponent<Storage>();
        Rigidbody2D = GetComponent<Rigidbody2D>();
        _currentState = IdleState;
        if(_isInMenu) _currentState = DeliveringState;
        BaseStorehouse = FindObjectOfType<Storehouse>();
        //_spriteRenderer = GetComponent<SpriteRenderer>();

    }

    public override void Start()
    {
        base.Start();
        BaseStorehouse.GetComponent<Health>().OnDeath.AddListener(ForceIdleState);
    }

    private void Update() 
    {
        _currentState.UpdateState(this);
        ChangeSpriteDirection();
    }

    private void FixedUpdate()
    {
        _currentState.FixedUpdateState(this);
        CalculateVelocity();
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

    private void CalculateVelocity()
    {
        _newPos = transform.position; 
        _objVelocity = (_newPos - _prevPos) / Time.fixedDeltaTime;  
        _prevPos = _newPos;
    }
    private void ChangeSpriteDirection()
    {
        if(_objVelocity.x > 0) 
            _spriteRenderer.transform.localScale =  new Vector3
            (1,
            _spriteRenderer.transform.localScale.y,
            _spriteRenderer.transform.localScale.z);
        //Debug.Log(_objVelocity)
        else if(_objVelocity.x < 0)
        {
            _spriteRenderer.transform.localScale =  new Vector3
            (-1,
            _spriteRenderer.transform.localScale.y,
            _spriteRenderer.transform.localScale.z
            );
        }
            
       //Debug.Log(_objVelocity);
    }
}
