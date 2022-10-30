using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaserAttackState : ChaserBaseState
{
    public override void EnterState(Chaser manager)
    {
        
    }

    public override void FixedUpdateState(Chaser manager)
    {

    }

    public override void UpdateState(Chaser manager)
    {
        if(manager.Target == null)
        {
            manager.SwitchState(manager.IdleState);
            return;
        }
        if(Vector2.Distance(manager.transform.position, manager.Target.transform.position) > manager.DistanceToHit)
        {
            manager.SwitchState(manager.ChaseState);
            return;
        }
        if(manager.CurrentPunchCooldown == 0)
        {
            manager.CurrentPunchCooldown = manager.PunchCooldown;
            manager.Target.DecreaseHealth(manager.Damage);
        }
    }
}
