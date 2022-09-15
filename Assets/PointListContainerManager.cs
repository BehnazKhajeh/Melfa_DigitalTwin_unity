using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PointListContainerManager : MonoBehaviour
{
    public List<Text> joints=new List<Text>();
     public List<Text> xyzs=new List<Text>();
     public Text Jname;
     List<float> joint_Angle=new List<float>();
     List<float> XYZ_Angle=new List<float>();
    // Start is called before the first frame update
    public void initiat_container(List<float> joints_,List<float> xyz_,string name_)
    {
        Jname.text=name_;
        for(int i=0 ; i<joints_.Count;i++)
        {
            joints[i].text=joints_[i].ToString();
            if(i<3)
            {
            xyzs[i].text=xyz_[i].ToString();
            }
        }
        joint_Angle=joints_;
        XYZ_Angle=xyz_;
    }
    public void Delete_point()
    {
        FindObjectOfType<TeachPointManager>().Delete_Point(Jname.text);
    }
    public void MoveJoint()
    {
        FindObjectOfType<JointMovment>().MoveJointsTogether(joint_Angle,FindObjectOfType<ControllerUIManager>().GetOvrd());
    }
    public void MoveXYZ(){
        FindObjectOfType<MovingTarget>().SetToGO(new Vector3(XYZ_Angle[0],XYZ_Angle[1],XYZ_Angle[2]),FindObjectOfType<ControllerUIManager>().GetOvrd()/10);
    }
}
