using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaserIdleState : ChaserBaseState
{
    private Coroutine _findCoroutine;
    public override void EnterState(Chaser manager)
    {
        manager.StartCoroutine(FindCoroutine(manager));
    }

    public override void FixedUpdateState(Chaser manager)
    {
        
    }

    public override void UpdateState(Chaser manager)
    {
        
    }

    public override void ExitState(Chaser manager)
    {

    }

    private IEnumerator FindCoroutine(Chaser manager)
    {
        while(true)
        {
            Health nearestUnit = null;
            var distance = float.PositiveInfinity;
            foreach(var unit in SpawnerManager.Instance.PlayerUnits)
            {
                var newDistance = Vector2.Distance(manager.transform.position, unit.transform.position);
                if(distance > newDistance)
                {
                    distance = newDistance;
                    nearestUnit = unit.GetComponent<Health>();
                }
            }
            if(nearestUnit != null)
            {
                manager.Target = nearestUnit;
                break;
            }
            yield return new WaitForSeconds(manager.ScanningEnemiesFrequency);
        }
        manager.SwitchState(manager.AttackState);
    }
}
