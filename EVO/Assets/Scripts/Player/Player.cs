using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;

public class Player : MonoBehaviour
{
    [HideInInspector] public static Player Instance;
    [Header("Selector Options")]
    [SerializeField] private LayerMask _clickableMask;

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

    [Header("Shop Options")]
    [SerializeField] public GameObject ShopCanvas;

    [Header("Building Options")]
    [SerializeField] public LayerMask BuildArea;
    [SerializeField] public float BuildingRadius;
    [SerializeField] private List<GameObject> _previews;
    [SerializeField] public GameObject CurrentPreview;
    [SerializeField] public Color ValidPlacementColor;
    [SerializeField] public Color InvalidPlacementColor;

    public PlayerBuyState BuyState = new PlayerBuyState();
    public PlayerOrderState OrderState = new PlayerOrderState();
    private PlayerBaseState _currentState;
    private Vector3 _startPosition;
    private Vector2 _cameraMoveDirection;
    private bool _isCameraSpedUp = false;
    public HashSet<Unit> _selectedUnits = new HashSet<Unit>();

    private void Awake() 
    {
        CurrentPreview = _previews[0];
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    private void Start() 
    {
        SwitchState(OrderState);
    }

    private void Update() 
    {
        _currentState.UpdateState(this);
    }

    private void FixedUpdate()
    {
        _currentState.FixedUpdateState(this);
    }

    private void LateUpdate() 
    {
        UpdateCamera();
    }

    public void SwitchState(PlayerBaseState state)
    {
        _currentState?.ExitState(this);
        _currentState = state;
        _currentState.EnterState(this);
    }

    public void AdditionalChoose(InputAction.CallbackContext context)
    {
        _currentState.AdditionalChoose(context, this);
    }
    public void SpeedUpCamera(InputAction.CallbackContext context)
    {
        _currentState.SpeedUpCamera(context, this);
    }
    public void Choose(InputAction.CallbackContext context)
    {
        _currentState.Choose(context, this);
    }
    public void MoveCamera(InputAction.CallbackContext context)
    {
        _currentState.MoveCamera(context, this);
    }

    public void ZoomCamera(InputAction.CallbackContext context)
    {
        _currentState.ZoomCamera(context, this);
    }

    public void OpenShop(InputAction.CallbackContext context)
    {
        if(!context.started) return;
        if(_currentState != BuyState)
        {
            SwitchState(BuyState);
        }
        else
        {
            SwitchState(OrderState);
        }

    }

    public void DefalutChoose(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            var clickedPoint = GetMousePosition();
            _startPosition = clickedPoint;
            foreach(var unit in _selectedUnits)
            {
                if(unit.HighlightArea != null) unit.HighlightArea.SetActive(false);
            }
        }
        else if(context.canceled)
        {
            _selectedUnits.Clear();
            var endPosition = GetMousePosition();
            var collider2DArray = Physics2D.OverlapAreaAll(_startPosition, endPosition, _clickableMask);
           
            foreach(var collider in collider2DArray)
            {
                Unit unitComponent;
                if(!collider.transform.parent.TryGetComponent<Unit>(out unitComponent)) continue;
                _selectedUnits.Add(unitComponent);
            }
            foreach(var unit in _selectedUnits)
            {
                if(unit.HighlightArea != null) unit.HighlightArea.SetActive(true);
            }
        }
    }

    public void DefaultAdditionalChoose(InputAction.CallbackContext context)
    {
        if(!context.performed) return;
        var clickedPoint = GetMousePosition();
        var collidersOnPoint = Physics2D.OverlapCircleAll(clickedPoint, 0f, _clickableMask);
        ITargetable target = null;
        var isDone = false;
        foreach(var collider in collidersOnPoint)
            if(collider.transform.parent.TryGetComponent<ITargetable>(out target))
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
                        if(unit.AttackOrder(target.GetGameObject().GetComponent<Health>()))
                            unitSet.Remove(unit);
                        break;
                    case Order.Deliver:
                        if(unit.DeliverOrder(target.GetGameObject().GetComponent<Mineshaft>()))
                            unitSet.Remove(unit);
                        break;
                }
        }    
    }

    public void DefaultMoveCamera(InputAction.CallbackContext context)
    {
        if(context.canceled)
        {
            _cameraMoveDirection = new Vector2(0,0);
            return;
        }
        if(!context.performed) return;
        _cameraMoveDirection = context.ReadValue<Vector2>();
    }

    public void DefaultZoomCamera(InputAction.CallbackContext context)
    {
        if(!context.performed) return;
        var zoomDirection = -context.ReadValue<Vector2>().normalized.y * _cameraZoomSpeed;
        MainCamera.orthographicSize = Mathf.Clamp(MainCamera.orthographicSize + zoomDirection, _minCameraZoom, _maxCameraZoom);
    }

    public void DefaultSpeedUpCamera(InputAction.CallbackContext context)
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
    
    public Vector3 GetMousePosition()
    {
        var point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return new Vector3(point.x, point.y, 0);
    }

    private void UpdateCamera()
    {
        if(_isCameraSpedUp)
            MainCamera.transform.Translate(_cameraMoveDirection * _cameraFastMoveSpeed * Time.deltaTime);
        else
            MainCamera.transform.Translate(_cameraMoveDirection * _cameraMoveSpeed * Time.deltaTime);
    }

    public void SetPreview(int id)
    {
        //Debug.Log(id);
        CurrentPreview = _previews[id];
        _currentState?.OnPreviewChanged(this);
        //Debug.Log(CurrentPreview);
    }
}
