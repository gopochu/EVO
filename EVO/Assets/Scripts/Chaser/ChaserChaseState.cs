using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaserChaseState : ChaserBaseState
{
    private MovingUtils.AIAgent _agent;
    private float _currentTimer;
    private Vector2 _moveDirection;
    public override void EnterState(Chaser manager)
    {
        if(manager.Target == null)
        {
            manager.SwitchState(manager.IdleState);
            return;
        }
        _currentTimer = manager.DirectionUpdateFrequency;
        if(_agent == null)
        {
            _agent = new MovingUtils.AIAgent(manager.gameObject,
             manager.ObstacleWeight, manager.MinObstacleDistance, manager.MaxObstacleDistance, 
             manager.ObstacleLayer, manager.AllyLayer);
        }
        _moveDirection = _agent.CalculateDirection(manager.Target.gameObject);
    }

    public override void FixedUpdateState(Chaser manager)
    {
        if(manager.Target == null)
        {
            manager.SwitchState(manager.IdleState);
            return;
        }
        if(Vector2.Distance(manager.transform.position, manager.Target.transform.position) <= manager.DistanceToHit + manager.Target.AgentRadius)
        {
            manager.SwitchState(manager.AttackState);
            return;
        }
        manager.Rigidbody2D.MovePosition((Vector2)manager.transform.position + _moveDirection * manager.Speed * Time.fixedDeltaTime);
    }

    public override void UpdateState(Chaser manager)
    {
        if(manager.Target == null)
        {
            manager.SwitchState(manager.IdleState);
            return;
        }
        if(_currentTimer == 0)
        {
            _moveDirection = _agent.CalculateDirection(manager.Target.gameObject);
            _currentTimer = manager.DirectionUpdateFrequency;
            return;
        }
        _currentTimer = Mathf.Max(0, _currentTimer - Time.deltaTime);
    }
    
    public override void ExitState(Chaser manager)
    {
        
    }
}
