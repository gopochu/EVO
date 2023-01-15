using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackingUnitWalkState : AttackingUnitBaseState
{
    public override void EnterState(AttackingUnit manager)
    {
        
    }

    public override void ExitState(AttackingUnit manager)
    {
        
    }

    public override void FixedUpdateState(AttackingUnit manager)
    {
        if(manager.WalkDestination == (Vector2)manager.transform.position)
        {
            manager.SwitchState(manager.IdleState);
            return;
        }
        var walkDirection = Vector2.MoveTowards(manager.transform.position, manager.WalkDestination, ((IWalking)manager).Speed * Time.fixedDeltaTime);
        //((IWalking)manager).
        manager.Rigidbody2D.MovePosition(walkDirection);
    }

    public override void UpdateState(AttackingUnit manager)
    {
        
    }
}
