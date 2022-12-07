using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerOrderState : PlayerBaseState
{
   public override void EnterState(Player manager)
    {
        
    }

    public override void UpdateState(Player manager)
    {
       
    }

    public override void FixedUpdateState(Player manager)
    {

    }

    public override void ExitState(Player manager)
    {
        
    }

    public override void AdditionalChoose(InputAction.CallbackContext context, Player manager)
    {
        manager.DefaultAdditionalChoose(context);
    }

    public override void Choose(InputAction.CallbackContext context, Player manager)
    {
        manager.DefalutChoose(context);
    }

    public override void MoveCamera(InputAction.CallbackContext context, Player manager)
    {
        manager.DefaultMoveCamera(context);
    }

    public override void SpeedUpCamera(InputAction.CallbackContext context, Player manager)
    {
        manager.DefaultSpeedUpCamera(context);   
    }

    public override void ZoomCamera(InputAction.CallbackContext context, Player manager)
    {
        manager.DefaultZoomCamera(context);
    }

    public override void OnPreviewChanged(Player manager)
    {
        throw new System.NotImplementedException();
    }
}
