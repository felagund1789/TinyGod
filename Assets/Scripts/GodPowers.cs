using System.Collections;
using EventBus;
using Events;
using UnityEngine;

public class GodPowers : MonoBehaviour
{
    [SerializeField] private FaithManager faithManager;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private GameObject rainEffect;
    [SerializeField] private GameObject fireballPrefab;
    [SerializeField] private float fireballForce = 500f;
    [SerializeField] private GameObject planet;
    [SerializeField] private LayerMask planetLayer;

    private UIController uiController;
    private Coroutine _rainCoroutine;

    public enum PowerType { Rain, Fireball }
    public PowerType currentPower = PowerType.Rain;

    private void Start()
    {
        uiController = FindFirstObjectByType<UIController>();
        if (uiController == null)
        {
            Debug.LogError("UIController not found in the scene.");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) currentPower = PowerType.Rain;
        if (Input.GetKeyDown(KeyCode.Alpha2)) currentPower = PowerType.Fireball;

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 1000f, planetLayer))
            {
                UsePower(hit.point);
            }
        }
    }

    void UsePower(Vector3 point)
    {
        switch (currentPower)
        {
            case PowerType.Rain:
                if (_rainCoroutine == null && faithManager.ConsumeFaith(10f))
                    _rainCoroutine = StartCoroutine(Rain(point));
                else
                {
                    uiController.ShowMessage("Can't use Rain right now.", true);
                    Debug.Log("Can't use Rain right now. Rain is already active or not enough faith.");
                }
                break;
            case PowerType.Fireball:
                if (faithManager.ConsumeFaith(20f))
                    ThrowFireball(point);
                else
                {
                    uiController.ShowMessage("Not enough faith to throw a fireball.", true);
                    Debug.Log("Not enough faith to throw a fireball.");
                }
                break;
        }
    }

    private IEnumerator Rain(Vector3 point)
    {
        Vector3 direction = point.normalized;
        Vector3 position = point + direction * 0.5f;
        GameObject rainGameObject = Instantiate(rainEffect, position, Quaternion.identity, planet.transform);
        rainGameObject.transform.up = direction;
        rainGameObject.transform.localScale *= 0.01f;
        ParticleSystem rainParticles = rainGameObject.GetComponent<ParticleSystem>();

        Bus<RainEvent>.Raise(new RainEvent(point, (RainType)Random.Range(0, 2)));
        yield return new WaitForSeconds(5f);

        rainParticles.Stop();
        Destroy(rainGameObject);
        _rainCoroutine = null;
    }

    private void ThrowFireball(Vector3 point)
    {
        GameObject fb = Instantiate(fireballPrefab, mainCamera.transform.position, Quaternion.identity);
        fb.GetComponent<Rigidbody>().AddForce((point - fb.transform.position).normalized * fireballForce);
    }
}