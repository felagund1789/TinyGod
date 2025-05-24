using System;
using EventBus;
using Events;
using UnityEngine;
using UnityEngine.Serialization;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private int population = 0;
        [SerializeField] private int foodSurplus = 20;
        [SerializeField] private int faith = 20;
        [SerializeField] private int maxFaith = 100;
        [SerializeField] private int happiness = 100;
        [SerializeField] private int maxHappiness = 100;
        [SerializeField] private UIController uiController;

        public float Faith => faith;
        public int Happiness => happiness;

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
            uiController.UpdateHappiness(happiness, maxHappiness);
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

        private void OnNPCDeathEvent(NPCDeathEvent evt)
        {
            uiController.UpdatePopulation(--population);
            CheckGameOver();
        }

        private void OnFoodProducedEvent(FoodProducedEvent evt)
        {
            foodSurplus += evt.Amount;
            uiController.UpdateFoodSurplus(Mathf.Max(0, foodSurplus)); // always keep food surplus non-negative

            happiness = Mathf.Clamp(maxHappiness + foodSurplus, 0, maxHappiness);
            uiController.UpdateHappiness(happiness, maxHappiness);
        }

        private void OnFoodConsumedEvent(FoodConsumedEvent evt)
        {
            foodSurplus -= evt.Amount;
            uiController.UpdateFoodSurplus(Mathf.Max(0, foodSurplus)); // always keep food surplus non-negative

            happiness = Mathf.Clamp(maxHappiness + foodSurplus, 0, maxHappiness);
            uiController.UpdateHappiness(happiness, maxHappiness);
            CheckGameOver();
        }

        private void OnFaithGeneratedEvent(FaithGeneratedEvent evt)
        {
            faith = Mathf.Min(faith + evt.Amount, maxFaith);
            uiController.UpdateFaith(faith, maxFaith);
        }

        private void OnFaithUsedEvent(FaithUsedEvent evt)
        {
            faith = Mathf.Max(0, faith - evt.Amount);
            uiController.UpdateFaith(faith, maxFaith);
        }

        private void CheckGameOver()
        {
            if (population <= 0 || happiness <= 0)
            {
                // Trigger game over logic
                PlayerPrefs.SetInt("LastGameScore", population); // Save last score
                SceneTransition.LoadScene("GameOver");
            }
        }
    }
}