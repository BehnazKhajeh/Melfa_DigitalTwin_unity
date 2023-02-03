using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GetMelfaProgrammes : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject ProgrammePanel;
    public GameObject LoadingPanel;
    public GameObject ProgrammeInputField;
    public List<GameObject> PPlace;
    public float TimeOutTimer=0.5f;
    bool num_recive=false;
    bool name_recive=false;
    int nums=0;
    int cp=0;
    void Start()
    {
         MQTT_Manager.OnMonitorMessage+=pnumz; 
         MQTT_Manager.OnMonitorMessage+=programname; 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void pnumz(string msg)
    {

          if(msg.Contains("programNumber"))
            {
               
                Programm_num_json pnum=JsonUtility.FromJson<Programm_num_json>(msg);
                num_recive=true;
                nums=pnum.num;
                Debug.Log(nums);
            }

    }
    public void CloseProgramePanel()
    {
        ProgrammePanel.SetActive(false);
        ProgrammeInputField.SetActive(true);
    }
       public void programname(string msg)
    {
                 if(msg.Contains("programName"))
            {
                cp++;
                  Programm_name_json pname=JsonUtility.FromJson<Programm_name_json>(msg);
                  name_recive=true;
                  SetNameP(cp,pname.programms);
            }
    }
    public void SetNameP(int number,string name)
    {
        PPlace[number-1].SetActive(true);
       Button btn=  PPlace[number-1].GetComponent<Button>();
        btn.GetComponent<runinmelfa>().setName(name);
    
        // btn.onClick.AddListener(OnClickBTN_S(name)); //subscribe to the onClick event
    }

    public void OpenProgrammePanel()
    {   
         FindObjectOfType<MQTT_Manager>().Publishing("melfa/control/prnum");
         ProgrammeInputField.SetActive(false);
         LoadingPanel.SetActive(true);
         StartCoroutine(WaitUntilNumRecive());
    }
    IEnumerator  TimeOut_Delay(bool isNUM)
    {
               yield return new WaitForSeconds(TimeOutTimer);
               if(isNUM){
                        if(!num_recive)
                        {
                              FindObjectOfType<MessagePanelManager>().ShowMessage("Time Out..Retry",Color.red,3,12);
                            OpenProgrammePanel();
                        }
               }
               else{
                          if(!name_recive)
                        {
                             
                           GetName_Melfa();
                        }
               }
              
    }
     IEnumerator WaitUntilNumRecive()
    {   StartCoroutine(TimeOut_Delay(true));
         yield return new WaitUntil(()=>num_recive);
         LoadingPanel.SetActive(false);
         ProgrammePanel.SetActive(true);
         GetAllProgrammeName();
    }
         IEnumerator WaitUntilNameRecive()
    {   StartCoroutine(TimeOut_Delay(false));
         yield return new WaitUntil(()=>name_recive);
         if(cp==nums)
         {
                    FindObjectOfType<MessagePanelManager>().ShowMessage(" All Programme Recive",Color.green,3,12);
         }
         else{         name_recive=false;
         GetName_Melfa();}

    }
    public void GetAllProgrammeName()
    {cp=0;
        if(nums>0)
        {
         
            string ltopictmp="pr"+(cp+1).ToString();
            FindObjectOfType<MQTT_Manager>().Publishing("melfa/control/"+ltopictmp);
            FindObjectOfType<MessagePanelManager>().ShowMessage("Waiting For Programme Number "+(cp+1),Color.green,3,12);
            StartCoroutine(WaitUntilNameRecive());
        }
        else{
            FindObjectOfType<MessagePanelManager>().ShowMessage("No Programme in Melfa "+(cp+1),Color.green,3,12);
        }
    }
    public void GetName_Melfa()
    {
       if(cp<=nums){

            string ltopictmp="pr"+(cp+1).ToString();
            FindObjectOfType<MQTT_Manager>().Publishing("melfa/control/"+ltopictmp);
            FindObjectOfType<MessagePanelManager>().ShowMessage("Waiting For Programme Number "+(cp+1),Color.green,3,12);

            StartCoroutine(WaitUntilNameRecive());
            }
       if(cp==nums){
        FindObjectOfType<MessagePanelManager>().ShowMessage(" All Programme Recive",Color.green,3,12);

            }

    }
    //     IEnumerator  _Delay(float timer )
    // {
    //               for(int i=1;i <=nums;i++)
    //         {

    //             string ltopictmp="pr"+i.ToString();
    //             FindObjectOfType<MQTT_Manager>().Publishing("melfa/control/"+ltopictmp);
    //             yield return new WaitForSeconds(timer);
    //         } 
              
    // }
}
