using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static GodPowers;

public class UIController : MonoBehaviour
{
    [SerializeField] private RectTransform rainIcon;
    [SerializeField] private RectTransform fireballIcon;
    [SerializeField] private Button rainButton;
    [SerializeField] private Button fireballButton;

    [Header("Animation Settings")]
    [SerializeField] private float selectedScale = 1.2f;
    [SerializeField] private float normalScale = 1f;
    [SerializeField] private float animationSpeed = 10f;

    [Header("Message Display")]
    [SerializeField] private TextMeshProUGUI messageText;
    [SerializeField] private float messageDuration = 2f;
    [SerializeField] private Color errorColor = Color.red;
    [SerializeField] private Color normalColor = Color.white;


    private GodPowers godPowers;
    private Coroutine _messageCoroutine;

    private void Start()
    {
        godPowers = FindFirstObjectByType<GodPowers>();
        if (godPowers == null)
        {
            Debug.LogError("GodPowers not found in the scene.");
            return;
        }

        // Setup button click handlers
        rainButton.onClick.AddListener(() => godPowers.currentPower = PowerType.Rain);
        fireballButton.onClick.AddListener(() => godPowers.currentPower = PowerType.Fireball);
    }

    private void Update()
    {
        // Smoothly animate icons based on current power
        bool isRainSelected = godPowers.currentPower == PowerType.Rain;

        rainIcon.localScale = Vector3.Lerp(
            rainIcon.localScale,
            Vector3.one * (isRainSelected ? selectedScale : normalScale),
            Time.deltaTime * animationSpeed
        );

        fireballIcon.localScale = Vector3.Lerp(
            fireballIcon.localScale,
            Vector3.one * (isRainSelected ? normalScale : selectedScale),
            Time.deltaTime * animationSpeed
        );
    }

    public void ShowMessage(string message, bool isError = false)
    {
        if (_messageCoroutine != null)
            StopCoroutine(_messageCoroutine);

        _messageCoroutine = StartCoroutine(ShowMessageCoroutine(message, isError));
    }

    private IEnumerator ShowMessageCoroutine(string message, bool isError)
    {
        messageText.text = message;
        messageText.color = isError ? errorColor : normalColor;
        messageText.gameObject.SetActive(true);

        yield return new WaitForSeconds(messageDuration);

        messageText.gameObject.SetActive(false);
        _messageCoroutine = null;
    }
}