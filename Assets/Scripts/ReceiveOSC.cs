using UnityEngine;
using extOSC;

public class ReceiveOSC : MonoBehaviour
{
    private OSCReceiver receiver;
    public int port = 7000; // Change this to match your Arduino's sending port

    void Start()
    {
        // Initialize the receiver
        receiver = gameObject.AddComponent<OSCReceiver>();
        receiver.LocalPort = port;

        // Bind the message handler
        receiver.Bind("/arduino/data", MessageReceived); // Change "/arduino/data" to match your OSC address
    }

    void MessageReceived(OSCMessage message)
    {
        if (message == null) return;

        // Handle different types of messages
        if (message.Values.Count > 0)
        {
            // For float values
            if (message.Values[0].Type == OSCValueType.Float)
            {
                float value = message.Values[0].FloatValue;
                Debug.Log($"Received float value: {value}");
            }
            // For integer values
            else if (message.Values[0].Type == OSCValueType.Int)
            {
                int value = message.Values[0].IntValue;
                Debug.Log($"Received integer value: {value}");
            }
            // For string values
            else if (message.Values[0].Type == OSCValueType.String)
            {
                string value = message.Values[0].StringValue;
                Debug.Log($"Received string value: {value}");
            }
        }
    }

    void OnDestroy()
    {
        // Clean up
        if (receiver != null)
            receiver.Close();
    }
}