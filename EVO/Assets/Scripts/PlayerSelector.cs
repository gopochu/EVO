using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;

public class PlayerSelector : MonoBehaviour
{
    [Header("WalkingOrder")]
    [SerializeField] private float _radiusIncrement = 3;
    [SerializeField] private int _positionsIncrement = 5;
    private Vector3 _startPosition;
    public HashSet<Unit> _selectedUnits = new HashSet<Unit>();

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
        Unit target = null;
        var isDone = false;
        foreach(var collider in collidersOnPoint)
            if(collider.gameObject.TryGetComponent<Unit>(out target))
            {
                isDone = true;
                break;
            }
        if(!isDone)
        {
            HandleWalkOrder(clickedPoint, _selectedUnits.ToList());
            return;
        }
        Debug.Log(target);
        var unitSet = new HashSet<Unit>(_selectedUnits);
        foreach(var order in target.OrderPriority)
        {
            Debug.Log(order);
            var unitsLeft = unitSet.ToList();
            foreach(var unit in unitsLeft)
                switch(order)
                {
                    case Order.Follow:
                        if(unit.FollowOrder(target.gameObject))
                            unitSet.Remove(unit);
                        break;
                    case Order.Attack:
                        if(unit.AttackOrder(target.gameObject))
                            unitSet.Remove(unit);
                        break;
                    case Order.Deliver:
                        if(unit.DeliverOrder(target.GetComponent<Mineshaft>()))
                            unitSet.Remove(unit);
                        break;
                    case Order.ToggleElectricity:
                        if(unit.ToggleElectricityOrder())
                            unitSet.Remove(unit);
                        break;
                }
        }
        
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
        var angleIncrement = 360 / positionsCount;
        var currentAngle = 0;
        for(var i = 0; i < positionsCount; i++)
        {
            positionList.Add(RotateVector(new Vector2(1, 0) * radius, currentAngle));
            currentAngle += angleIncrement;
        }
        return positionList;
    }

    private Vector2 RotateVector(Vector2 vector, float angle)
    {
        return Quaternion.Euler(0,0,angle) * vector;
    }

    private Vector3 GetMousePosition()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
}
