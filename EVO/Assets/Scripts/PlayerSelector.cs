using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;

public class PlayerSelector : MonoBehaviour
{
    [Header("Camera Options")]
    [SerializeField] public Camera MainCamera;
    [SerializeField] private float _cameraMoveSpeed;
    [SerializeField] private float _cameraFastMoveSpeed;
    [SerializeField] private float _cameraZoomSpeed;
    [SerializeField] private float _minCameraZoom;
    [SerializeField] private float _maxCameraZoom;

    [Header("Walking Order")]
    [SerializeField] private float _radiusIncrement = 3;
    [SerializeField] private int _positionsIncrement = 5;
    private Vector3 _startPosition;
    private Vector2 _cameraMoveDirection;
    private bool _isCameraSpedUp = false;
    public HashSet<Unit> _selectedUnits = new HashSet<Unit>();

    private void LateUpdate() 
    {
        UpdateCamera();
        //Debug.Log(_isCameraSpedUp);
    }

    public void Choose(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            var clickedPoint = GetMousePosition();
            _startPosition = clickedPoint;
        }
        else if(context.canceled)
        {
            _selectedUnits.Clear();
            var endPosition = GetMousePosition();
            var collider2DArray = Physics2D.OverlapAreaAll(_startPosition, endPosition);
            foreach(var collider in collider2DArray)
            {
                Unit unitComponent;
                if(!collider.gameObject.TryGetComponent<Unit>(out unitComponent)) continue;
                _selectedUnits.Add(unitComponent);
            }
        }
    }

    public void AdditionalChoose(InputAction.CallbackContext context)
    {
        if(!context.performed) return;
        var clickedPoint = GetMousePosition();
        var collidersOnPoint = Physics2D.OverlapCircleAll(clickedPoint, 0f);
        ITargetable target = null;
        var isDone = false;
        foreach(var collider in collidersOnPoint)
            if(collider.gameObject.TryGetComponent<ITargetable>(out target))
            {
                isDone = true;
                break;
            }
        if(!isDone)
        {
            HandleWalkOrder(clickedPoint, _selectedUnits.ToList());
            return;
        }
        var unitSet = new HashSet<Unit>(_selectedUnits);
        foreach(var order in target.GetOrderPriority())
        {
            Debug.Log(order);
            foreach(var unit in _selectedUnits)
                switch(order)
                {
                    case Order.Follow:
                        if(unit.FollowOrder(target.GetGameObject()))
                            unitSet.Remove(unit);
                        break;
                    case Order.Attack:
                        if(unit.AttackOrder(target.GetGameObject()))
                            unitSet.Remove(unit);
                        break;
                    case Order.Deliver:
                        if(unit.DeliverOrder(target.GetGameObject().GetComponent<Mineshaft>()))
                            unitSet.Remove(unit);
                        break;
                    case Order.ToggleElectricity:
                        if(unit.ToggleElectricityOrder())
                            unitSet.Remove(unit);
                        break;
                }
        }    
    }

    public void MoveCamera(InputAction.CallbackContext context)
    {
        if(context.canceled)
        {
            _cameraMoveDirection = new Vector2(0,0);
            return;
        }
        if(!context.performed) return;
        _cameraMoveDirection = context.ReadValue<Vector2>();
    }

    public void ZoomCamera(InputAction.CallbackContext context)
    {
        if(!context.performed) return;
        var zoomDirection = -context.ReadValue<Vector2>().normalized.y * _cameraZoomSpeed;
        MainCamera.orthographicSize = Mathf.Clamp(MainCamera.orthographicSize + zoomDirection, _minCameraZoom, _maxCameraZoom);
    }

    public void SpeedUpCamera(InputAction.CallbackContext context)
    {
        if(context.canceled)
            _isCameraSpedUp = false;
        if(context.performed)
            _isCameraSpedUp = true;
    }

    private void HandleWalkOrder(Vector3 clickedPoint, List<Unit> selectedList)
    {
        var positionList = GetWalkPositions().Take(_selectedUnits.Count).ToList();
        var unitI = 0;
        var positionI = 0;
        while(unitI < positionList.Count)
        {
            if(selectedList[unitI].WalkOrder((Vector2)clickedPoint + positionList[positionI]))
            {
                positionI += 1;
            }
            unitI += 1;
        }
    }

    private IEnumerable<Vector2> GetWalkPositions()
    {
        var currentRadius = 0f;
        var currentPositions = 1;
        while(true)
        {
            var positions = GetPositionsInCircle(currentRadius, currentPositions);
            foreach(var position in positions)
            {
                yield return position; 
            }
            currentRadius += _radiusIncrement;
            currentPositions += _positionsIncrement;
        } 
    }

    private List<Vector2> GetPositionsInCircle(float radius, int positionsCount)
    {
        var positionList = new List<Vector2>();
        var angleIncrement = (float)360 / positionsCount;
        var currentAngle = 0f;
        for(var i = 0; i < positionsCount; i++)
        {
            positionList.Add((new Vector2(1, 0) * radius).RotateVector(currentAngle));
            currentAngle += angleIncrement;
        }
        return positionList;
    }
    
    private Vector3 GetMousePosition()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void UpdateCamera()
    {
        if(_isCameraSpedUp)
            MainCamera.transform.Translate(_cameraMoveDirection * _cameraFastMoveSpeed * Time.deltaTime);
        else
            MainCamera.transform.Translate(_cameraMoveDirection * _cameraMoveSpeed * Time.deltaTime);
    }
}
