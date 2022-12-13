using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : Unit
{
    private TurretBaseState _currentState;
    public TurretAttackState AtackState = new TurretAttackState();
    public TurretIdleState IdleState = new TurretIdleState();

    [SerializeField] public int Damage = 1;
    [SerializeField] public float AttackCooldown;
    public float CurrentAttackCooldown;
    public GameObject Target;


    private void Awake()
    {
        _currentState = IdleState;

        _currentState.EnterState(this);
    }

    // Update is called once per frame
    void Update()
    {
        _currentState.UpdateState(this);
        UpdateCooldown();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.name);
        _currentState.OnCollisionEnter(this, collision.gameObject);
    }

    public void SwitchState(TurretBaseState state)
    {
        _currentState = state;
        state.EnterState(this);
    }
    private void UpdateCooldown()
    {
        CurrentAttackCooldown = Mathf.Max(0, CurrentAttackCooldown - Time.deltaTime);
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
        throw new System.NotImplementedException();
    }

    public override void HandleDeath()
    {
        Destroy(this.gameObject);
    }
}
