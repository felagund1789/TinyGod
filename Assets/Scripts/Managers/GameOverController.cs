using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
        if (PlayerPrefs.HasKey("LastGameScore"))
        {
            scoreText.text = $"Your civilization reached {PlayerPrefs.GetInt("LastGameScore")} population before collapsing...";
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