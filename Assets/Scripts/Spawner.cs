using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Transform _spawnPointA;
    [SerializeField] private Transform _spawnPointB;
    [SerializeField] private Transform _spawnPointC;
    [SerializeField] private Transform _spawnPointD;
    [SerializeField] private EnemyLogic _enemyPrefab;
    [SerializeField] private float _spawnDelay;
    private List<Transform> _spawnPoints;

    private void Awake()
    {
        _spawnPoints = new List<Transform>() { _spawnPointA, _spawnPointB, _spawnPointC, _spawnPointD };

        StartCoroutine(StartSpawning());
    }

    private IEnumerator StartSpawning()
    {
        WaitForSeconds wait = new WaitForSeconds(_spawnDelay);

        while (true)
        {
            SpawnEnemy();

            yield return wait;
        }
    }

    private void SpawnEnemy()
    {
        int minIndex = 0;
        int maxIndex = _spawnPoints.Count;
        Vector3 direction = new Vector3(Random.Range(-Random.value, Random.value), 0, Random.Range(-Random.value, Random.value));

        EnemyLogic enemy = Instantiate(_enemyPrefab, _spawnPoints[Random.Range(minIndex, maxIndex)].position, Quaternion.identity);               

        enemy.SetDirection(direction);
    }
}
