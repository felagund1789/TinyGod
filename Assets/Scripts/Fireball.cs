using System;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    private void Awake()
    {
        Destroy(gameObject, 1.25f);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the other object has the IDestructible interface
        if (!other.gameObject.TryGetComponent(out IDestructible _)) return;

        Debug.Log($"Destroying {other.gameObject.name}");
        Destroy(other.gameObject); // burn!
    }
}