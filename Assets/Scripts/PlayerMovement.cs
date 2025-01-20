using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float playerSpeed = 5.0f; // Speed for forward movement
    public float horizontalSpeed = 5.0f; // Speed for horizontal movement
    public float rightLimit = 5.5f; // Right boundary
    public float leftLimit = -5.5f; // Left boundary

    private float horizontalInput = 0.0f;

    void Start()
    {
        // Initialization if needed
    }

    void Update()
    {
        // Update horizontal input based on input source
        HandleInput();

        // Move the player forward
        transform.Translate(Vector3.forward * Time.deltaTime * playerSpeed, Space.World);

        // Calculate new horizontal position
        float newXPosition = transform.position.x + horizontalInput * Time.deltaTime * horizontalSpeed;

        // Clamp the new position within boundaries
        newXPosition = Mathf.Clamp(newXPosition, leftLimit, rightLimit);

        // Apply the new position
        transform.position = new Vector3(newXPosition, transform.position.y, transform.position.z);
    }

    private void HandleInput()
    {
        horizontalInput = 0.0f; // Reset horizontal input

        // Handle Arduino input (placeholder - replace with actual Arduino code)
        // Example: horizontalInput = ArduinoInput.GetHorizontal(); 

        // Handle phone tilt for WebGL
        if (Application.platform == RuntimePlatform.WebGLPlayer)
        {
            horizontalInput = Input.acceleration.x; // -1 for left, 1 for right
        }

        // Handle keyboard input (fallback for testing or desktop)
        if (Input.GetKey(KeyCode.A))
        {
            horizontalInput -= 1.0f;
        }
        if (Input.GetKey(KeyCode.D))
        {
            horizontalInput += 1.0f;
        }

        // Clamp the input to ensure it stays within a valid range
        horizontalInput = Mathf.Clamp(horizontalInput, -1.0f, 1.0f);
    }


    public void ResetPlayer() {
        // Reset player position
        transform.position = new Vector3(0, 0, 0);
    }
}