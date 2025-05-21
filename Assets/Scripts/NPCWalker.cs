using System;
using EventBus;
using Events;
using Managers;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class NPCWalker : AbstractSpawnable, IDestructible
{
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float faithGenerationInterval = 5f;
    [SerializeField] private float foodConsumptionInterval = 5f;
    [SerializeField] private float reproductionInterval = 20f;
    private float _faithGenerationTimer;
    private float _foodConsumptionTimer;
    private float _reproduceTimer;
    private float _movementTimer;
    private Vector3 _targetDir;

    void Start()
    {
        _faithGenerationTimer = faithGenerationInterval;
        _foodConsumptionTimer = foodConsumptionInterval;
        _movementTimer = Random.Range(5f, 15f);
        Bus<NPCSpawnEvent>.Raise(new NPCSpawnEvent());
        PickNewDirection();
    }

    void Update()
    {

        // update timers
        _faithGenerationTimer -= Time.deltaTime;
        _movementTimer -= Time.deltaTime;
        _foodConsumptionTimer -= Time.deltaTime;
        _reproduceTimer -= Time.deltaTime;

        GenerateFaith();
        TryConsumeFood();
        TryReproduce();
        MoveAround();
    }

    private void MoveAround()
    {
        // move around
        transform.RotateAround(Vector3.zero, _targetDir, moveSpeed * Time.deltaTime);

        // check if we should pick a new direction
        if (_movementTimer <= 0)
        {
            PickNewDirection();
            _movementTimer = Random.Range(5f, 15f);
        }
    }

    private void TryReproduce()
    {
        // check if we should reproduce
        if (_reproduceTimer > 0) return;

        // spawn a new NPC with a 25% chance
        if (Random.value < 0.25f)
            Bus<NPCReproduceEvent>.Raise(new NPCReproduceEvent());

        _reproduceTimer = reproductionInterval;
    }

    private void TryConsumeFood()
    {
        // check if we should consume food
        if (_foodConsumptionTimer > 0) return;

        Bus<FoodConsumedEvent>.Raise(new FoodConsumedEvent(1));
        _foodConsumptionTimer = foodConsumptionInterval;
    }

    private void GenerateFaith()
    {
        // check if we should generate faith
        if (_faithGenerationTimer > 0) return;

        Bus<FaithGeneratedEvent>.Raise(new FaithGeneratedEvent(1));
        _faithGenerationTimer = faithGenerationInterval;
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