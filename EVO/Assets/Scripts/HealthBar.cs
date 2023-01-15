using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Slider _slider;
    [SerializeField] private Health _objectHealth;
    
    private void Start()
    {
        _slider = GetComponent<Slider>();
        _slider.maxValue = _objectHealth.MaxHealth;
        _slider.value = _objectHealth.MaxHealth;
        //Debug.Log(_objectHealth);
        _objectHealth.OnHealthChanged.AddListener(UpdateHealth);
    }

    public void UpdateHealth()
    {
        Debug.Log("PEnis");
        _slider.value = _objectHealth.CurrentHealth;
    }
}
