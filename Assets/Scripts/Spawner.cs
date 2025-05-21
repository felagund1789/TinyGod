using EventBus;
using Events;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject planet;
    [SerializeField] private GameObject treePrefab;
    [SerializeField] private GameObject farmPrefab;
    [SerializeField] private GameObject followerPrefab;
    [SerializeField] private LayerMask farmsLayerMask;
    [Range(1, 5)] [SerializeField] private int forestsCount = 2;
    [Range(10, 100)] [SerializeField] private int treesPerForest = 20;
    [Range(0.1f, 0.5f)] [SerializeField] private float maxDistance = 0.1f;
    [Range(0.1f, 0.5f)] [SerializeField] private float clearRadius = 0.25f;

    void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            SpawnPrefabs(Random.onUnitSphere, farmPrefab, 1);
            SpawnPrefabs(Random.onUnitSphere, followerPrefab, 1);
        }

        for (int i = 0; i < forestsCount; i++)
            SpawnPrefabs(Random.onUnitSphere, treePrefab, treesPerForest);
    }

    private void Awake()
    {
        Bus<RainEvent>.OnEvent += OnRainEvent;
        Bus<NPCReproduceEvent>.OnEvent += OnNPCReproduceEvent;
    }

    private void OnDestroy()
    {
        Bus<RainEvent>.OnEvent -= OnRainEvent;
        Bus<NPCReproduceEvent>.OnEvent -= OnNPCReproduceEvent;
    }

    private void OnNPCReproduceEvent(NPCReproduceEvent args)
    {
        SpawnPrefabs(Random.insideUnitSphere, followerPrefab, 1);
    }

    private void OnRainEvent(RainEvent evt)
    {
        Collider[] farms = new Collider[1];
        int count = Physics.OverlapSphereNonAlloc(evt.Location, clearRadius, farms, farmsLayerMask);
        if (count == 0)
            SpawnPrefabs(evt.Location, treePrefab, 10);
    }

    private void SpawnPrefabs(Vector3 location, GameObject prefab, int count = 5)
    {
        Vector3 basePosition = location.normalized * (planet.transform.localScale.x / 2);
        for (int i = 0; i < count; i++)
        {
                
            Vector3 offset = (basePosition + Random.insideUnitSphere * maxDistance).normalized;
            Vector3 targetPosition = planet.transform.position + offset * (planet.transform.localScale.x / 2);
            Vector3 direction = targetPosition.normalized;
            GameObject spawnable = Instantiate(prefab, planet.transform);
            spawnable.GetComponent<AbstractSpawnable>().Spawn(targetPosition, direction);
        }
    }
}