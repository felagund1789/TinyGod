using UnityEngine;

public class Sun : MonoBehaviour
{
    public float moveSpeed = 15f;

    void Update()
    {
        transform.RotateAround(Vector3.zero, Vector3.forward, moveSpeed * Time.deltaTime);
    }
}
