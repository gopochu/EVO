using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBuyState : PlayerBaseState
{
    private GameObject _workingPreview;
    public override void EnterState(Player manager)
    {
        manager.ShopCanvas.SetActive(true);
        _workingPreview = Object.Instantiate(manager.CurrentPreview, manager.transform.position, Quaternion.identity);
    }

    public override void UpdateState(Player manager)
    {
        _workingPreview.transform.position = manager.GetMousePosition();
        var sprite = _workingPreview.GetComponentInChildren<SpriteRenderer>();
        var previewComponent = _workingPreview.GetComponent<UnitPreview>();
        if(previewComponent.IsValidPlacement() &&
            Storehouse.Instance.GetComponent<Storage>().OilCount >= previewComponent.OilCost &&
            Storehouse.Instance.GetComponent<Storage>().MetalCount >= previewComponent.MetalCost)
        {
                sprite.color = manager.ValidPlacementColor;
        }
        else
        {
                sprite.color = manager.InvalidPlacementColor;
        }
    }

    public override void FixedUpdateState(Player manager)
    {

    }

    public override void ExitState(Player manager)
    {
        manager.ShopCanvas.SetActive(false);
        Object.Destroy(_workingPreview);
    }

    public override void AdditionalChoose(InputAction.CallbackContext context, Player manager)
    {
        if(!context.started) return;
        var previewComponent = _workingPreview.GetComponent<UnitPreview>();
        if(previewComponent.IsValidPlacement() &&
        Storehouse.Instance.GetComponent<Storage>().OilCount >= previewComponent.OilCost &&
        Storehouse.Instance.GetComponent<Storage>().MetalCount >= previewComponent.MetalCost)
        {
            Object.Instantiate(previewComponent.Unit, manager.GetMousePosition(), Quaternion.identity);
            Storehouse.Instance.GetComponent<Storage>().DecreaseOil(previewComponent.OilCost);
            Storehouse.Instance.GetComponent<Storage>().DecreaseMetal(previewComponent.MetalCost);
        }
    }

    public override void Choose(InputAction.CallbackContext context, Player manager)
    {
        
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
        if(_workingPreview != null)
        {
            Object.Destroy(_workingPreview);
        }
        _workingPreview = Object.Instantiate(manager.CurrentPreview, manager.transform.position, Quaternion.identity);
        //Debug.Log(_workingPreview);
    }
}
