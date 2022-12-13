using UnityEngine;

public class TurretIdleState : TurretBaseState
{
    public override void EnterState(Turret manager)
    {

    }

    public override void UpdateState(Turret manager)
    {

    }

    public override void OnCollisionEnter(Turret manager, GameObject collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            manager.Target = collision;
            manager.SwitchState(manager.AtackState);       
        }
    }
}