using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class PlayerBaseState
{
    public abstract void EnterState(Player manager);
    public abstract void UpdateState(Player manager);
    public abstract void FixedUpdateState(Player manager);
    public abstract void ExitState(Player manager);
    public abstract void Choose(InputAction.CallbackContext context, Player manager);
    public abstract void AdditionalChoose(InputAction.CallbackContext context, Player manager);
    public abstract void MoveCamera(InputAction.CallbackContext context, Player manager);
    public abstract void ZoomCamera(InputAction.CallbackContext context, Player manager);
    public abstract void SpeedUpCamera(InputAction.CallbackContext context, Player manager);
    public abstract void OnPreviewChanged(Player manager);
}
