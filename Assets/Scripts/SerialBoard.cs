using UnityEngine;
using System.IO.Ports;

public class ArduinoBoard : MonoBehaviour 
{
    SerialPort serialPort;
    
    void Start()
    {
        serialPort = new SerialPort("COM8", 9600); // Port und Baudrate anpassen
        serialPort.Open();
    }

    void Update()
    {
        if (serialPort.IsOpen && serialPort.BytesToRead > 0)
        {
            string data = serialPort.ReadLine();
            // Daten verarbeiten
        }
    }

    void OnApplicationQuit()
    {
        if (serialPort != null && serialPort.IsOpen)
        {
            serialPort.Close();
        }
    }
}