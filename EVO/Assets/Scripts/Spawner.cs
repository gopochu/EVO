using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : EnemyUnit
{
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private float _spawnDelay;

    private void Start() {
        SpawnerManager.Instance.Spawners.Add(this);
    }
    public void Spawn(float enemyNumber, int health)
    {
       StopAllCoroutines();
       StartCoroutine(SpawnCoroutine(enemyNumber, health));
    }

    private IEnumerator SpawnCoroutine(float enemyCount, int health)
    {
        for(var i = 0; i < enemyCount; i++)
        {
            var newEnemy = Instantiate(_enemyPrefab, transform.position, Quaternion.identity);
            newEnemy.GetComponent<Health>().MaxHealth = health;
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

    private void OnDestroy() {
        SpawnerManager.Instance.Spawners.Remove(this);
        SpawnerManager.Instance.CheckSpawnerCount();
        StopAllCoroutines();
    }
}
