using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenuController : MonoBehaviour
{
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private Canvas pauseCanvas;

    private bool isPaused = false;

    void Start()
    {
        resumeButton.onClick.AddListener(ResumeGame);
        quitButton.onClick.AddListener(QuitToMenu);
        pauseCanvas.gameObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    private void TogglePause()
    {
        isPaused = !isPaused;
        pauseCanvas.gameObject.SetActive(isPaused);
        Time.timeScale = isPaused ? 0f : 1f;
        AudioListener.pause = isPaused;
    }

    private void ResumeGame()
    {
        TogglePause();
    }

    private void QuitToMenu()
    {
        Time.timeScale = 1f;  // Reset time scale before loading menu
        AudioListener.pause = false; // Ensure audio is unpaused when returning to menu
        // SceneTransition.LoadScene("MainMenu"); // TODO: Check if this can be used, the SceneTransition object should be added
        SceneManager.LoadScene("MainMenu");
    }

    private void OnDestroy()
    {
        // Ensure time scale is reset when scene changes
        Time.timeScale = 1f;
        AudioListener.pause = false;
    }
}