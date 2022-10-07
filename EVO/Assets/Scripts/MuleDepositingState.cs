using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuleDepositingState : MuleBaseState
{
    private float _timer;
    public override void EnterState(MuleStateManager manager)
    {
        _timer = 0;
    }

    public override void UpdateState(MuleStateManager manager)
    {
        if(_timer < manager.TimeToDeposit)
        {
            _timer += Time.deltaTime;
            return;
        }

        var targetStorehouse = manager.MainTarget.GetComponent<Storehouse>();

        targetStorehouse.IncreaseMetal(manager.Storehouse.MetalCount);
        manager.Storehouse.DecreaseMetal((manager.Storehouse.MetalCount));

        targetStorehouse.IncreaseOil(manager.Storehouse.OilCount);
        manager.Storehouse.DecreaseOil((manager.Storehouse.OilCount));

        manager.ToggleTarget();
        manager.SwitchState(manager.WalkingState);
    }

    public override void FixedUpdateState(MuleStateManager manager)
    {
        
    }
}