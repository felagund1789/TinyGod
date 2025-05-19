using UnityEngine;

public class Fireball : MonoBehaviour
{
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Tree") || col.gameObject.CompareTag("NPC") || col.gameObject.CompareTag("Farm"))
        {
            Debug.Log($"Destroying {col.gameObject.name}");
            Destroy(col.gameObject); // burn!
        }
        gameObject.GetComponent<Rigidbody>().isKinematic = true; // stop moving
        Destroy(gameObject, 0.5f);
    }
}