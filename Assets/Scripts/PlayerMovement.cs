using UnityEngine;
using System.IO.Ports;

public class PlayerMovement : MonoBehaviour
{
    public float initialSpeed = 5.0f;
    public float horizontalSpeed;
    public float speedIncreaseRate = 0.1f;
    public float maxSpeed = 20.0f;
    public float rightLimit = 5.5f;
    public float leftLimit = -5.5f;
    public float rampUpDuration = 4.0f;
    public float smoothingSpeed = 5.0f; // New smoothing parameter
    
    private SerialPort port;
    private bool isSerialRunning = false;
    private float horizontalInput = 0.0f;
    private float targetHorizontalInput = 0.0f; // Target for smoothing
    public float playerSpeed = 0.0f;
    private bool rampUpComplete = false;
    private float rampUpTimeElapsed = 0.0f;
    private float currentVelocity = 0f; // For SmoothDamp

    void Start()
    {
        playerSpeed = 0.0f;
        rampUpComplete = false;
        rampUpTimeElapsed = 0.0f;
        InitializeSerial();
    }

    private void InitializeSerial()
    {
        try
        {
            port?.Close();
            port = new SerialPort("COM7", 115200);
            port.Open();
            isSerialRunning = true;
        }
        catch
        {
            isSerialRunning = false;
        }
    }

    void Update()
    {
        HandleInput();
        UpdateSpeed();
        UpdatePosition();
    }

    private void UpdateSpeed()
    {
        if (!rampUpComplete)
        {
            rampUpTimeElapsed += Time.deltaTime;
            playerSpeed = Mathf.Lerp(0.0f, initialSpeed, rampUpTimeElapsed / rampUpDuration);

            if (rampUpTimeElapsed >= rampUpDuration)
            {
                rampUpComplete = true;
                playerSpeed = initialSpeed;
            }
        }
        else
        {
            playerSpeed = Mathf.Min(playerSpeed + speedIncreaseRate * Time.deltaTime, maxSpeed);
        }
    }

    private void UpdatePosition()
    {
        // Smooth horizontal input using SmoothDamp
        horizontalInput = Mathf.SmoothDamp(horizontalInput, targetHorizontalInput, ref currentVelocity, 1f / smoothingSpeed);

        transform.Translate(Vector3.forward * Time.deltaTime * playerSpeed, Space.World);

        float newXPosition = transform.position.x + horizontalInput * Time.deltaTime * horizontalSpeed;
        newXPosition = Mathf.Clamp(newXPosition, leftLimit, rightLimit);
        transform.position = new Vector3(newXPosition, transform.position.y, transform.position.z);
    }

    private void HandleInput()
    {
        float newInput = 0.0f;

        if (isSerialRunning)
        {
            try
            {
                if (port?.BytesToRead > 0)
                {
                    string serialData = port.ReadLine().Trim();
                    
                    if (float.TryParse(serialData, out float serialValue))
                    {
                        newInput = Mathf.Clamp(-serialValue, -1f, 1f);
                    }
                }
            }
            catch (System.Exception e)
            {
                Debug.LogError($"Serial error: {e.Message}");
                isSerialRunning = false;
                InitializeSerial();
            }
        }

        if (Application.platform == RuntimePlatform.WebGLPlayer || Application.platform == RuntimePlatform.Android)
        {
            newInput = Input.acceleration.x;
        }

        if (Input.GetKey(KeyCode.A))
        {
            newInput -= 1.0f;
        }
        if (Input.GetKey(KeyCode.D))
        {
            newInput += 1.0f;
        }

        targetHorizontalInput = Mathf.Clamp(newInput, -1.0f, 1.0f);
    }

    public void ResetPlayer()
    {
        transform.position = new Vector3(0, 0, 0);
        playerSpeed = 0.0f;
        rampUpComplete = false;
        rampUpTimeElapsed = 0.0f;
        horizontalInput = 0f;
        targetHorizontalInput = 0f;
        currentVelocity = 0f;
    }

    void OnDisable()
    {
        port?.Close();
    }
}