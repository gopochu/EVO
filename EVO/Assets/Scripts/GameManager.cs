using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private GameObject WinCanvas;
    [SerializeField] private GameObject FailCanvas;
    
    private void Awake() 
    {
        if(Instance == null)
            Instance = this;
        else
            Debug.LogError("More than one GameManager");
        
    }

    private void Start() {
        Storehouse.Instance.GetComponent<Health>().OnDeath.AddListener(ExecuteFailState);
        SpawnerManager.Instance.OnSpawnersEmpty.AddListener(ExecuteWinState);
    }

    private void ExecuteFailState()
    {
        Debug.Log("You Lost!");
        Destroy(Player.Instance);
        FailCanvas.SetActive(true);
    }

    private void ExecuteWinState()
    {
        Debug.Log("You won");
        Destroy(Player.Instance);
        WinCanvas.SetActive(true);
    }
}
