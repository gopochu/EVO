using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Storehouse))]
public class Mineshaft : MonoBehaviour
{
    [Header("Oil")]
    [SerializeField] private int _oilGain = 1;
    [SerializeField] private float _oilInterval = 1f;
    private float _oilTimer = 0;

    [Header("Metal")]
    [SerializeField] private int _metalGain = 1;
    [SerializeField] private float _metalInterval = 1f;
    private float _metalTimer = 0;

    [HideInInspector] public Storehouse Storehouse;

    private void Awake() 
    {
        Storehouse = GetComponent<Storehouse>();
    }

    private void Update() 
    {
        UpdateOil();
        UpdateMetal();
    }

    private void UpdateOil()
    {
        _oilTimer += Time.deltaTime;
        if(_oilTimer < _oilInterval) return;
        _oilTimer = 0;
        Storehouse.IncreaseOil(_oilGain);
    }

    private void UpdateMetal()
    {
        _metalTimer += Time.deltaTime;
        if(_metalTimer < _metalInterval) return;
        _metalTimer = 0;
        Storehouse.IncreaseMetal(_metalGain);
    }
}
