using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DashboardManager : MonoBehaviour
{
    public float timeout=5f;
    
    public Text State;
    public Text State1;
    public Text State2;
    public Text command_Text;
    bool rcvState=false;
    bool sending=false;
    bool CR_running=false;
    // Start is called before the first frame update
    void Start()
    {
          MQTT_Manager.OnMessage+=ChangeState;    
          MQTT_Manager.OnState+=MelfaState;    
          MQTT_Manager.OnMonitorMessage+=MonitorMessage;    
       
        
    }
    public void MonitorMessage(string msg){
      State myObject = JsonUtility.FromJson<State>(msg);

      State2.text= myObject.state;
    }
    public void MelfaState(string msg)
    {
      State myObject = JsonUtility.FromJson<State>(msg);
      if(myObject.state.Contains("OK")){
         State1.color=Color.green;
      }
      State1.text= myObject.state;
    //State myObject1 = JsonUtility.FromJson<State>(myObject.data);
    //Data dataObj=JsonUtility.FromJson<Data>(myObject.data.data);
      Data dataObj=myObject.data;
      command_Text.text=dataObj.data;
      
    }
        void OnEnable()
    {
     rcvState=false;
     sending=true;
                FindObjectOfType<MQTT_Manager>().
          Publishing("melfa/control/testsv");
        //   if(  !CR_running ){
        StartCoroutine(Timer());
        //   }
    }
    public void ChangeState(string msg)
    {   
        rcvState=true;
        if(msg.Contains("nline"))
        {
        State.color=Color.green;
        }
        else{
            State.color=Color.yellow;
        }
         State.text=msg;
        // StartCoroutine(Timer());
        // sending=false;
       
    }
            IEnumerator Timer()
   {
// CR_running=true;
    yield return new WaitForSeconds(timeout);
          sending=true;
          rcvState=false;
          FindObjectOfType<MQTT_Manager>().
          Publishing("melfa/control/testsv");
        //   StartCoroutine(Timer());
    yield return new WaitForSeconds(timeout/2);
    sending=false;
    StartCoroutine(Timer());
    // CR_running=false;
   }

    void Update()
    {
            if(!rcvState && !sending) 
            {
            State.color=Color.red;
            State.text="Melfa is Offline Now";

            }
    }


    //           void OnDisable()
    // {
    // MQTT_Manager.OnMessage-=ChangeState;    
    // }
  
}
