using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MovingUtils
{
    public static Vector2 TryBypass(Vector3 currentPosition, Vector3 targetPosition, float bypassAngleIncrement, float speed, int vectorDeviation, LayerMask obstacleLayer)
    {
        var movingDirection = (Vector2)(targetPosition - currentPosition).normalized;
        var supposedMovingDirection = (Vector2)(targetPosition - currentPosition).normalized;
        for(var i = 0; i < 180 / bypassAngleIncrement; i++)
        {
            var hit = Physics2D.Raycast(currentPosition, supposedMovingDirection, speed * Time.fixedDeltaTime, obstacleLayer);
            Debug.DrawRay(currentPosition, supposedMovingDirection, Color.red, 1);
            if(hit.collider == null)
            {
                movingDirection = supposedMovingDirection;
                break;
            }
            supposedMovingDirection = supposedMovingDirection.RotateVector(vectorDeviation * bypassAngleIncrement);
        }
        var movingVector = Vector2.MoveTowards(currentPosition, currentPosition + (Vector3)movingDirection * speed, speed * Time.fixedDeltaTime);
        return movingVector;
    }

    public static Vector2 TryBypass(Vector3 currentPosition, Vector3 targetPosition, float bypassAngleIncrement, float speed, int vectorDeviation, LayerMask obstacleLayer, Collider2D exception)
    {
        exception.enabled = false;
        var result = TryBypass(currentPosition, targetPosition, bypassAngleIncrement, speed, vectorDeviation, obstacleLayer);
        exception.enabled = true;
        return result;
    }

    public class AIAgent
    {
        private Vector2 _lastKnownPosition;
        private GameObject _manager;
        private float _obstacleWeight;
        private float _maxObstacleDistance;
        private float _minObstacleDistance;
        private LayerMask _obstacleLayer;
        private LayerMask _allyLayer;

        public AIAgent(GameObject manager, float obstacleWeight, float minObstacleDistance, float maxObstacleDistance, LayerMask obstacleLayer, LayerMask enemyLayer)
        {
            _manager = manager;
            _obstacleWeight = obstacleWeight;
            _minObstacleDistance = minObstacleDistance;
            _maxObstacleDistance = maxObstacleDistance;
            _obstacleLayer = obstacleLayer;
            _allyLayer = enemyLayer;
        }

        public Vector2 CalculateDirection(GameObject target)
        {
            CalculateLastKnownPosition(target);
            var targetMap = CalculateTargetMap();
            var obstacleMap = CalculateObstacleMap();
            var position = _manager.transform.position;
            var direction = Vector2.zero;
            for(var i = 0; i < 8; i++)
            {
                var vectorMultiplier = Mathf.Clamp(targetMap[i] - obstacleMap[i], 0, 1);
                direction += vectorMultiplier * Directions.EightDirections[i];
            }
            Debug.DrawRay(position, direction.normalized, Color.green, 2);
            return direction.normalized;
        }

        private float[] CalculateObstacleMap()
        {
            var obstacleMap = new float[8];
            var position = _manager.transform.position;
            var obstacleList = Physics2D.OverlapCircleAll(position, _maxObstacleDistance, _obstacleLayer);
            var enemyList = Physics2D.OverlapCircleAll(position, _maxObstacleDistance, _allyLayer);
            FillObstacleMap(obstacleMap, obstacleList);
            FillObstacleMap(obstacleMap, enemyList);

            return obstacleMap;
        }

        private void FillObstacleMap(float[] obstacleMap, Collider2D[] obstacleList)
        {
            var position = _manager.transform.position;
            foreach(var obstacle in obstacleList)
            {
                var closestPoint = obstacle.ClosestPoint(position);
                var vector = (closestPoint - (Vector2)position).normalized;
                var distance = Vector2.Distance(closestPoint, position);
                var weight = Mathf.Clamp(_obstacleWeight - 
                (distance - _minObstacleDistance) / (_maxObstacleDistance - _minObstacleDistance) * _obstacleWeight
                , 0, _obstacleWeight);
                for(var i = 0; i < 8; i++)
                {
                    obstacleMap[i] = Mathf.Max(obstacleMap[i], weight * Vector2.Dot(vector, Directions.EightDirections[i]));
                }
            }
        }
    
        private float[] CalculateTargetMap()
        {
            var targetMap = new float[8];
            var vector = (_lastKnownPosition - (Vector2)_manager.transform.position).normalized;
            for(var i = 0; i < 8; i++)
            {
                targetMap[i] = Mathf.Clamp(Vector2.Dot(vector, Directions.EightDirections[i]), 0, 1);
            }
            
            return targetMap;
        }

        private void CalculateLastKnownPosition(GameObject _target)
        {
            var position = _manager.transform.position;
            var hit = Physics2D.Raycast(position, _target.transform.position - position, Vector2.Distance( _target.transform.position, position), _obstacleLayer);
            if (hit.collider == null)
            {
                _lastKnownPosition = _target.transform.position;
            }
        }
    }
}
