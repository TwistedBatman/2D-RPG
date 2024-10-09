using UnityEngine;

public class CameraEffect : MonoBehaviour
{
    public Transform target;

    public float smoothSpeed; // 10
    public Vector3 offset; // Offset for the camera position 0 0 -10
    public Vector2 minPosition; // -7 -5
    public Vector2 maxPosition; // 8 5

    void FixedUpdate()
    {
        // Use this if statement to give the camera an effect that falls behind when the player is moving
        if (transform.position != target.position)
        {
            Vector3 desiredPosition = target.position + offset; // Add the 2 vectors to get the target position we want

            // Create the boundaries for the camera
            desiredPosition.x = Mathf.Clamp(desiredPosition.x, minPosition.x, maxPosition.x);
            desiredPosition.y = Mathf.Clamp(desiredPosition.y, minPosition.y, maxPosition.y);

            transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        }
    }
}