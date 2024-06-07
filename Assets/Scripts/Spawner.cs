using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Transform _spawnPointA;
    [SerializeField] private Transform _spawnPointB;
    [SerializeField] private Transform _spawnPointC;
    [SerializeField] private Transform _spawnPointD;
    [SerializeField] private Transform _waypointA;
    [SerializeField] private Transform _waypointB;
    [SerializeField] private Transform _waypointC;
    [SerializeField] private Transform _waypointD;
    [SerializeField] private EnemyLogic _enemyAPrefab;
    [SerializeField] private EnemyLogic _enemyBPrefab;
    [SerializeField] private EnemyLogic _enemyCPrefab;
    [SerializeField] private EnemyLogic _enemyDPrefab;
    [SerializeField] private TargetLogic _targetAPrefab;
    [SerializeField] private TargetLogic _targetBPrefab;
    [SerializeField] private TargetLogic _targetCPrefab;
    [SerializeField] private TargetLogic _targetDPrefab;
    [SerializeField] private float _spawnDelay;

    private List<Transform> _spawnPoints;
    private List<Transform> _waypoints;
    private List<EnemyLogic> _enemies;
    private List<TargetLogic> _targetsPrefabs;
    private List<TargetLogic> _targetsOnScene;

    private void Awake()
    {
        AssignLists();
        SpawnTargets();
        StartCoroutine(StartEnemySpawning());
    }

    public List<Transform> GetWaypoints()
    {
        return _waypoints;
    }

    private void AssignLists()
    {
        _spawnPoints = new List<Transform>() { _spawnPointA, _spawnPointB, _spawnPointC, _spawnPointD };
        _waypoints = new List<Transform>() { _waypointA, _waypointB, _waypointC, _waypointD };
        _enemies = new List<EnemyLogic>() { _enemyAPrefab, _enemyBPrefab, _enemyCPrefab, _enemyDPrefab };
        _targetsPrefabs = new List<TargetLogic>() { _targetAPrefab, _targetBPrefab, _targetCPrefab, _targetDPrefab };
        _targetsOnScene = new List<TargetLogic>() { };
    }

    private void SpawnTargets()
    {
        for (int i = 0; i < _targetsPrefabs.Count; i++)
        {
            TargetLogic target = Instantiate(_targetsPrefabs[i], _spawnPoints[i].position, Quaternion.identity);

            _targetsOnScene.Add(target);
        }
    }

    private IEnumerator StartEnemySpawning()
    {
        WaitForSeconds wait = new WaitForSeconds(_spawnDelay);

        while (true)
        {
            yield return wait;

            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        int minIndex = 0;
        int maxIndex = _spawnPoints.Count;
        int index = Random.Range(minIndex, maxIndex);
        Vector3 direction = new Vector3(Random.Range(-Random.value, Random.value), 0, Random.Range(-Random.value, Random.value));

        EnemyLogic enemy = Instantiate(_enemies[index], _spawnPoints[index].position, Quaternion.identity);

        enemy.SetTarget(_targetsOnScene[index].transform);
    }
}
