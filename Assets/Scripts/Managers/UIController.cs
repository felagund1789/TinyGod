using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static GodPowers;

namespace Managers
{
    public class UIController : MonoBehaviour
    {
        [Header("UI Elements")]
        [SerializeField] private TextMeshProUGUI populationText;
        [SerializeField] private TextMeshProUGUI foodSurplusText;
        [SerializeField] private TextMeshProUGUI currentYearText;
        [SerializeField] private Image faithBarImage;
        [SerializeField] private Image happinessBarImage;
        [SerializeField] private RectTransform rainIcon;
        [SerializeField] private RectTransform fireballIcon;
        [SerializeField] private RectTransform lightningIcon;
        [SerializeField] private Button rainButton;
        [SerializeField] private Button fireballButton;
        [SerializeField] private Button lightningButton;

        [Header("Animation Settings")]
        [SerializeField] private float selectedScale = 1.2f;
        [SerializeField] private float normalScale = 1f;
        [SerializeField] private float animationSpeed = 10f;

        [Header("Message Display")]
        [SerializeField] private TextMeshProUGUI messageText;
        [SerializeField] private float messageDuration = 2f;
        [SerializeField] private Color errorColor = Color.red;
        [SerializeField] private Color normalColor = Color.white;


        private GodPowers _godPowers;
        private Coroutine _messageCoroutine;
        private float _beginningOfTime;

        private void Start()
        {
            _beginningOfTime = Time.time;
            _godPowers = FindFirstObjectByType<GodPowers>();
            if (_godPowers == null)
            {
                Debug.LogError("GodPowers not found in the scene.");
                return;
            }

            // Setup button click handlers
            rainButton.onClick.AddListener(() => _godPowers.currentPower = PowerType.Rain);
            fireballButton.onClick.AddListener(() => _godPowers.currentPower = PowerType.Fireball);
        }

        private void Update()
        {
            // Show the current year in the top of the screen
            float currentYear = Mathf.FloorToInt(Time.time - _beginningOfTime);
            currentYearText.text = $"Year {currentYear}";

            // Smoothly animate icons based on current power
            bool isRainSelected = _godPowers.currentPower == PowerType.Rain;
            bool isFireballSelected = _godPowers.currentPower == PowerType.Fireball;
            bool isLightningSelected = _godPowers.currentPower == PowerType.Lightning;

            rainIcon.localScale = Vector3.Lerp(
                rainIcon.localScale,
                Vector3.one * (isRainSelected ? selectedScale : normalScale),
                Time.deltaTime * animationSpeed
            );

            fireballIcon.localScale = Vector3.Lerp(
                fireballIcon.localScale,
                Vector3.one * (isFireballSelected ? selectedScale : normalScale),
                Time.deltaTime * animationSpeed
            );

            // if lightning is discovered, enable and show the lightning button
            if (_godPowers.IsLightningDiscovered)
            {
                lightningButton.interactable = true;
                fireballIcon.anchoredPosition = new Vector2(-225f, -100);
                rainIcon.anchoredPosition = new Vector2(-350f, -100);

                lightningIcon.localScale = Vector3.Lerp(
                    lightningIcon.localScale,
                    Vector3.one * (isLightningSelected ? selectedScale : normalScale),
                    Time.deltaTime * animationSpeed
                );
            }
        }

        public void UpdatePopulation(int population) => populationText.text = $"{population}";
        public void UpdateFoodSurplus(int food) => foodSurplusText.text = $"{food}";

        public void UpdateFaith(int faith, int maxFaith) =>
            faithBarImage.transform.localScale = new Vector3((float)faith / maxFaith, 1f, 1f);

        public void UpdateHappiness(int happiness, int maxHappiness)
        {
            float blend = (float)happiness / maxHappiness;
            happinessBarImage.color = blend < 0.5f
                ? Color.Lerp(Color.red, Color.yellow, blend * 2f)
                : Color.Lerp(Color.yellow, Color.green, (blend - 0.5f) * 2f);

            happinessBarImage.transform.localScale = new Vector3((float)happiness / maxHappiness, 1f, 1f);
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
}