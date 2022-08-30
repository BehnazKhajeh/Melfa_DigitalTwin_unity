using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ControllerManager : MonoBehaviour
{
    
    public float timeout=5f;
        public Text State;
    bool rcvState=false;
    bool timetocheck=true;
    bool sending=false;
    // Start is called before the first frame update
    void Start()
    { 
      
    }
    void OnEnable()
    {
        Debug.Log("ONENABLE CONTROLLer");
              FindObjectOfType<MQTT_Manager>().
          Publishing("melfa/control/svon");
       
    }
        void OnDisable()
    {
               FindObjectOfType<MQTT_Manager>().
          Publishing("melfa/control/svoff");
         
    }
  
    
    // Update is called once per frame
    void Update()
    {
        
    }
    
    // public void j1(int sign){
    //       FindObjectOfType<MQTT_Manager>().
    //       Publishing("melfa/control/start");
    // }
}
