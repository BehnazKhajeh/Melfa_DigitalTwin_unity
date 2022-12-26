using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DT_Agent : MonoBehaviour
{
    // Start is called before the first frame update
    bool roboMoving=false;
    bool humanDetection=false;
    bool isFirstSync=true;
    public float timeout=1f;
    void Start()
    {
          MQTT_Manager.OnMonitorMessage+=Detect; 
           MQTT_Manager.OnMonitorMessage+=MoveJoint;  
    }
    public void Detect(string msg)
    {
                 if(msg.Contains("Human Detected"))
            {
                humanDetection=true;
                 StartCoroutine(Timer_Detecting(timeout));
                    print(System. DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")+" : "+"DETECTION HUMAN");
            }
                           

    }
      public void MoveJoint(string msg)
    {
                 if(msg.Contains("JPOSF"))
            {
                if(!isFirstSync)
                {
                    roboMoving=true;
                    StartCoroutine(Timer_Moving(timeout));
                    print(System. DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")+" : "+"ROBOT IS MOVING");
                    
                }
                isFirstSync=false;
            }

    }
                IEnumerator Timer_Moving(float time)
   {

     yield return new WaitForSeconds(time);
     roboMoving=false;
   }
                   IEnumerator Timer_Detecting(float time)
   {

    yield return new WaitForSeconds(time);
    humanDetection=false;
   }
    void Update(){
        if(roboMoving & humanDetection )
        {
                     FindObjectOfType<MQTT_Manager>().
          Publishing("melfa/control/emgstop");
            print(System. DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")+" : "+"STOP EMG");
            humanDetection=false;
            roboMoving=false;
        }
    }
}
