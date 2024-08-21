using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    [Header("Settings")]
    [Range(0f, 0.1f)]
    public float mouseParallaxFactor = 0.02f;

    [Header("Constraints")]
    public Vector2 movementRange = new Vector2(1f, 1f); // Limits the movement range

    private Vector3 startPosition;
    private Vector3 screenCenter;

    void Start()
    {
        startPosition = transform.position;
        screenCenter = new Vector3(Screen.width / 2f, Screen.height / 2f, 0);
    }

    void Update()
    {
        // Calculate mouse delta relative to screen center
        Vector3 mouseDelta = (Input.mousePosition - screenCenter) * mouseParallaxFactor;

        // Calculate new position
        Vector3 newPosition = startPosition + new Vector3(mouseDelta.x, mouseDelta.y, 0);

        // Constrain position within movement range
        newPosition.x = Mathf.Clamp(newPosition.x, startPosition.x - movementRange.x, startPosition.x + movementRange.x);
        newPosition.y = Mathf.Clamp(newPosition.y, startPosition.y - movementRange.y, startPosition.y + movementRange.y);

        // Apply constrained position
        transform.position = new Vector3(newPosition.x, newPosition.y, transform.position.z);
    }
}
