using UnityEngine;
using System.IO.Ports;

public class ArduinoReader : MonoBehaviour 
{
   private SerialPort port;
   private bool isRunning = false;

   void OnEnable()
   {
       InvokeRepeating("TryInitialize", 0f, 2f);
   }

   void TryInitialize()
   {
       if(isRunning) return;
       
       try
       {
           port?.Close();
           port = new SerialPort("/dev/cu.usbserial-2130", 115200);
           port.Open();
           
           isRunning = true;
           CancelInvoke("TryInitialize");
       }
       catch {}
   }

   void Update()
   {
       if(!isRunning) return;
       
       try
       {
           if(port?.BytesToRead > 0)
               Debug.Log($"Data: {port.ReadLine()}");
       }
       catch
       {
           isRunning = false;
       }
   }

   void OnDisable()
   {
       CancelInvoke();
       port?.Close();
   }
}