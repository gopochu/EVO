using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Storehouse))]
public class MuleStateManager : MonoBehaviour
{
    [Header("Targets")]
    [SerializeField] public GameObject BaseStorehouse;
    [SerializeField] public GameObject Mineshaft;
    [SerializeField] public GameObject MainTarget;

    [Header("Oil")]
    [SerializeField] public int OilCount;
    [SerializeField] private int _oilMax = 20;

    [Header("Metal")]
    [SerializeField] public int MetalCount;
    [SerializeField] private int _metalMax = 100;

    [Header("Other")]
    [SerializeField] public float TimeToDeposit = 2f;
    [SerializeField] public float TimeToCollect = 3f;
    [SerializeField] public float Speed = 10f;

    [HideInInspector] public MuleBaseState WalkingState = new MuleWalkingState();
    [HideInInspector] public MuleBaseState CollectingState = new MuleCollectingState();
    [HideInInspector] public MuleBaseState DepositingState = new MuleDepositingState();
    [HideInInspector] private MuleBaseState _currentState;
    [HideInInspector] public Storehouse Storehouse;
    [HideInInspector] public Rigidbody2D Rigidbody2D;

    private void Awake() 
    {
        Storehouse = GetComponent<Storehouse>();
        Rigidbody2D = GetComponent<Rigidbody2D>();
        _currentState = WalkingState;
    }

    private void Update() {
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
}
