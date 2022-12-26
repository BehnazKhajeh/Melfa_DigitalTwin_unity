using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using M2MqttUnity;
public class MQTT_Manager : M2MqttUnityClient
{
public delegate void CallbackDelegate(string msg);
 public static event CallbackDelegate OnMessage;
 public static event CallbackDelegate OnState;
public static event CallbackDelegate OnMonitorMessage;
     [Header("MQTT Connection")]
        // public string brokerAddress_M="";
        // public string brokerPort_M="";
        // public bool  isEncrypted_M=false;
        public string topic_message="";
        public string topic_control="";
        public string topic_monitor="";
        // public string topic2pub="";


      protected override void Start()
        {
            // SetUiMessage("Ready.");
            // updateUI = true;
            Debug.Log(System. DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")+" : "+"Start");
            // SetupConnection();
            base.Start();
        }
        // public  void SetupConnection()
        // {
        //     this.brokerAddress = brokerAddress_M;
        //     this.brokerAddress=brokerPort_M;
        //     this.isEncrypted=isEncrypted_M;
        // }
        public void Publishing(string topic)
        {
            client.Publish(topic, System.Text.Encoding.UTF8.GetBytes(""), MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE, false);
            Debug.Log(System. DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")+" : "+topic+":send");
            // AddUiMessage("Test message published.");
        }
                public void Publishing_Payload(string topic,string payload)
        {
            client.Publish(topic, System.Text.Encoding.UTF8.GetBytes(payload), MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE, false);
            Debug.Log(System. DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")+"Test message published");
            // AddUiMessage("Test message published.");
        }

           protected override void OnConnecting()
        {
            base.OnConnecting();
             Debug.Log(System. DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")+"Connecting to broker on " + brokerAddress + ":" + brokerPort.ToString() + "...\n");
            // SetUiMessage("Connecting to broker on " + brokerAddress + ":" + brokerPort.ToString() + "...\n");
        }

        protected override void OnConnected()
        {
            base.OnConnected();
            Debug.Log(System. DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")+"Connected to broker on " + brokerAddress + "\n");
            // SetUiMessage("Connected to broker on " + brokerAddress + "\n");

            // if (autoTest)
            // {
            //     TestPublish();
            // }
        }

        protected override void SubscribeTopics()
        {
            client.Subscribe(new string[] { topic_message+"/#",topic_control+"/#",topic_monitor+"/#" }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE,MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE,MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
        }

        protected override void UnsubscribeTopics()
        {
            client.Unsubscribe(new string[] {  topic_message+"/#",topic_control+"/#",topic_monitor+"/#"});
        }

        protected override void OnConnectionFailed(string errorMessage)
        {
            // AddUiMessage("CONNECTION FAILED! " + errorMessage);
        }

        protected override void OnDisconnected()
        {
            // AddUiMessage("Disconnected.");
        }

        protected override void OnConnectionLost()
        {
            // AddUiMessage("CONNECTION LOST!");
        }




        protected override void DecodeMessage(string topic, byte[] message)
        {
            string msg = System.Text.Encoding.UTF8.GetString(message);
           
            Debug.Log(System. DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")+" : "+topic+"   : "+msg);
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


                protected override void Update()
        {
            base.Update(); // call ProcessMqttEvents()

            // if (eventMessages.Count > 0)
            // {
            //     foreach (string msg in eventMessages)
            //     {
            //         ProcessMessage(msg);
            //     }
            //     eventMessages.Clear();
           // }
            // if (updateUI)
            // {
            //     UpdateUI();
            // }
        }



        public void SetBrokerAddress(string brokerAddress)
        {
                this.brokerAddress = brokerAddress;
        }

        public void SetBrokerPort(string brokerPort)
        {
                int.TryParse(brokerPort, out this.brokerPort);
        }
        public void SetEncrypted(bool isEncrypted)
        {
            this.isEncrypted = isEncrypted;
        }
}
