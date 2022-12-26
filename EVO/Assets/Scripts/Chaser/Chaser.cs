using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Chaser : EnemyUnit
{
    //[Header("Walk Options")]
    //[SerializeField] public float Speed;

    [Header("Attack Options")]
    [SerializeField] public float DistanceToHit;
    [SerializeField] public float DistanceToStopHit;
    [SerializeField] public int Damage;
    [SerializeField] public float PunchCooldown;
    [SerializeField] public float ScanningEnemiesFrequency;

    [HideInInspector] public float CurrentPunchCooldown;

    [Header("Chase Options")]
    [SerializeField] public float DirectionUpdateFrequency;
    [SerializeField] public float ObstacleWeight;
    [SerializeField] public float MinObstacleDistance;
    [SerializeField] public float MaxObstacleDistance;
    [SerializeField] public LayerMask ObstacleLayer;
    [SerializeField] public LayerMask AllyLayer;


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
        _currentState.FixedUpdateState(this);
    }

    public void SwitchState(ChaserBaseState state)
    {
        _currentState?.ExitState(this);
        _currentState = state;
        _currentState.EnterState(this);
    }

    private void UpdateTimer()
    {
        CurrentPunchCooldown = Mathf.Max(0, CurrentPunchCooldown - Time.deltaTime);
    }

    public void HandleDeath()
    {
        Destroy(this.gameObject);
    }

}
