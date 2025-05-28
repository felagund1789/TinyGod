using System;
using System.Collections;
using EventBus;
using Events;
using Managers;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using Random = UnityEngine.Random;

public class GodPowers : MonoBehaviour
{
    [SerializeField] private PlanetController planetController;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private UIController uiController;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private GameObject lightningEffect;
    [SerializeField] private GameObject rainEffect;
    [SerializeField] private GameObject fireballPrefab;
    [SerializeField] private float fireballForce = 500f;
    [SerializeField] private GameObject planet;
    [SerializeField] private LayerMask planetLayer;

    public enum PowerType { Rain, Fireball, Lightning }
    public PowerType currentPower = PowerType.Rain;
    [field: SerializeField] public float LightningDiscoverTime { get; private set;  } = 100f;
    private float _beginningOfTime = float.MaxValue;
    public bool IsLightningDiscovered => Time.time - _beginningOfTime >= LightningDiscoverTime;

    private void Start()
    {
        _beginningOfTime = Time.time;
    }

    void Update()
    {
        // If the game is paused, do not process input
        if (Time.timeScale == 0f)
            return;

        if (Input.GetKeyDown(KeyCode.Alpha1)) currentPower = PowerType.Rain;
        if (Input.GetKeyDown(KeyCode.Alpha2)) currentPower = PowerType.Fireball;
        if (Input.GetKeyDown(KeyCode.Alpha3) && IsLightningDiscovered) currentPower = PowerType.Lightning;

        if (planetController.IsDragging) return;

        if (Input.GetMouseButtonUp(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 1000f, planetLayer)
                && !EventSystem.current.IsPointerOverGameObject())
            {
                UsePower(hit.point);
            }
        }
    }

    void UsePower(Vector3 point)
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        switch (currentPower)
        {
            case PowerType.Rain:
                if (gameManager.Faith >= 10)
                {
                    Bus<FaithUsedEvent>.Raise(new FaithUsedEvent(10));
                    Coroutine rainCoroutine = StartCoroutine(Rain(point));
                }
                else
                {
                    uiController?.ShowMessage("Not enough faith to use Rain.", true);
                    Debug.Log("Not enough faith to use Rain.");
                }
                break;
            case PowerType.Fireball:
                if (gameManager.Faith >= 20)
                {
                    Bus<FaithUsedEvent>.Raise(new FaithUsedEvent(20));
                    ThrowFireball(point);
                }
                else
                {
                    uiController?.ShowMessage("Not enough faith to throw a fireball.", true);
                    Debug.Log("Not enough faith to throw a fireball.");
                }
                break;
            case PowerType.Lightning:
                if (!IsLightningDiscovered) break;
                if (gameManager.Faith >= 20)
                {
                    Bus<FaithUsedEvent>.Raise(new FaithUsedEvent(20));
                    Coroutine lightningCoroutine = StartCoroutine(StrikeWithLightning(point));
                }
                else
                {
                    uiController?.ShowMessage("Not enough faith to strike with lightning.", true);
                    Debug.Log("Not enough faith to strike with lightning.");
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
    }

    private void ThrowFireball(Vector3 point)
    {
        GameObject fb = Instantiate(fireballPrefab, mainCamera.transform.position, Quaternion.identity);
        fb.GetComponent<Rigidbody>().AddForce((point - fb.transform.position).normalized * fireballForce);
    }

    
    private IEnumerator StrikeWithLightning(Vector3 point)
    {
        Vector3 direction = point.normalized;
        Vector3 position = point + direction * 0.2f;
        GameObject lightningGameObject = Instantiate(lightningEffect, position, Quaternion.identity, planet.transform);
        lightningGameObject.transform.up = direction;
        lightningGameObject.transform.localScale *= 0.1f;
        ParticleSystem lightningParticles = lightningGameObject.GetComponent<ParticleSystem>();

        Bus<LightningEvent>.Raise(new LightningEvent(point));
        yield return new WaitForSeconds(4f);

        lightningParticles.Stop();
        Destroy(lightningGameObject);
    }
}