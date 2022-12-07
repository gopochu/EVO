using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    
    [HideInInspector] public static SpawnerManager Instance;
    [HideInInspector] public List<Unit> PlayerUnits;
    [HideInInspector] public List<Spawner> Spawners;
    [SerializeField] private float _enemyMultiplierIncrement;
    [SerializeField] private float _enemyHealthIncrement;
    [SerializeField] private float _currentEnemyMultiplier;
    [SerializeField] private float _currentHealthMultiplier;
    [SerializeField] private int _waveDelay;
    [SerializeField] private int _firstWaveDelay;
    private Coroutine _waveCoroutine;
    
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
            _currentEnemyMultiplier += _enemyMultiplierIncrement;
            _currentHealthMultiplier += _enemyHealthIncrement;
            yield return new WaitForSeconds(_waveDelay);
        }
    }

    private void ExecuteWave()
    {
        var randomId = (int)Random.Range(0, Spawners.Count);
        if(Spawners.Count > 0)
            Spawners[randomId].Spawn(_currentEnemyMultiplier, _currentHealthMultiplier);
    }
}
