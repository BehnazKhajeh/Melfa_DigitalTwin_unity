using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using System.Threading;
public class PortConnection : MonoBehaviour
{
SerialPort stream ;
public delegate void CallbackDelegate(string msg);
public static event CallbackDelegate OnMonitorMessage;
public string the_com="COM2";
bool isExit=false;
public float timeout=0.01f;
public  int ReadTimeout=500000;

    void Start () {
    //    string[] ports = SerialPort.GetPortNames();
 
        // Debug.Log("The following serial ports were found:");
 
        // foreach(string port in ports)
        // {
        //     Debug.Log(port);
        // }
    stream = new SerialPort(the_com, 9600);
    stream.WriteTimeout = 300;
    stream.ReadTimeout = ReadTimeout;
    stream.Parity=Parity.Even;
    stream.StopBits=StopBits.Two;
    stream.ReadBufferSize=2;
    stream.DtrEnable = true;
    stream.RtsEnable = true;
    stream.Open();

             if (!stream.IsOpen)
            {
                stream.Open();
                print("opened stream");
            }
            
        // Thread _thread = new Thread(ReadingFromPort);
        // _thread.Start();
        StartCoroutine(Timer(3));

    }
    // Update is called once per frame
 private void OnApplicationQuit() {
    isExit=true;
}
    IEnumerator Timer(float time)
   {

    yield return new WaitForSeconds(time);
    //   Debug.Log( stream.ReadLine());   
    // StartCoroutine(Timer(timeout));
         Thread _thread = new Thread(ReadingFromPort);
        _thread.Start();

   }
   public void ReadingFromPort(){
    // byte[] b=(byte)255;
    // Debug.Log( "ReadingFromPort");
    string line;
    while(true)
    {
        line=stream.ReadLine();
//  char [] cArray= System.Text.Encoding.ASCII.GetString(line).ToCharArray();
   
     Debug.Log(System. DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")+" : "+line); 
     if(line.Contains("JPOSF"))
     {
        OnMonitorMessage(line);
     }

      if(isExit)
      {
        stream.Close();
        break;
      }
    }
    
 
    
   }


    
}
