using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Edge_Manager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
          MQTT_Manager.OnMonitorMessage+=Detect;  
         
    }
    public void Detect(string msg)
    {
                 if(msg.Contains("detect"))
            {
                print(System. DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")+"DETECTION HUMAN AND STOP ROBOT");

            }

    }



}
