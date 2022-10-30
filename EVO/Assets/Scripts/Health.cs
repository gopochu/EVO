using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] private int _maxHealth;
    [SerializeField] private int _currentHealth;
    [SerializeField] private UnityEvent OnDeath;
    public int CurrentHealth {get => _currentHealth;}
    
    public void DecreaseHealth(int value)
    {
        _currentHealth -= value;
        if (_currentHealth <= 0) 
            OnDeath.Invoke();
    }

    public void IncreaseHealth(int value)
    {
        _currentHealth = Mathf.Min(_currentHealth + value, _maxHealth);
    }
}
