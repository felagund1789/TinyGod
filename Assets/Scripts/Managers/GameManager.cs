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
        [SerializeField] public int faith = 0;
        [SerializeField] private int maxFaith = 100;
        [SerializeField] private UIController uiController;

        public float Faith => faith;

        private void Start()
        {
            Bus<NPCSpawnEvent>.OnEvent += OnNPCSpawnEvent;
            Bus<NPCDeathEvent>.OnEvent += OnNPCDeathEvent;
            Bus<FoodProducedEvent>.OnEvent += OnFoodProducedEvent;
            Bus<FoodConsumedEvent>.OnEvent += OnFoodConsumedEvent;
            Bus<FaithGeneratedEvent>.OnEvent += OnFaithGeneratedEvent;
            Bus<FaithUsedEvent>.OnEvent += OnFaithUsedEvent;

            // Initialize UI
            uiController.UpdatePopulation(population);
            uiController.UpdateFoodSurplus(foodSurplus);
            uiController.UpdateFaith(faith, maxFaith);
        }

        private void OnDestroy()
        {
            Bus<NPCSpawnEvent>.OnEvent -= OnNPCSpawnEvent;
            Bus<NPCDeathEvent>.OnEvent -= OnNPCDeathEvent;
            Bus<FoodProducedEvent>.OnEvent -= OnFoodProducedEvent;
            Bus<FoodConsumedEvent>.OnEvent -= OnFoodConsumedEvent;
            Bus<FaithGeneratedEvent>.OnEvent -= OnFaithGeneratedEvent;
            Bus<FaithUsedEvent>.OnEvent -= OnFaithUsedEvent;
        }

        private void OnNPCSpawnEvent(NPCSpawnEvent evt) => uiController.UpdatePopulation(++population);
        private void OnNPCDeathEvent(NPCDeathEvent evt) => uiController.UpdatePopulation(--population);

        private void OnFoodProducedEvent(FoodProducedEvent evt) =>
            uiController.UpdateFoodSurplus(foodSurplus += evt.Amount);

        private void OnFoodConsumedEvent(FoodConsumedEvent evt) =>
            uiController.UpdateFoodSurplus(foodSurplus -= evt.Amount);

        private void OnFaithGeneratedEvent(FaithGeneratedEvent evt) =>
            uiController.UpdateFaith(Mathf.Clamp(faith += evt.Amount, 0, maxFaith), maxFaith);

        private void OnFaithUsedEvent(FaithUsedEvent evt) =>
            uiController.UpdateFaith(Mathf.Clamp(faith -= evt.Amount, 0, maxFaith), maxFaith);
    }
}