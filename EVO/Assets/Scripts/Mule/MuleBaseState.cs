using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MuleBaseState
{
    public abstract void EnterState(Mule manager);

    public abstract void FixedUpdateState(Mule manager);
    
    public abstract void UpdateState(Mule manager);
    
}
