using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTransition : MonoBehaviour
{
    [SerializeField] private Image fadePanel;
    [SerializeField] private float fadeDuration = 1f;

    private static SceneTransition instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static void LoadScene(string sceneName)
    {
        instance.StartCoroutine(instance.FadeAndLoadScene(sceneName));
    }

    private IEnumerator FadeAndLoadScene(string sceneName)
    {
        // Fade to black
        float timer = 0;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            fadePanel.color = new Color(0, 0, 0, timer / fadeDuration);
            yield return null;
        }

        // Load new scene
        SceneManager.LoadScene(sceneName);

        // Fade back in
        timer = fadeDuration;
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            fadePanel.color = new Color(0, 0, 0, timer / fadeDuration);
            yield return null;
        }
    }
}
