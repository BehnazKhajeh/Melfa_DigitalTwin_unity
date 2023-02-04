using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

[Serializable]
public class Settings_Model
{
  
           public string ip_mqtt;
           public string port_mqtt;
           public bool Rs232;
           public string rs232_com;
           public bool monitoring;
           public float timer;
           public bool h_r;
           public bool h_model;
}
 
public class SettingsSript : MonoBehaviour
{
     Settings_Model set=new Settings_Model();
 [Header("MQTT Connection")]
           public string ip_mqtt;
           public InputField ip_m;
           public string port_mqtt;
            public InputField port_m;
           public GameObject mqtt;
 [Header("RS232 Connection")]
           public bool Rs232;
            public Toggle rs;
           public string rs232_com;
            public InputField Com_in;
           public GameObject _Rs232;
        
 [Header("Monitoring Mode")]
           public bool monitoring;
           public Toggle mn;
           public float timer;
           public InputField timer_in;
    
              public GameObject melfa;
 [Header("H-R")]
           public bool h_r;
           public Toggle hr;
           public bool hr_model;
           public Toggle _hr_model;
              public GameObject human_model;
           public GameObject _h_r;
    // Start is called before the first frame update
    void Awake()
    {
      if(  PlayerPrefs.GetString("Json_Setting","")=="")
      {
        Defualt_Save();
      }
      else{
        Load_setting();
      }
    }
    public void Defualt_Save()
    {
        ip_mqtt="192.168.247.128";
        port_mqtt="1883";
        Rs232=false;
        rs232_com="COM3";
        monitoring=false;
        timer=0.208f;
        h_r=false;
        hr_model=false;
      set.h_r=h_r;
      set.ip_mqtt=ip_mqtt;
      set.port_mqtt=port_mqtt;
      set.Rs232=Rs232;
      set.rs232_com=rs232_com;
      set.monitoring=monitoring;
      set.timer=timer;
      set.h_model=hr_model;
      string json = JsonUtility.ToJson(set);
      PlayerPrefs.SetString("Json_Setting", json);
      Ui_Change();
      Make_change();
    }
    public void Save()
    {
      bool ok=true;
      if(ip_m.text!=""){  ip_mqtt=ip_m.text;}
      else{ok=false;}
       
      if(port_m.text!="")   port_mqtt=port_m.text;
       else{ok=false;}
        Rs232=rs.isOn;
      if(Com_in.text!="")   rs232_com=Com_in.text;
       else{ok=false;}
        monitoring=mn.isOn;
      if(timer_in.text!="")   timer=float.Parse(timer_in.text);
       else{ok=false;}
        h_r=hr.isOn;
        hr_model=_hr_model.isOn;
      if(ok)
      {
      set.h_r=h_r;
      set.ip_mqtt=ip_mqtt;
      set.port_mqtt=port_mqtt;
      set.Rs232=Rs232;
      set.rs232_com=rs232_com;
      set.monitoring=monitoring;
      set.timer=timer;
      set.h_model=hr_model;
      string json = JsonUtility.ToJson(set);
      PlayerPrefs.SetString("Json_Setting", json);
      Make_change();
       Retry();
      }else{
        FindObjectOfType<MessagePanelManager>().ShowMessage("Filling all Input",Color.red,6,18);
      }
    }
    public void Make_change()
    {
       
        mqtt.GetComponent<MQTT_Manager>().SetBrokerAddress(set.ip_mqtt);
           mqtt.GetComponent<MQTT_Manager>().SetBrokerPort(set.port_mqtt);
            
           if(set.Rs232)
           {
            mqtt.SetActive(false);
            _Rs232.SetActive(true);
            
           _Rs232.GetComponent<PortConnection>().the_com=set.rs232_com;
           melfa.GetComponent<JointMovment>().isRs232=true;
           }
           if(set.monitoring)
           {
             _Rs232.SetActive(false);
              mqtt.SetActive(true);
             melfa.GetComponent<JointMovment>().isRs232=false;
               melfa.GetComponent<SyncWithMelfa>().timeloop=set.timer;
               melfa.GetComponent<SyncWithMelfa>().isSync=true;
           }
           _h_r.SetActive(set.h_r);
           human_model.SetActive(set.h_model);
             Time.timeScale = 1f;

               
    }
    public void Load_setting()
    {   
         set= JsonUtility.FromJson<Settings_Model>( PlayerPrefs.GetString("Json_Setting"));
         Debug.Log(PlayerPrefs.GetString("Json_Setting"));
         
        Ui_Change();
        Make_change();
    }   
    public void Retry()
    {
        //Restarts current level
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void Ui_Change()
    {
        ip_m.text= set.ip_mqtt;
        port_m.text= set.port_mqtt;
        rs.isOn=set.Rs232;
        Com_in.text=set.rs232_com;
        mn.isOn=set.monitoring;
        timer_in.text=set.timer.ToString();
        hr.isOn=set.h_r;
         _hr_model.isOn=set.h_model;
    }

}
