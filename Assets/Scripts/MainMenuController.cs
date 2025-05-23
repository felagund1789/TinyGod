using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private GameObject planet;
    [SerializeField] private float rotationSpeed = 10f;

    void Start()
    {
        startButton.onClick.AddListener(StartGame);
        quitButton.onClick.AddListener(QuitGame);
    }

    void Update()
    {
        // Slowly rotate the planet for visual effect
        planet.transform.Rotate(Vector3.up, -rotationSpeed * Time.deltaTime);
    }

    private void StartGame()
    {
        SceneTransition.LoadScene("Game");
    }

    private void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
