using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuleCollectingState : MuleBaseState
{
    private float _timer;
    
    public override void EnterState(Mule manager)
    {
        _timer = 0;
    }
    
    public override void UpdateState(Mule manager)
    {
        if(_timer < manager.TimeToCollect)
        {
            _timer += Time.deltaTime;
            return;
        }

        var storehouse = manager.MainTarget.GetComponent<Storage>();
        
        manager.Storehouse.IncreaseMetal(storehouse.MetalCount);
        storehouse.DecreaseMetal(storehouse.MetalCount);
        
        manager.Storehouse.IncreaseOil(storehouse.OilCount);
        storehouse.DecreaseOil(storehouse.OilCount);

        manager.ToggleTarget();
        manager.SwitchState(manager.DeliveringState);
    }

    public override void FixedUpdateState(Mule manager)
    {
        
    }

}
