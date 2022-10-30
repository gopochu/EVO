using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Chaser : EnemyUnit
{
    [SerializeField] public Health Target;
    [SerializeField] public float Speed;

    [Header("Damage Options")]
    [SerializeField] public float DistanceToHit;
    [SerializeField] public int Damage;
    [SerializeField] public float PunchCooldown;
    [HideInInspector] public float CurrentPunchCooldown;

    private ChaserBaseState _currentState;
    public ChaserIdleState IdleState = new ChaserIdleState();
    public ChaserChaseState ChaseState = new ChaserChaseState();
    public ChaserAttackState AttackState = new ChaserAttackState();
    [HideInInspector] public Rigidbody2D Rigidbody2D;


    private void Awake() 
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
    }
    
    private void Start()
    {
        SwitchState(ChaseState);
    }

    private void Update()
    {
        _currentState.UpdateState(this);
        UpdateTimer();
    }

    private void FixedUpdate()
    {
        Debug.Log(_currentState);
        _currentState.FixedUpdateState(this);
    }

    public void SwitchState(ChaserBaseState state)
    {
        _currentState = state;
        _currentState.EnterState(this);
    }

    private void UpdateTimer()
    {
        CurrentPunchCooldown = Mathf.Max(0, CurrentPunchCooldown -= Time.deltaTime);
    }

}
