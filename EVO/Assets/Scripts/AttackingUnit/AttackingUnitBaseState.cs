using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackingUnitBaseState
{
    public abstract void EnterState(AttackingUnit manager);
    public abstract void UpdateState(AttackingUnit manager);
    public abstract void FixedUpdateState(AttackingUnit manager);
    public abstract void ExitState(AttackingUnit manager);
}
