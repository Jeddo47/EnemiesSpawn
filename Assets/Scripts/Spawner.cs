using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private SpawnPointInfo _spawnPointA;
    [SerializeField] private SpawnPointInfo _spawnPointB;
    [SerializeField] private SpawnPointInfo _spawnPointC;
    [SerializeField] private SpawnPointInfo _spawnPointD;
    [SerializeField] private Transform _waypointA;
    [SerializeField] private Transform _waypointB;
    [SerializeField] private Transform _waypointC;
    [SerializeField] private Transform _waypointD;
    [SerializeField] private float _spawnDelay;

    private List<SpawnPointInfo> _spawnPoints;
    private List<Transform> _waypoints;
    private List<PathFollower> _targetsOnScene;

    private void Awake()
    {
        AssignLists();
        SpawnTargets();
        StartCoroutine(StartEnemySpawning());
    }

    public List<Transform> GetWaypoints()
    {
        List<Transform> waypointsClone = _waypoints;
        
        return waypointsClone;
    }

    private void AssignLists()
    {
        _spawnPoints = new List<SpawnPointInfo>() { _spawnPointA, _spawnPointB, _spawnPointC, _spawnPointD };
        _waypoints = new List<Transform>() { _waypointA, _waypointB, _waypointC, _waypointD };        
        _targetsOnScene = new List<PathFollower>() { };
    }

    private void SpawnTargets()
    {
        for (int i = 0; i < _spawnPoints.Count; i++)
        {
            PathFollower target = Instantiate(_spawnPoints[i].Target, _spawnPoints[i].transform.position, Quaternion.identity);
            target.AssignWaypoints(new List<Transform> (_waypoints) );
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

        TargetFollower enemy = Instantiate(_spawnPoints[index].Enemy, _spawnPoints[index].transform.position, Quaternion.identity);

        enemy.SetTarget(_targetsOnScene[index].transform);
    }
}
