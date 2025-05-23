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

        void Start()
        {
            Invoke(nameof(ShowWelcomeMessage), 1f);
            Invoke(nameof(ShowRightClickToRotateMessage), 2f);
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