using System;
using EventBus;
using Events;
using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    [field: SerializeField] public int Population { get; private set; }
    [field: SerializeField] public int FoodSurplus { get; private set; }
    [SerializeField] private UIController uiController;

    private void Start()
    {
        Bus<NPCSpawnEvent>.OnEvent += OnNPCSpawnEvent;
        Bus<NPCDeathEvent>.OnEvent += OnNPCDeathEvent;
        Bus<FoodProducedEvent>.OnEvent += OnFoodProducedEvent;
        Bus<FoodConsumedEvent>.OnEvent += OnFoodConsumedEvent;
    }

    private void OnDestroy()
    {
        Bus<NPCSpawnEvent>.OnEvent -= OnNPCSpawnEvent;
        Bus<NPCDeathEvent>.OnEvent -= OnNPCDeathEvent;
        Bus<FoodProducedEvent>.OnEvent -= OnFoodProducedEvent;
        Bus<FoodConsumedEvent>.OnEvent -= OnFoodConsumedEvent;
    }

    private void OnNPCSpawnEvent(NPCSpawnEvent evt) => uiController.UpdatePopulation(++Population);
    private void OnNPCDeathEvent(NPCDeathEvent evt) => uiController.UpdatePopulation(--Population);
    private void OnFoodProducedEvent(FoodProducedEvent evt) => uiController.UpdateFoodSurplus(FoodSurplus += evt.Amount);
    private void OnFoodConsumedEvent(FoodConsumedEvent evt) => uiController.UpdateFoodSurplus(FoodSurplus -= evt.Amount);
}