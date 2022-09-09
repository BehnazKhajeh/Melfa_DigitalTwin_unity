using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;

public class TeachPointManager : MonoBehaviour
{
    private DataManager data=new DataManager();
    
    public Text JointText;
    List<float> jointAngle;
    public InputField name_Box;
      PointByJoint joi=new PointByJoint();
      public GameObject pointPanel;

    // Start is called before the first frame update
    void Start()
    {
      
    }
    void WriteJointInText()
    {   jointAngle=FindObjectOfType<JointMovment>().Get_All_Joint_Angles();
    //  Mathf.Round(jointAngle[0]* 100.0f) * 0.01f
        // string tmpText;
        // FindObjectOfType<JointMovment>().NormalizedToNegativDegree(Mathf.Round(jointAngle[0]* 100.0f))
        JointText.text=
        "J1="+ FindObjectOfType<JointMovment>().NormalizedToNegativDegree(Mathf.Round(jointAngle[0]* 100.0f)* 0.01f)+
        " J2="+FindObjectOfType<JointMovment>().NormalizedToNegativDegree(Mathf.Round(jointAngle[1]* 100.0f)* 0.01f)+
        " J3="+FindObjectOfType<JointMovment>().NormalizedToNegativDegree(Mathf.Round(jointAngle[2]* 100.0f)* 0.01f)+
        " J4="+FindObjectOfType<JointMovment>().NormalizedToNegativDegree(Mathf.Round(jointAngle[3]* 100.0f)* 0.01f)+
        " J5="+FindObjectOfType<JointMovment>().NormalizedToNegativDegree(Mathf.Round(jointAngle[4]* 100.0f)* 0.01f)+
        " J6="+FindObjectOfType<JointMovment>().NormalizedToNegativDegree(Mathf.Round(jointAngle[5]* 100.0f)* 0.01f);
        // JointText.text=tmpText;
    }
    // Update is called once per frame
    public void SavePoint()
    {
         string pointName=name_Box.text;
         bool duplicate_f=false;
         Joints_ j=new Joints_();
         Points_joint pp=new Points_joint();
         pp.joints=new Joints_();
         joi.points=new List<Points_joint>();
              if(pointName.Equals(""))
        {
            FindObjectOfType<MessagePanelManager>().ShowMessage(" Please enter point name");
        }
        else{
       string tmpdata=data.loadjson("JointPoint");
   Debug.Log(tmpdata);
       if(tmpdata.Equals(""))
       {
        data.savejson("JointPoint","");

       }else{
        joi=JsonConvert.DeserializeObject<PointByJoint>(tmpdata);
    
        for (int i=0 ;i<joi.points.Count;i++)
        {
             if(   joi.points[i].name.Equals(pointName))
             {
                duplicate_f=true;
             }
           
        }
       }
        if(duplicate_f)
        {
            FindObjectOfType<MessagePanelManager>().ShowMessage("'"+pointName+"' name is already exist!");
        }
        else{
            //  PointByJoint p=new PointByJoint();
            j.joint=new List<float>();
                   for (int i=0 ;i<jointAngle.Count;i++)
        {

           j.joint.Add(jointAngle[i]);
        }
            //  j.joint[0]=jointAngle[0];
            //  j.joint2=jointAngle[1];
            //  j.joint3=jointAngle[2];
            //  j.joint4=jointAngle[3];
            //  j.joint5=jointAngle[4];
            //  j.joint6=jointAngle[5];
             pp.name=pointName;
             pp.joints=j;
             joi.points.Add(pp);

             data.savejson("JointPoint", JsonConvert.SerializeObject(joi));
             FindObjectOfType<MessagePanelManager>().ShowMessage("'"+pointName+"' is sucessfully added",Color.green,1,10);
        }
        }
    }
    public void GetPointList()
    {
         Joints_ j=new Joints_();
         Points_joint pp=new Points_joint();
         pp.joints=new Joints_();
         joi.points=new List<Points_joint>();
         
        string tmpdata=data.loadjson("JointPoint");
   pointPanel.SetActive(true);
        joi=JsonConvert.DeserializeObject<PointByJoint>(tmpdata);
        FindObjectOfType<ListPanelUI_Manager>().refresh_Panel();
        for (int i=0 ;i<joi.points.Count;i++)
        {
              Debug.Log( joi.points[i].name);
             FindObjectOfType<ListPanelUI_Manager>().Listing(joi.points[i]);
        }
       
    }
    public void Delete_Point(string pname)
    {
         Joints_ j=new Joints_();
         Points_joint pp=new Points_joint();
         pp.joints=new Joints_();
         joi.points=new List<Points_joint>();
         
        string tmpdata=data.loadjson("JointPoint");
        pointPanel.SetActive(true);
        joi=JsonConvert.DeserializeObject<PointByJoint>(tmpdata);
        FindObjectOfType<ListPanelUI_Manager>().refresh_Panel();
        for (int i=0 ;i<joi.points.Count;i++)
        {
            //   Debug.Log( joi.points[i].name);
            if(joi.points[i].name.Equals(pname))
            {
                joi.points.RemoveAt(i);
            }

        }
          data.savejson("JointPoint", JsonConvert.SerializeObject(joi));
         GetPointList();
    }
    void Update()
    {
   
     WriteJointInText();
        
    }
}
