using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;
public class MinimapController : MonoBehaviour
{
    public static MinimapController Instance;
    public Dictionary<MinimapObjectType, List<GameObject>> MinimapObjectsDictionary;
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

    }

    public void SelectPlayerBuildings(InputAction.CallbackContext context)
    {
        if(!context.started) return;
        SelectObjectType(MinimapObjectType.PlayerBuilding);
    }

    public void SelectPlayerUnits(InputAction.CallbackContext context)
    {
        if(!context.started) return;
        SelectObjectType(MinimapObjectType.PlayerUnit);
    }

    public void SelectSpawners(InputAction.CallbackContext context)
    {
        if(!context.started) return;
        SelectObjectType(MinimapObjectType.Spawner);
    }


    private void SelectObjectType(MinimapObjectType selectedType)
    {
        foreach(var type in Enum.GetValues(typeof(MinimapObjectType)))
        {
            if((MinimapObjectType)type == selectedType)
            {
                foreach(var gameObject in MinimapObjectsDictionary[selectedType])
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
