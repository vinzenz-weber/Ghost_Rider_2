using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float initialSpeed = 5.0f; // Speed after ramp-up
    public float horizontalSpeed = 5.0f; // Speed for horizontal movement
    public float speedIncreaseRate = 0.1f; // Rate at which the speed increases per second
    public float maxSpeed = 20.0f; // Maximum speed limit
    public float rightLimit = 5.5f; // Right boundary
    public float leftLimit = -5.5f; // Left boundary
    public float rampUpDuration = 4.0f; // Time to ramp up to the initial speed

    private float horizontalInput = 0.0f;
    public float playerSpeed = 0.0f; // Start speed at 0
    private bool rampUpComplete = false;
    private float rampUpTimeElapsed = 0.0f;

    void Start()
    {
        // Initialize player speed and state
        playerSpeed = 0.0f;
        rampUpComplete = false;
        rampUpTimeElapsed = 0.0f;
    }

    void Update()
    {
        // Handle horizontal input
        HandleInput();

        // Gradually ramp up speed over the ramp-up duration
        if (!rampUpComplete)
        {
            rampUpTimeElapsed += Time.deltaTime;
            playerSpeed = Mathf.Lerp(0.0f, initialSpeed, rampUpTimeElapsed / rampUpDuration);

            if (rampUpTimeElapsed >= rampUpDuration)
            {
                rampUpComplete = true;
                playerSpeed = initialSpeed; // Ensure speed reaches the initial value
            }
        }
        else
        {
            // Apply normal speed increase rate after ramp-up
            playerSpeed = Mathf.Min(playerSpeed + speedIncreaseRate * Time.deltaTime, maxSpeed);
        }

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

    public void ResetPlayer()
    {
        // Reset player position
        transform.position = new Vector3(0, 0, 0);

        // Reset player speed and ramp-up state
        playerSpeed = 0.0f;
        rampUpComplete = false;
        rampUpTimeElapsed = 0.0f;
    }
}