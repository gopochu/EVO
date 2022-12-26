using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : EnemyUnit
{
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private float _spawnDelay;
    [SerializeField] private int _startEnemyNumber;

    private void Start() {
        SpawnerManager.Instance.Spawners.Add(this);
    }
    public void Spawn(float enemyNumberMultiplier, float healthMultiplier)
    {
       StopAllCoroutines();
       StartCoroutine(SpawnCoroutine(enemyNumberMultiplier, healthMultiplier));
    }

    private IEnumerator SpawnCoroutine(float enemyNumberMultiplier, float healthMultiplier)
    {
        for(var i = 0; i < _startEnemyNumber * enemyNumberMultiplier; i++)
        {
            var newEnemy = Instantiate(_enemyPrefab, transform.position, Quaternion.identity);
            newEnemy.GetComponent<Health>().MaxHealth = (int) (newEnemy.GetComponent<Health>().MaxHealth * healthMultiplier);
            newEnemy.GetComponent<Health>().SetHealth(newEnemy.GetComponent<Health>().MaxHealth);
            var randomId = (int) Random.Range(0, SpawnerManager.Instance.PlayerUnits.Count);
            newEnemy.GetComponent<EnemyUnit>().Target = SpawnerManager.Instance.PlayerUnits[randomId].GetComponent<Health>();
            yield return new WaitForSeconds(_spawnDelay);
        }
    }

    public void HandleDeath()
    {
        Destroy(this.gameObject);
    }
}
