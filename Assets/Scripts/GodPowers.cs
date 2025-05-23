using System.Collections;
using EventBus;
using Events;
using Managers;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class GodPowers : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private UIController uiController;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private GameObject rainEffect;
    [SerializeField] private GameObject fireballPrefab;
    [SerializeField] private float fireballForce = 500f;
    [SerializeField] private GameObject planet;
    [SerializeField] private LayerMask planetLayer;

    public enum PowerType { Rain, Fireball }
    public PowerType currentPower = PowerType.Rain;

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
}