using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTNGetTeachPoint : MonoBehaviour
{
    // Start is called before the first frame update
    private List<float> p1;
    private List<float> p2;
    public float OVRD=50;
    private Vector3 pT1;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void GetP1()
    {   pT1=FindObjectOfType<GetTeachPoint>().GetPoint();
            p1=FindObjectOfType<JointMovment>().Get_All_Joint_Angles();
                    for (int i=0 ; i<p1.Count;i++)
        {
         Debug.Log(p1[i]);
        }
    }
      public void GetP2()
    {
         p2=FindObjectOfType<JointMovment>().Get_All_Joint_Angles();
                            for (int i=0 ; i<p2.Count;i++)
        {
         Debug.Log(p2[i]);
        }
    }
    public void Go_P1_XYZ()
    {
FindObjectOfType<MovingTarget>().SetToGO(pT1,OVRD/50);
    }
    public void Go_P1()
    {
         FindObjectOfType<JointMovment>().MoveJointsTogether(p1,OVRD);
        // FindObjectOfType<JointMovment>().MoveJointsTogether()
        // List<float> ff=FindObjectOfType<JointMovment>().Get_All_Joint_Angles();
        // for (int i=0 ; i<ff.Count;i++)
        // {
        //  Debug.Log(ff[i]);
        // }
      
        // Debug.Log(FindObjectOfType<GetTeachPoint>().GetPoint());
        
    }
       public void Go_P2()
    {
    
        FindObjectOfType<JointMovment>().MoveJointsTogether(p2,OVRD);
    }
}
