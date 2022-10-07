using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MuleBaseState
{
    public abstract void EnterState(MuleStateManager manager);

    public abstract void FixedUpdateState(MuleStateManager manager);
    
    public abstract void UpdateState(MuleStateManager manager);
    
}
