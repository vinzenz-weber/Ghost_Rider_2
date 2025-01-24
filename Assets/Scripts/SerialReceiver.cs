using UnityEngine;
using System.IO.Ports;

public class DualArduinoReader : MonoBehaviour 
{
   SerialPort dial;
   SerialPort board;
   
   void Start()
   {
       dial = new SerialPort("COM8", 9600); // Port anpassen
       board = new SerialPort("COM7", 115200); // Port anpassen
       
       dial.Open();
       board.Open();
   }

   void Update()
   {
       if (dial.IsOpen && dial.BytesToRead > 0)
       {
           string data1 = dial.ReadLine();
           Debug.Log("Dial: " + data1);
       }
       
       if (board.IsOpen && board.BytesToRead > 0)
       {
           string data2 = board.ReadLine();
           Debug.Log("Board: " + data2); 
       }
   }

   void OnApplicationQuit()
   {
       if (dial != null && dial.IsOpen) dial.Close();
       if (board != null && board.IsOpen) board.Close();
   }
}