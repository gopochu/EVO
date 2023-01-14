using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;
public class MinimapController : MonoBehaviour
{
    public static MinimapController Instance;
    public GameObject MinimapCamera;
    public Dictionary<MinimapObjectType, List<GameObject>> MinimapObjectsDictionary;
    public float MinimapSize;

    public MinimapObjectType CurrentObjectType;
    private void Awake() 
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("Two instances of MinimapController!");
        }
        MinimapObjectsDictionary = new Dictionary<MinimapObjectType, List<GameObject>>();
        foreach(var type in Enum.GetValues(typeof(MinimapObjectType)))
            MinimapObjectsDictionary[(MinimapObjectType)type] = new List<GameObject>();
        CurrentObjectType = MinimapObjectType.Spawner;
    }

    private void Start() 
    {
        UpdateMinimapObjectType();
    }
    public void SelectPlayerBuildings(InputAction.CallbackContext context)
    {
        if(!context.started) return;
        CurrentObjectType = MinimapObjectType.PlayerBuilding;
        UpdateMinimapObjectType();
    }

    public void SelectPlayerUnits(InputAction.CallbackContext context)
    {
        if(!context.started) return;
        CurrentObjectType = MinimapObjectType.PlayerUnit;
        UpdateMinimapObjectType();
    }

    public void SelectSpawners(InputAction.CallbackContext context)
    {
        if(!context.started) return;
        CurrentObjectType = MinimapObjectType.Spawner;
        UpdateMinimapObjectType();
    }


    private void UpdateMinimapObjectType()
    {
        foreach(var type in Enum.GetValues(typeof(MinimapObjectType)))
        {
            if((MinimapObjectType)type == CurrentObjectType)
            {
                foreach(var gameObject in MinimapObjectsDictionary[CurrentObjectType])
                {
                    gameObject.SetActive(true);
                }
            }
            else
            {
                foreach(var gameObject in MinimapObjectsDictionary[(MinimapObjectType)type])
                {
                    gameObject.SetActive(false);
                }
            }
            
        }
    }
}
