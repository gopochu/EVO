using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    private void Awake() 
    {
        if(Instance == null)
            Instance = this;
        else
            Debug.LogError("More than one GameManager");
        
    }

    private void Update() 
    {
        if(Storehouse.Instance == null)
        {
            ExecuteFailState();
        }
        else if(SpawnerManager.Instance.Spawners.Count == 0)
        {
            ExecuteWinState();
        }
    }

    private void ExecuteFailState()
    {
        Debug.Log("You Lost!");
    }

    private void ExecuteWinState()
    {
        Debug.Log("You won");
    }
}
