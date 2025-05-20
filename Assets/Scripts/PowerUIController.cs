using UnityEngine;
using UnityEngine.UI;

public class PowerUIController : MonoBehaviour
{
    [SerializeField] private RectTransform rainIcon;
    [SerializeField] private RectTransform fireballIcon;

    [Header("Animation Settings")]
    [SerializeField] private float selectedScale = 1.2f;
    [SerializeField] private float normalScale = 1f;
    [SerializeField] private float animationSpeed = 10f;

    private GodPowers godPowers;

    private void Start()
    {
        godPowers = FindFirstObjectByType<GodPowers>();
        if (godPowers == null)
        {
            Debug.LogError("GodPowers not found in the scene.");
            return;
        }
    }

    private void Update()
    {
        // Smoothly animate icons based on current power
        bool isRainSelected = godPowers.currentPower == GodPowers.PowerType.Rain;

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
}