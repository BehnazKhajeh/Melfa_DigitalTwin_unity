using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class runinmelfa : MonoBehaviour
{
    public Text tname;
    // Start is called before the first frame update
    public string pname;
    void Start()
    {
        
    }
    public void setName(string name){
        pname=name;
        tname.text=name;
    }
    // Update is called once per frames
   public void RunInMelfaProgramm()
    {
        FindObjectOfType<MQTT_Manager>().Publishing_Payload("melfa/control/RunPrgm",tname.text);
    }
}
