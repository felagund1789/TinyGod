using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Managers
{
    public class GameOverController : MonoBehaviour
    {
        [SerializeField] private Button restartButton;
        [SerializeField] private Button quitButton;
        [SerializeField] private TextMeshProUGUI scoreText;

        void Start()
        {
            restartButton.onClick.AddListener(RestartGame);
            quitButton.onClick.AddListener(QuitToMenu);

            // Display final score/stats
            if (PlayerPrefs.HasKey("YearReached"))
            {
                scoreText.text = $"You survived until year {PlayerPrefs.GetInt("YearReached")}.";
            }

            if (PlayerPrefs.HasKey("MaxPopulationReached"))
            {
                scoreText.text += $"\nYour civilization reached population of {PlayerPrefs.GetInt("MaxPopulationReached")} before collapsing...";
            }
        }

        private void RestartGame()
        {
            SceneTransition.LoadScene("Game");
        }

        private void QuitToMenu()
        {
            SceneTransition.LoadScene("MainMenu");
        }
    }
}