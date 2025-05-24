using System;
using EventBus;
using Events;
using TMPro;
using UnityEngine;

namespace Managers
{
    public class TutorialManager : MonoBehaviour
    {
        [Header("UI Elements")]
        [SerializeField] private GameObject welcomeMessagePanel;
        [SerializeField] private GameObject powersExplanationPanel;
        [SerializeField] private GameObject tutorialMessagePanel;
        [SerializeField] private TextMeshProUGUI tutorialMessageText;
        private bool _npcReproduceMessageShown = false;
        private bool _foodProducedMessageShown = false;

        private void Awake()
        {
            // Subscribe to events
            Bus<NPCReproduceEvent>.OnEvent += OnNPCReproduceEvent;
            Bus<FoodProducedEvent>.OnEvent += OnFoodProducedEvent;
        }

        void Start()
        {
            Invoke(nameof(ShowWelcomeMessage), 1f);
            Invoke(nameof(ShowPowersExplainer), 1.1f);
            Invoke(nameof(ShowRightClickToRotateMessage), 1.2f);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                QuitTutorial();
            }
        }

        private void OnDestroy()
        {
            Bus<NPCReproduceEvent>.OnEvent -= OnNPCReproduceEvent;
            Bus<FoodProducedEvent>.OnEvent -= OnFoodProducedEvent;
        }

        private void OnNPCReproduceEvent(NPCReproduceEvent args)
        {
            if (_npcReproduceMessageShown) return;
            _npcReproduceMessageShown = true;
            ShowMessage("Every sometime, your followers will reproduce and create new followers. " +
                        "More followers means more faith is generated. But followers consume food, " +
                        "so make sure you have enough food to feed them all!");
        }

        private void OnFoodProducedEvent(FoodProducedEvent args)
        {
            if (_foodProducedMessageShown) return;
            _foodProducedMessageShown = true;
            ShowMessage("Your farms produce food which your followers will consume. " +
                        "If there is not enough food, they will become unhappy and their faith in you will dwindle.");

            // Show the message for 5 seconds
            Invoke(nameof(CloseTutorial), 5f);
        }

        private void ShowWelcomeMessage()
        {
            Time.timeScale = 0;
            welcomeMessagePanel.SetActive(true);
        }

        private void ShowPowersExplainer()
        {
            Time.timeScale = 0;
            powersExplanationPanel.SetActive(true);
        }

        private void ShowRightClickToRotateMessage() =>
            ShowMessage("You can right-click and move the mouse to rotate the planet. " +
                        "Left click on the planet to use the selected power.");

        private void ShowMessage(string message)
        {
            Time.timeScale = 0;
            tutorialMessageText.SetText(message);
            tutorialMessagePanel.gameObject.SetActive(true);
        }

        public void CloseWelcomeMessage()
        {
            welcomeMessagePanel.SetActive(false);
            Time.timeScale = 1;
        }

        public void ClosePowersExplainer()
        {
            powersExplanationPanel.SetActive(false);
            Time.timeScale = 1;
        }

        public void CloseTutorial()
        {
            tutorialMessagePanel.gameObject.SetActive(false);
            Time.timeScale = 1;
        }

        private void QuitTutorial()
        {
            if (Time.timeScale == 0)
            {
                // If the game is paused and the tutorial is closed, unpause the game
                Time.timeScale = 1;
            }
            Destroy(gameObject);
        }
    }
}