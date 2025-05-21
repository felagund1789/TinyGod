using System.Collections;
using EventBus;
using Events;
using Unity.VisualScripting;
using UnityEngine;

public class Farm : AbstractSpawnable, IDestructible
{
    private MeshRenderer _meshRenderer;
    private Coroutine _growCoroutine;

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        Bus<RainEvent>.OnEvent += OnRainEvent;
    }

    private void OnDestroy()
    {
        Bus<RainEvent>.OnEvent -= OnRainEvent;
    }

    private void OnRainEvent(RainEvent evt)
    {
        if (Vector3.Distance(evt.Location, transform.position) < 0.25f && _growCoroutine == null)
            _growCoroutine = StartCoroutine(GrowCrops());
    }

    private IEnumerator GrowCrops()
    {
        // simulate crop growth
        _meshRenderer.material.color = Color.green;
        float startTime = Time.time;
        float growthTime = 5f; // simulate growth time
        Vector3 initialScale = transform.localScale;
        Vector3 targetScale = new Vector3(initialScale.x, initialScale.y * 5f, initialScale.z); // simulate wheat growth
        while (Time.time < startTime + growthTime)
        {
            float t = (Time.time - startTime) / growthTime;
            transform.localScale = Vector3.Lerp(initialScale, targetScale, t);
            _meshRenderer.material.color = Color.Lerp(Color.green, Color.yellow, t);
            yield return null;
        }

        _meshRenderer.material.color = Color.yellow;
        yield return new WaitForSeconds(2f); // simulate harvest time

        int food = Random.Range(3, 6); // crop yield
        Bus<FoodProducedEvent>.Raise(new FoodProducedEvent(food));

        // reset scale and color
        transform.localScale = initialScale;
        _meshRenderer.material.color = Color.white;
        _growCoroutine = null;
    }
}