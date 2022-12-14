using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuleDeliveringState : MuleBaseState
{
    private Vector3 _offset = new Vector3(0, -0.01f, 0);
    public override void FixedUpdateState(Mule manager)
    {
        var targetPosition = manager.MainTarget.transform.position + _offset;
        if(manager.transform.position == targetPosition)
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
        var movingVector = Vector2.MoveTowards(currentPosition, targetPosition, manager.Speed * Time.fixedDeltaTime);
        manager.Rigidbody2D.MovePosition(movingVector);

    }

    public override void UpdateState(Mule manager)
    {
       
    }

    public override void EnterState(Mule manager)
    {
        
    }
}
