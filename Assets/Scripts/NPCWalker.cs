using System;
using EventBus;
using Events;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class NPCWalker : AbstractSpawnable, IDestructible
{
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float faithGenerationInterval = 5f;
    private float _faithGenerationTimer = 5f;
    private float _movementTimer;
    private Vector3 _targetDir;

    void Start()
    {
        _faithGenerationTimer = faithGenerationInterval;
        _movementTimer = Random.Range(5f, 15f);
        Bus<NPCSpawnEvent>.Raise(new NPCSpawnEvent());
        PickNewDirection();
    }

    void Update()
    {
        // move around
        transform.RotateAround(Vector3.zero, _targetDir, moveSpeed * Time.deltaTime);

        // update timers
        _faithGenerationTimer -= Time.deltaTime;
        _movementTimer -= Time.deltaTime;

        // check if we should generate faith
        if (_faithGenerationTimer <= 0)
        {
            Bus<FaithGeneratedEvent>.Raise(new FaithGeneratedEvent(1));
            _faithGenerationTimer = faithGenerationInterval;
        }

        // check if we should pick a new direction
        if (_movementTimer <= 0)
        {
            PickNewDirection();
            _movementTimer = Random.Range(5f, 15f);
        }
    }

    private void PickNewDirection()
    {
        _targetDir = Random.onUnitSphere;
    }

    private void OnDestroy()
    {
        Bus<NPCDeathEvent>.Raise(new NPCDeathEvent());
    }
}