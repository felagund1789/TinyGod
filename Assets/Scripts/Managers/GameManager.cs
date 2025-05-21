using EventBus;
using Events;
using UnityEngine;
using UnityEngine.Serialization;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private int population;
        [SerializeField] private int foodSurplus;
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

        private void OnNPCSpawnEvent(NPCSpawnEvent evt) => uiController.UpdatePopulation(++population);
        private void OnNPCDeathEvent(NPCDeathEvent evt) => uiController.UpdatePopulation(--population);
        private void OnFoodProducedEvent(FoodProducedEvent evt) => uiController.UpdateFoodSurplus(foodSurplus += evt.Amount);
        private void OnFoodConsumedEvent(FoodConsumedEvent evt) => uiController.UpdateFoodSurplus(foodSurplus -= evt.Amount);
    }
}