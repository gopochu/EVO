using UnityEngine;

public class TurretAttackState : TurretBaseState
{
    public override void EnterState(Turret manager)
    {

    }

    public override void UpdateState(Turret manager)
    {
        if (manager.Target == null)
        {
            manager.SwitchState(manager.IdleState);
            return;
        }
        if (manager.CurrentAttackCooldown == 0)
        {
            manager.CurrentAttackCooldown = manager.AttackCooldown;
            Debug.Log("Attack");
        }
    }

    public override void OnCollisionEnter(Turret manager, GameObject collision)
    {
        
    }
}
