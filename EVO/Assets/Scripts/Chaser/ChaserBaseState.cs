using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ChaserBaseState
{
    public abstract void EnterState(Chaser manager);
    public abstract void UpdateState(Chaser manager);
    public abstract void FixedUpdateState(Chaser manager);
}
