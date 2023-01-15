using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackingUnitAttackState : AttackingUnitBaseState
{
    public override void EnterState(AttackingUnit manager)
    {
        
    }

    public override void ExitState(AttackingUnit manager)
    {
        
    }

    public override void FixedUpdateState(AttackingUnit manager)
    {

    }

    public override void UpdateState(AttackingUnit manager)
    {
        if(manager.Target == null)
        {
            manager.SwitchState(manager.IdleState);
            return;
        }
        if(Vector2.Distance(manager.Target.transform.position, manager.transform.position) > manager.DistanceToStopHit + manager.Target.AgentRadius)
        {
            manager.SwitchState(manager.ChaseState);
            return;
        }
        if(manager.CurrentAttackCooldown == 0)
        {
            manager.CurrentAttackCooldown = manager.AttackCooldown;
            manager.Target.SetHealth(manager.Target.CurrentHealth - manager.Damage, manager.gameObject);
        }
    }
}
