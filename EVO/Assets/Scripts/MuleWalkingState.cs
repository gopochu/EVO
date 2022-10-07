using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuleWalkingState : MuleBaseState
{
    public override void FixedUpdateState(MuleStateManager manager)
    {
        if(manager.transform.position == manager.MainTarget.transform.position)
        {
           if(manager.MainTarget == manager.BaseStorehouse)
           {
                manager.SwitchState(manager.DepositingState);
           }
           else if(manager.MainTarget == manager.Mineshaft)
           {
                manager.SwitchState(manager.CollectingState);
           }
           return;
        }

        var currentPosition = manager.transform.position;
        var targetPosition = manager.MainTarget.transform.position;
        var movingVector = Vector2.MoveTowards(currentPosition, targetPosition, manager.Speed * Time.fixedDeltaTime);
        manager.Rigidbody2D.MovePosition(movingVector);

    }

    public override void UpdateState(MuleStateManager manager)
    {
       
    }

    public override void EnterState(MuleStateManager manager)
    {
        
    }
}
