using UnityEngine;

/// <summary>
/// Simple orbit camera: hold the right mouse button to look around, scroll to zoom.
/// Attach to the Main Camera and assign a target (e.g. an empty object in the
/// center of the construction site).
/// </summary>
public class CameraOrbit : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float distance = 18f;
    [SerializeField] private float rotationSpeed = 4f;
    [SerializeField] private float zoomSpeed = 4f;
    [SerializeField] private float minDistance = 6f;
    [SerializeField] private float maxDistance = 30f;
    [SerializeField] private float minPitch = 15f;
    [SerializeField] private float maxPitch = 75f;

    // Current view angles, updated by mouse input.
    private float yaw = 45f;
    private float pitch = 40f;

    private void LateUpdate()
    {
        if (target == null) return;

        // Rotate while holding the right mouse button
        if (Input.GetMouseButton(1))
        {
            yaw += Input.GetAxis("Mouse X") * rotationSpeed;
            pitch -= Input.GetAxis("Mouse Y") * rotationSpeed;
            // Keep the vertical angle in a sensible range so the camera can't flip over.
            pitch = Mathf.Clamp(pitch, minPitch, maxPitch);
        }

        // Scroll to zoom
        distance -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed * 5f;
        distance = Mathf.Clamp(distance, minDistance, maxDistance);

        // Position the camera on a sphere around the target and look at it.
        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0f);
        transform.position = target.position + rotation * new Vector3(0f, 0f, -distance);
        transform.LookAt(target.position);
    }
}
