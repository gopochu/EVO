using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpawnerManager : MonoBehaviour
{
    
    [HideInInspector] public static SpawnerManager Instance;
    [HideInInspector] public List<Unit> PlayerUnits;
    [HideInInspector] public List<Spawner> Spawners;
    public UnityEvent OnSpawnersEmpty;

    [Header("Health Options")]
    [SerializeField] private int _enemyHealthIncrement;
    [SerializeField] private int _healthIncrementWaveInterval;
    [SerializeField] private int _currentHealth;

    [Header("Count Options")]
    [SerializeField] private float _enemyCountIncrement;
    [SerializeField] private int _countIncrementWaveInterval;
    [SerializeField] private float _currentEnemyCount;

    [Header("Wave Options")]
    [SerializeField] private int _waveDelay;
    [SerializeField] private int _firstWaveDelay;
    private Coroutine _waveCoroutine;
    private int _waveNumber = 1;
    
    private void Awake() 
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
    }

    private void Start()
    {
        _waveCoroutine = StartCoroutine(ExecuteWaveCoroutine());
    }

    private IEnumerator ExecuteWaveCoroutine()
    {
        yield return new WaitForSeconds(_firstWaveDelay);
        while(true)
        {
            ExecuteWave();
            _waveNumber++;
            if(_waveNumber % _countIncrementWaveInterval == 0)
                _currentEnemyCount += _enemyCountIncrement;
            if(_waveNumber % _healthIncrementWaveInterval == 0)
            _currentHealth += _enemyHealthIncrement;
            yield return new WaitForSeconds(_waveDelay);
        }
    }

    private void ExecuteWave()
    {
        var randomId = (int)Random.Range(0, Spawners.Count);
        if(Spawners.Count > 0)
            Spawners[randomId].Spawn(_currentEnemyCount, _currentHealth);
    }
    
    public void CheckSpawnerCount()
    {
        if(Spawners.Count == 0)
        {

        }
    }
}
