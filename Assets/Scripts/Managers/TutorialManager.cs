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
        [SerializeField] private GameObject tutorialBackgroundPanel;
        [SerializeField] private RectTransform tutorialMessagePanel;
        [SerializeField] private TextMeshProUGUI tutorialMessageText;
        private bool _faithMessageShown = false;
        private bool _rainMessageShown = false;
        private bool _npcReproduceMessageShown = false;
        private bool _foodProducedMessageShown = false;

        private void Awake()
        {
            // Subscribe to events
            Bus<FaithUsedEvent>.OnEvent += OnFaithUsedEvent;
            Bus<RainEvent>.OnEvent += OnRainEvent;
            Bus<NPCReproduceEvent>.OnEvent += OnNPCReproduceEvent;
            Bus<FoodProducedEvent>.OnEvent += OnFoodProducedEvent;
        }

        void Start()
        {
            Invoke(nameof(ShowWelcomeMessage), 1f);
            Invoke(nameof(ShowRightClickToRotateMessage), 2f);
        }
        
        private void OnDestroy()
        {
            Bus<FaithUsedEvent>.OnEvent -= OnFaithUsedEvent;
            Bus<RainEvent>.OnEvent -= OnRainEvent;
            Bus<NPCReproduceEvent>.OnEvent -= OnNPCReproduceEvent;
            Bus<FoodProducedEvent>.OnEvent -= OnFoodProducedEvent;
        }

        private void OnFaithUsedEvent(FaithUsedEvent args)
        {
            if (_faithMessageShown) return;
            _faithMessageShown = true;
            ShowMessage(@"Great! You have used some of your faith to use your powers.

You can switch between powers by pressing 1 for the Rain and 2 for the Fireball, or by clicking on the icons in the UI.");
        }

        private void OnRainEvent(RainEvent args)
        {
            if (_rainMessageShown) return;
            _rainMessageShown = true;
            ShowMessage(@"Rain is a powerful tool! 

If one or more farms are within the radius of the rain, they will be watered and produce food to feed your followers. 

Otherwise, a small forest will be created where the rain fell.");
        }

        private void OnNPCReproduceEvent(NPCReproduceEvent args)
        {
            if (_npcReproduceMessageShown) return;
            _npcReproduceMessageShown = true;
            ShowMessage(@"Every sometime, your followers will reproduce and create new followers.

More followers means more faith is generated. But followers consume food, so make sure you have enough food to feed them all!");
        }

        private void OnFoodProducedEvent(FoodProducedEvent args)
        {
            if (_foodProducedMessageShown) return;
            _foodProducedMessageShown = true;
            ShowMessage(@"Your farms produce food which your followers will consume.

If there is not enough food, they will become unhappy and their faith in you will dwindle.");

            // Show the message for 5 seconds
            Invoke(nameof(CloseTutorial), 5f);
        }

        private void ShowWelcomeMessage() =>
            ShowMessage(@"Welcome to Tiny God!

You are a god who watches over a tiny planet with tiny followers.
Use your powers to help them grow and thrive.");

        private void ShowRightClickToRotateMessage() =>
            ShowMessage(@"You can right-click and move the mouse to rotate the planet.

Left click on the planet to use the selected power.");

        private void ShowMessage(string message)
        {
            Time.timeScale = 0;
            tutorialMessageText.SetText(message);
            // tutorialMessagePanel.sizeDelta = tutorialMessageText.GetPreferredValues();
            tutorialBackgroundPanel.SetActive(true);
        }

        public void CloseTutorial()
        {
            tutorialBackgroundPanel.SetActive(false);
            Time.timeScale = 1;
        }
    }
}