using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ProgrammingManager : MonoBehaviour
{
    public TMP_InputField codeBox;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void Send2Melfa()
    {
        string tmp=codeBox.text;
        FindObjectOfType<MQTT_Manager>().Publishing_Payload("melfa/control/prgrm",tmp);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
