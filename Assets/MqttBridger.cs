using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MqttBridger : MonoBehaviour
{
            public string topic_message="";
        public string topic_control="";
        public string topic_monitor="";
        // public string topic2pub="";

           public string topic_service="";
public delegate void CallbackDelegate(string msg);
 public static event CallbackDelegate OnMessage;
 public static event CallbackDelegate OnState;
public static event CallbackDelegate OnMonitorMessage;
    // Start is called before the first frame update
    void Start()
    {
        
    }
  public void SubBridger(string packet)
        {
            // Packet Devide to Topic | Payload
            string[] message=packet.Split('|');
            string topic=message[0];
            string msg=message[1];
            Debug.Log(System. DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")+" : "+topic+"|"+msg);
            if(topic.Contains(topic_message))
            {
            
                try{
                    OnMessage(msg);
                }
                catch{

                }
                
            }
            if(topic.Contains(topic_control)){

            }
            if(topic.Contains(topic_monitor))
            {
                    if(topic.Contains("message/state"))
                 {
                 try{
                    OnState(msg);
                   
                }
                catch{
                    
                }
                 }
                 else{
                             try{
                    OnMonitorMessage(msg);
                }
                catch{

                }
                
                 }
                //  else if()
        
            }
            // // StoreMessage(msg);
            // if (topic == topic2Sub)
            // {
            //     Debug.Log(topic+" 1: " + msg);
            // }
            // else if (topic==topic2Sub1)
            // {
            //        Debug.Log(topic2Sub1+" 2 :" + msg);
            // }
            // else{
            //      Debug.Log(topic+ msg);
            // }
        }
    // Update is called once per frame
    void Update()
    {
        
    }
}
