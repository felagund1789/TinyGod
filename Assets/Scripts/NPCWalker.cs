using UnityEngine;

public class NPCWalker : MonoBehaviour
{
    public float moveSpeed = 50f;
    private float _movementTime = 5f;
    private Vector3 _targetDir;

    void Start()
    {
        PickNewDirection();
    }

    void Update()
    {
        transform.RotateAround(Vector3.zero, _targetDir, moveSpeed * Time.deltaTime);
        _movementTime -= Time.deltaTime;
        if (_movementTime <= 0)
        {
            PickNewDirection();
            _movementTime = Random.Range(5f, 15f);
        }
    }

    void PickNewDirection()
    {
        _targetDir = Random.onUnitSphere;
    }
}