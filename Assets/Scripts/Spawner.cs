using EventBus;
using Events;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject planet;
    [SerializeField] private GameObject treePrefab;
    [SerializeField] private GameObject farmPrefab;
    [Range(1, 5)] [SerializeField] private int forestsCount = 2;
    [Range(10, 100)] [SerializeField] private int treesPerForest = 20;
    [Range(0.1f, 0.5f)] [SerializeField] private float maxDistance = 0.1f;

    void Start()
    {
        for (int i = 0; i < forestsCount; i++)
            SpawnPrefabs(Random.onUnitSphere, treePrefab, treesPerForest);
    }

    private void Awake()
    {
        Bus<RainEvent>.OnEvent += OnRainEvent;
    }

    private void OnDestroy()
    {
        Bus<RainEvent>.OnEvent -= OnRainEvent;
    }

    private void OnRainEvent(RainEvent evt)
    {
        switch (evt.Type)
        {
            case RainType.Light:
                SpawnPrefabs(evt.Location, farmPrefab);
                break;
            case RainType.Medium:
                SpawnPrefabs(evt.Location, treePrefab);
                break;
        }
    }

    private void SpawnPrefabs(Vector3 location, GameObject prefab, int count = 5)
    {
        Vector3 basePosition = location.normalized * (planet.transform.localScale.x / 2);
        for (int i = 0; i < count; i++)
        {
                
            Vector3 offset = (basePosition + Random.insideUnitSphere * maxDistance).normalized;
            Vector3 targetPosition = planet.transform.position + offset * (planet.transform.localScale.x / 2);
            Vector3 direction = targetPosition.normalized;
            GameObject tree = Instantiate(prefab, planet.transform);
            tree.GetComponent<AbstractGrowable>().Spawn(targetPosition, direction);
        }
    }
}