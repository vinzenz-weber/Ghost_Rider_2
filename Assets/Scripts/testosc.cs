using UnityEngine;
using extOSC;

public class testosc : MonoBehaviour
{
    [Header("OSC Settings")]
    public int port = 7000; // Port for OSC listening
    private OSCReceiver _receiver;



    void Start()
    {
        // Create an OSCReceiver instance
        _receiver = gameObject.AddComponent<OSCReceiver>();

        // Set the listening port
        _receiver.LocalPort = port;

        // Register a callback for all incoming messages
        _receiver.Bind("/Gyro/XY", OnMessageReceived);

        Debug.Log($"OSC Receiver started on port {port} for address /any");
    }

    private void OnMessageReceived(OSCMessage message)
    {
        Debug.Log($"Received OSC message from address: {message.Address}");

        // Check if the message contains at least two values (X and Y)
        if (message.Values.Count >= 2)
        {
            float mappedX = message.Values[0].FloatValue; // First value (X)
            float mappedY = message.Values[1].FloatValue; // Second value (Y)

            Debug.Log($"Mapped Acceleration X: {mappedX}, Y: {mappedY}");
        }
        else
        {
            Debug.LogWarning("OSC message does not contain enough values.");
        }
    }
}