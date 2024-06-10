using UnityEngine;

public class SpawnPointInfo : MonoBehaviour
{
    [SerializeField] private TargetFollower _enemy;
    [SerializeField] private PathFollower _target;   
    
    public TargetFollower Enemy { get { return _enemy; } }
    public PathFollower Target { get { return _target; } }
}
