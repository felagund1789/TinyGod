using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;

[RequireComponent(typeof(DecalProjector))]
public class MouseDecalProjector : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private GodPowers godPowers;
    [SerializeField] private LayerMask groundLayerMask;
    private DecalProjector _projector;
    private const float RayDistance = 100f;
    private readonly Vector3 _decalOffset = Vector3.up * 0.01f; // Prevent z-fighting

    private void Awake()
    {
        _projector = GetComponent<DecalProjector>();
        if (_projector == null)
        {
            Debug.LogError("Projector component not found on this GameObject.");
            enabled = false;
            return;
        }
        
        StartCoroutine(UpdateDecalRoutine());
    }

    private void Update()
    {
        // SetDecalSize();
        // UpdateDecalPosition();
    }

    private IEnumerator UpdateDecalRoutine()
    {
        while (true)
        {
            UpdateDecalPosition();
            yield return new WaitForSeconds(0.05f); // every 50ms (~20FPS)
        }
    }

    private void SetDecalSize()
    {
        _projector.size = godPowers.currentPower == GodPowers.PowerType.Rain 
            ? new Vector3(0.5f, 0.5f, 0.5f) 
            : new Vector3(0.25f, 0.25f, 0.25f);
    }

    private void UpdateDecalPosition()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, RayDistance, groundLayerMask))
        {
            // Move the decal to the hit point
            transform.position = hit.point + _decalOffset;

            // Align the decal to face downward (align to hit normal if needed)
            transform.rotation = Quaternion.LookRotation(-hit.normal, Vector3.up);
        }
    }
}