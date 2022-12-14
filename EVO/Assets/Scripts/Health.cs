using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] public int MaxHealth;
    [SerializeField] private int _currentHealth;
    [SerializeField] public float AgentRadius;
    [SerializeField] public UnityEvent OnDeath;
    [SerializeField] public UnityEvent<GameObject> OnAttack;

    public int CurrentHealth {get => _currentHealth;}
    
    public void SetHealth(int value)
    {
        _currentHealth = Mathf.Min(value, MaxHealth);
        if (_currentHealth <= 0) 
            OnDeath.Invoke();
    }

    public void SetHealth(int value, GameObject attacker)
    {
        Debug.Log(attacker);
        if (value < _currentHealth)
            OnAttack.Invoke(attacker);
        SetHealth(value);
    }
}
