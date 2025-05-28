using UnityEngine;
using UnityEngine.InputSystem;

namespace Managers
{
    public class PlanetController : MonoBehaviour
    {
        [SerializeField] private float rotationSpeed = 360f;
        public bool IsDragging { get; private set;  } = false;

        void Update()
        {
            if (Mouse.current == null || (!Mouse.current.rightButton.isPressed && !Mouse.current.leftButton.isPressed))
            {
                IsDragging = false;
                return;
            }

            Vector2 delta = Mouse.current.delta.ReadValue();
            if (delta.sqrMagnitude > 0.01f) // Detect small movements as dragging
            {
                IsDragging = true;
                transform.Rotate(Vector3.up, -delta.x * rotationSpeed * Time.deltaTime, Space.World);
                transform.Rotate(Vector3.right, delta.y * rotationSpeed * Time.deltaTime, Space.World);
            }
        }
    }
}