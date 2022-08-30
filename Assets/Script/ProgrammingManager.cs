using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ProgrammingManager : MonoBehaviour
{
    public TMP_InputField codeBox;
    public TMP_InputField nameBox;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void Send2Melfa()
    {
        string tmpcode=codeBox.text;
string tmpname=nameBox.text;
        if (!tmpcode.Equals("")&&!tmpname.Equals(""))
        {
            FindObjectOfType<MQTT_Manager>().Publishing_Payload("melfa/control/prgrm/"+tmpname.ToUpper(),tmpcode.ToUpper());
        }
        else{
            FindObjectOfType<MessagePanelManager>().ShowMessage("Fill Code and Name Box",Color.red,3f,14);
        }
        
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
