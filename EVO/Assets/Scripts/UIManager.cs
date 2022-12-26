using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _oilCounter;
    [SerializeField] private TextMeshProUGUI _metalCounter;
    [SerializeField] TextMeshProUGUI _healthCounter;
    [SerializeField] private Health _health;
    [SerializeField] private Storage _storage;

    private void Update() 
    {
        _oilCounter.text = _storage.OilCount.ToString();
        _metalCounter.text = _storage.MetalCount.ToString();
        _healthCounter.text = _health.CurrentHealth.ToString();

    } 
}
