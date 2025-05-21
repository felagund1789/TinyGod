using System.Collections;
using EventBus;
using Events;
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

    private void OnRainEvent(RainEvent evt)
    {
        if (Vector3.Distance(evt.Location, transform.position) < 0.25f)
            Water();
    }

    private void Water()
    {
        if (_growCoroutine != null) return;

        _growCoroutine = StartCoroutine(GrowCrops()); // simulate growth delay
    }

    private IEnumerator GrowCrops()
    {
        _meshRenderer.material.color = Color.green;
        yield return new WaitForSeconds(5f); // simulate growth time

        _meshRenderer.material.color = Color.yellow;
        yield return new WaitForSeconds(2f); // simulate harvest time

        int food = Random.Range(3, 6); // crop yield
        Bus<FoodProducedEvent>.Raise(new FoodProducedEvent(food));
        _meshRenderer.material.color = Color.white;
        _growCoroutine = null;
    }
}