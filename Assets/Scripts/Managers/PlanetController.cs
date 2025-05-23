using UnityEngine;
using UnityEngine.InputSystem;

namespace Managers
{
    public class PlanetController : MonoBehaviour
    {
        [SerializeField] private float rotationSpeed = 360f;

        void Update()
        {
            if (Mouse.current == null || !Mouse.current.rightButton.isPressed)
                return;

            Vector2 delta = Mouse.current.delta.ReadValue();
            transform.Rotate(Vector3.up, -delta.x * rotationSpeed * Time.deltaTime, Space.World);
            transform.Rotate(Vector3.right, delta.y * rotationSpeed * Time.deltaTime, Space.World);
        }
    }
}
