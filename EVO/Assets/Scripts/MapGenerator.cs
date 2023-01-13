using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class MapObjectInfo
{
    public GameObject MapObject;
    public int Occurencies;
    public float MinRadius;
    public float MaxRadius;
}

public class MapGenerator : MonoBehaviour
{

    public List<MapObjectInfo> Objects;
    
    private void Start() 
    {
        foreach(var mapObjectInfo in Objects)
        {
            for(var i = 0; i < mapObjectInfo.Occurencies; i++)
            {
                var newPosition = UnityEngine.Random.insideUnitCircle * mapObjectInfo.MaxRadius;
                while(true)
                {
                    if(Vector2.Distance(newPosition, Vector2.zero) >= mapObjectInfo.MinRadius)
                    {
                        break;
                    }
                    newPosition = UnityEngine.Random.insideUnitCircle * mapObjectInfo.MaxRadius;
                }
                Debug.Log(newPosition);
                Instantiate(mapObjectInfo.MapObject, newPosition, Quaternion.identity);
            }
        }
    }
}
