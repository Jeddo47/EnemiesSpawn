using UnityEngine;

public class EnemyLogic : MonoBehaviour
{
    [SerializeField] private float _speed;
    private Vector3 _direction;

    private void Update()
    {
        transform.Translate(_direction.normalized * _speed * Time.deltaTime);
    }

    public void SetDirection(Vector3 direction)
    {
        _direction = direction;
    }
}
