using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaserChaseState : ChaserBaseState
{
    private int _vectorDeviation;
    public override void EnterState(Chaser manager)
    {
        _vectorDeviation = Random.value < 0.5 ? -1 : 1;
    }

    public override void FixedUpdateState(Chaser manager)
    {
        if(manager.Target == null)
        {
            manager.SwitchState(manager.IdleState);
            return;
        }
        if(Vector2.Distance(manager.transform.position, manager.Target.transform.position) <= manager.DistanceToHit)
        {
            manager.SwitchState(manager.AttackState);
            return;
        }
        var currentPosition = manager.transform.position;
        var targetPosition = manager.Target.transform.position;
        var movingDirection = (Vector2)(targetPosition - currentPosition).normalized;
        float debugSum = 0;
        for(var i = 0; i < 180 / manager.BypassAngleIncrement - 1; i++)
        {
            var hit = Physics2D.Raycast(currentPosition, movingDirection, manager.Speed * Time.fixedDeltaTime, manager.EnemyLayer);
            if(hit.collider == null) break;
            movingDirection = movingDirection.RotateVector(_vectorDeviation * manager.BypassAngleIncrement);
            debugSum += _vectorDeviation * manager.BypassAngleIncrement;
        }
        var movingVector = Vector2.MoveTowards(currentPosition, currentPosition + (Vector3)movingDirection * manager.Speed, manager.Speed * Time.fixedDeltaTime);
        manager.Rigidbody2D.MovePosition(movingVector);
    }

    public override void UpdateState(Chaser manager)
    {

    }
}
