using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaserChaseState : ChaserBaseState
{
    public override void EnterState(Chaser manager)
    {
        
    }

    public override void FixedUpdateState(Chaser manager)
    {
        if(Vector2.Distance(manager.transform.position, manager.Target.transform.position) <= manager.DistanceToHit)
        {
            manager.SwitchState(manager.AttackState);
            return;
        }
        var currentPosition = manager.transform.position;
        var targetPosition = manager.Target.transform.position;
        var movingVector = Vector2.MoveTowards(currentPosition, targetPosition, manager.Speed * Time.fixedDeltaTime);
        manager.Rigidbody2D.MovePosition(movingVector);
    }

    public override void UpdateState(Chaser manager)
    {

    }
}
