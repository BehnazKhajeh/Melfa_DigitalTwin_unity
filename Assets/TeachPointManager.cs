using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;

public class TeachPointManager : MonoBehaviour
{
    private DataManager data=new DataManager();
    
    public Text JointText;
    public Text XYZText;
    List<float> jointAngle;
    List<float>  XYZPoint;
    public InputField name_Box;
    PointByJoint joi=new PointByJoint();
    PointByXYZ xyzz=new PointByXYZ();
    public GameObject pointPanel;
    
    // Start is called before the first frame update
    void Start()
    {
        //       data.savejson("JointPoint","");
        // data.savejson("XYZPoint","");
    }
    void WriteJointInText()
    {   jointAngle=FindObjectOfType<JointMovment>().Get_All_Joint_Angles();
        JointText.text=
        "J1="+ FindObjectOfType<JointMovment>().NormalizedToNegativDegree(Mathf.Round(jointAngle[0]* 100.0f)* 0.01f)+
        " J2="+FindObjectOfType<JointMovment>().NormalizedToNegativDegree(Mathf.Round(jointAngle[1]* 100.0f)* 0.01f)+
        " J3="+FindObjectOfType<JointMovment>().NormalizedToNegativDegree(Mathf.Round(jointAngle[2]* 100.0f)* 0.01f)+
        " J4="+FindObjectOfType<JointMovment>().NormalizedToNegativDegree(Mathf.Round(jointAngle[3]* 100.0f)* 0.01f)+
        " J5="+FindObjectOfType<JointMovment>().NormalizedToNegativDegree(Mathf.Round(jointAngle[4]* 100.0f)* 0.01f)+
        " J6="+FindObjectOfType<JointMovment>().NormalizedToNegativDegree(Mathf.Round(jointAngle[5]* 100.0f)* 0.01f);
               XYZPoint=FindObjectOfType<GetTeachPoint>().GetPoint();
                XYZText.text=
        "X="+ XYZPoint[0]+
        " Y"+XYZPoint[1]+
        " Z="+XYZPoint[2]+
        " A="+
        " B="+
        " C=";
 
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
         ///
         XYZ_ xyz=new XYZ_();
         Points_XYZ pxyz=new Points_XYZ();
         pxyz.point=new XYZ_();
         xyzz.points=new List<Points_XYZ>();
         ///
              if(pointName.Equals(""))
        {
            FindObjectOfType<MessagePanelManager>().ShowMessage(" Please enter point name");
        }
        else{
       string tmpdata=data.loadjson("JointPoint");
           string tmpdataXYZ=data.loadjson("XYZPoint");
//    Debug.Log(tmpdata);
       if(tmpdata.Equals(""))
       {
        data.savejson("JointPoint","");
        data.savejson("XYZPoint","");
       }else{
        joi=JsonConvert.DeserializeObject<PointByJoint>(tmpdata);
        xyzz=JsonConvert.DeserializeObject<PointByXYZ>(tmpdataXYZ);
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
            ///Saving Joint
            j.joint=new List<float>();
                   for (int i=0 ;i<jointAngle.Count;i++)
                 {

                      j.joint.Add(jointAngle[i]);
                 }

             pp.name=pointName;
             pp.joints=j;
             joi.points.Add(pp);
            //  Debug.Log(pp.joints.joint);
            //  Debug.Log(JsonConvert.SerializeObject(joi));
             data.savejson("JointPoint", JsonConvert.SerializeObject(joi));
          
       
            /// Saving XYZ
            xyz.point=new List<float>();
            for (int i=0 ;i<XYZPoint.Count;i++)
                 {

                      xyz.point.Add(XYZPoint[i]);
                 }
             pxyz.name=pointName;
             pxyz.point=xyz;
             xyzz.points.Add(pxyz);
            //  Debug.Log(pxyz.point.point);
            //  Debug.Log(JsonConvert.SerializeObject(xyzz));
             data.savejson("XYZPoint", JsonConvert.SerializeObject(xyzz));
          

               FindObjectOfType<MessagePanelManager>().ShowMessage("'"+pointName+"' is sucessfully added",Color.green,1,10);
        }
        }
    }
    public void GetPointList()
    {   ///Joint
         Joints_ j=new Joints_();
         Points_joint pp=new Points_joint();
         pp.joints=new Joints_();
         joi.points=new List<Points_joint>();
         
         ///XYZ
         XYZ_ xyz=new XYZ_();
         Points_XYZ pxyz=new Points_XYZ();
         pxyz.point=new XYZ_();
         xyzz.points=new List<Points_XYZ>();
         ///
        string tmpdata=data.loadjson("JointPoint");
        string tmpdataXYZ=data.loadjson("XYZPoint");
        if(!tmpdata.Equals("")){
        joi=JsonConvert.DeserializeObject<PointByJoint>(tmpdata);
        xyzz=JsonConvert.DeserializeObject<PointByXYZ>(tmpdataXYZ);
        pointPanel.SetActive(true);
      
        FindObjectOfType<ListPanelUI_Manager>().refresh_Panel();
        for (int i=0 ;i<joi.points.Count;i++)
        {
            // Debug.Log( joi.points[i].name);
             FindObjectOfType<ListPanelUI_Manager>().Listing(joi.points[i],xyzz.points[i]);
        }
        }else{ pointPanel.SetActive(true);}
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
