using UnityEngine;

public abstract class TurretBaseState 
{
    public abstract void EnterState(Turret manager);

    public abstract void UpdateState(Turret manager);

    public abstract void OnCollisionEnter(Turret manager, GameObject collision);
}
