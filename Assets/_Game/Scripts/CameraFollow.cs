using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // The target the camera will follow
    public float smoothSpeed = 0.125f; // The speed of the camera's movement
    public Vector3 offset; // The offset from the target's position
    public Vector3 rotationOffset; // The offset for the camera's rotation

    public void OnInit()
    {
        if (target == null)
        {
            Debug.LogError("CameraFollow: No target assigned. Please assign a target for the camera to follow.");
        }
        else
        {
            // Initialize the camera's position and rotation based on the target and offsets
            transform.position = target.position + offset;
            transform.rotation = Quaternion.Euler(rotationOffset);
        }
    }
    void LateUpdate()
    {
        if (target != null)
        {
            Vector3 desiredPosition = target.position + offset; // Calculate the desired position
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed); // Smoothly interpolate to the desired position
            transform.position = smoothedPosition; // Update the camera's position

            Quaternion desiredRotation = Quaternion.Euler(rotationOffset); // Calculate the desired rotation
            transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, smoothSpeed); // Smoothly interpolate to the desired rotation
        }
    }
}