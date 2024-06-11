using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private SpawnPointInfo[] _spawnPoints;
    [SerializeField] private Transform[] _waypoints;
    [SerializeField] private float _spawnDelay;
        
    private List<PathFollower> _targetsOnScene;

    private void Awake()
    {
        AssignLists();
        SpawnTargets();
        StartCoroutine(StartEnemySpawning());
    }
        
    private void AssignLists()
    {               
        _targetsOnScene = new List<PathFollower>() { };
    }

    private void SpawnTargets()
    {
        for (int i = 0; i < _spawnPoints.Length; i++)
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
        int maxIndex = _spawnPoints.Length;
        int index = Random.Range(minIndex, maxIndex);
        Vector3 direction = new Vector3(Random.Range(-Random.value, Random.value), 0, Random.Range(-Random.value, Random.value));

        TargetFollower enemy = Instantiate(_spawnPoints[index].Enemy, _spawnPoints[index].transform.position, Quaternion.identity);

        enemy.SetTarget(_targetsOnScene[index].transform);
    }
}
