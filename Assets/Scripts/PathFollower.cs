using System.Collections.Generic;
using UnityEngine;

public class PathFollower : MonoBehaviour
{
    [SerializeField] private float _speed;

    private List<Transform> _waypoints;
    private int _currentWaypoint = 0;

    private void Update()
    {
        if (transform.position == _waypoints[_currentWaypoint].position) 
        {
            _currentWaypoint = (_currentWaypoint + 1) % _waypoints.Count;        
        }

        transform.position = Vector3.MoveTowards(transform.position, _waypoints[_currentWaypoint].position, _speed * Time.deltaTime);
        transform.LookAt(_waypoints[_currentWaypoint]);
    }

    public void AssignWaypoints(List<Transform> waypoints) 
    {
        _waypoints = waypoints;                    
    }
}
