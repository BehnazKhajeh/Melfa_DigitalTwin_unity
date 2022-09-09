using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PointListContainerManager : MonoBehaviour
{
    public List<Text> joints=new List<Text>();
     public List<Text> xyzs=new List<Text>();
     public Text Jname;
    // Start is called before the first frame update
    public void initiat_container(List<float> joints_,List<float> xyz_,string name_)
    {
        Jname.text=name_;
        for(int i=0 ; i<joints_.Count;i++)
        {
            joints[i].text=joints_[i].ToString();
            xyzs[i].text=xyz_[i].ToString();
        }
    }
    public void Delete_point()
    {
        FindObjectOfType<TeachPointManager>().Delete_Point(Jname.text);
    }
}
