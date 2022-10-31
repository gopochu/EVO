using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuleDepositingState : MuleBaseState
{
    private float _timer;

    public override void EnterState(Mule manager)
    {
        _timer = 0;
    }

    public override void UpdateState(Mule manager)
    {
        if(_timer < manager.TimeToDeposit)
        {
            _timer += Time.deltaTime;
            return;
        }

        var targetStorehouse = manager.MainTarget.GetComponent<Storage>();

        targetStorehouse.IncreaseMetal(manager.Storage.MetalCount);
        manager.Storage.DecreaseMetal((manager.Storage.MetalCount));

        targetStorehouse.IncreaseOil(manager.Storage.OilCount);
        manager.Storage.DecreaseOil((manager.Storage.OilCount));

        manager.ToggleTarget();
        manager.SwitchState(manager.DeliveringState);
    }

    public override void FixedUpdateState(Mule manager)
    {
        
    }
}
