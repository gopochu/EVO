using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuleWalkingState : MuleBaseState
{
    public override void EnterState(Mule manager)
    {
        
    }

    public override void FixedUpdateState(Mule manager)
    {
        if((Vector2)manager.transform.position == manager.WalkDestination)
        {
            manager.SwitchState(manager.IdleState);
            return;
        }
        var movingVector = Vector2.MoveTowards(manager.transform.position, manager.WalkDestination, manager.Speed * Time.fixedDeltaTime);
        manager.Rigidbody2D.MovePosition(movingVector);
    }

    public override void UpdateState(Mule manager)
    {
        
    }
}
