using System;
using EventBus;
using Events;
using UnityEngine;
using Random = UnityEngine.Random;

public class NPCWalker : AbstractSpawnable, IDestructible
{
    public float moveSpeed = 10f;
    private float _movementTime = 5f;
    private Vector3 _targetDir;

    void Start()
    {
        Bus<NPCSpawnEvent>.Raise(new NPCSpawnEvent());
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

    private void OnDestroy()
    {
        Bus<NPCDeathEvent>.Raise(new NPCDeathEvent());
    }
}