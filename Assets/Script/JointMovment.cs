using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JointMovment : MonoBehaviour
{
    
    public List<Transform> Joints;
    public List<float> JointsAngles=new List<float>();
    public List<Vector2> AngleLimit;
    public bool update_angle=false;
    public float JOG_VAL=1f;
    bool simu_state=false;
    // Start is called before the first frame update
    void Start()
    {
        if( Joints.Count != JointsAngles.Count && AngleLimit.Count !=Joints.Count)
        {
            Debug.LogError("Joints number and Angles must be same!");
        }
    }
   
    void Apply()
    {
        if(update_angle)
        {   
                SetJointsAngles();
        }
    }
    public bool Get_Simu_State()
    {
        return simu_state;
    }
    public void SetJointAngle(int i,float Angles)
    {
        JointsAngles[i]=Angles;
    }
        public void SetJointsAngles(List<float> Angles)
    {
                if( Joints.Count != Angles.Count)
        {
            Debug.LogError("Joints number and Angles must be same!");
        }
        else{
              JointsAngles=Angles;
        }
       
    }
    public void MoveJointsTogether(List<float> angles,float ovrd)
    {
        
        simu_state=true;
           StartCoroutine(Moving(angles,ovrd));
    }
    public float NormalizedToNegativDegree(float angle)
    {
        return (angle > 180) ? angle - 360 : angle;
    }
    IEnumerator Moving(List<float> angles,float ovrd)
    {
        List<bool> JointFlag=new List<bool>();
        bool DoWhileFlag=true;
        for (int i=0; i<angles.Count;i++)
        {
            JointFlag.Add(false);
        }
         while (DoWhileFlag)
            {
                for (int i=0 ; i<angles.Count;i++)
                {   float tmpAngle=NormalizedToNegativDegree( Mathf.RoundToInt(get_joint_angles(i)));
                    float tmpToAngle=NormalizedToNegativDegree( Mathf.RoundToInt(angles[i]));
                
                    if(tmpAngle>tmpToAngle)
                    {
                            //Moving
                            SetJointAngle(i,tmpAngle-JOG_VAL);
                    }
                    else if (tmpAngle<tmpToAngle)
                    {
                             //Moving
                               SetJointAngle(i,tmpAngle+JOG_VAL);
                    }
                    else {
                            JointFlag[i]=true;
                            //   Debug.Log("FINISH Joint:"+(i+1));
                    }

                }
                    yield return new  WaitForSeconds(1/ovrd);
                    int c=0;
                 for (int i=0; i<JointFlag.Count;i++)
                 {  
                      if(!JointFlag[i])
                      {
                        c++;
                      }
                  
                 }
                     if(c==0)
                      {
                        DoWhileFlag=false;
                      }
                    
            }
            // Debug.Log("FINISH WHILE");
            simu_state=false;
       
    }
        public void SetJointsAngles()
    {
            
            for(int i=0; i < JointsAngles.Count;i++)
            {

                 Joints[i].localRotation= Quaternion.Euler(set_joint_angles(i,JointsAngles[i]));
               
            }
        
    }
    public List<float> Get_All_Joint_Angles()
    {
        List<float> angles=new List<float>();
            for(int i=0 ; i< Joints.Count;i++)
            {
                   angles.Add( get_joint_angles(i));
            }
            return angles;
    }
    
    // Todo: Check Limit
    private Vector3 set_joint_angles(int i,float Angle)
    {
        Vector3 eularJointAngle=new Vector3();
        switch (i)
        {
            case 0:
                    eularJointAngle =new Vector3(Joints[i].localEulerAngles.x,Angle,Joints[i].localEulerAngles.z);
                  
            break;
                  case 1:
                         eularJointAngle =new Vector3(Angle,Joints[i].localEulerAngles.y,Joints[i].localEulerAngles.z);
                             
            break;
                  case 2:
                             eularJointAngle =new Vector3(Angle,Joints[i].localEulerAngles.y,Joints[i].localEulerAngles.z);
            break;
                  case 3:
                         eularJointAngle =new Vector3(Joints[i].localEulerAngles.x,Joints[i].localEulerAngles.y,Angle);
            break;
                  case 4:
                                 eularJointAngle =new Vector3(Angle,Joints[i].localEulerAngles.y,Joints[i].localEulerAngles.z);
            break;
                  case 5:
                             eularJointAngle =new Vector3(Joints[i].localEulerAngles.x,Joints[i].localEulerAngles.y,Angle);
            break;
            default :
                 Debug.LogError("Somthing Went Wrong!");
                 break;
        }

        return eularJointAngle;
    }
     private float get_joint_angles(int i)
    {
        float angle=-1;
        switch (i)
        {
            case 0:

                    angle=Joints[i].localEulerAngles.y;
                  
            break;
                  case 1:
                   
                         angle=Joints[i].localEulerAngles.x;
                             
            break;
                  case 2:
                           
                             angle=Joints[i].localEulerAngles.x;
            break;
                  case 3:
                        
                         angle=Joints[i].localEulerAngles.z;
            break;
                  case 4:
                              
                                 angle=Joints[i].localEulerAngles.x;
            break;
                  case 5:
                           
                             angle=Joints[i].localEulerAngles.z;
            break;
            default :
                 Debug.LogError("Somthing Went Wrong!");
                 break;
        }

        return angle;
    }
    public Vector2 GetAngleLimit(int i){
        return AngleLimit[i];
    }
    public bool Check_angle_limit()
    {
        return true;
    }
    // Update is called once per frame
    void LateUpdate()
    {
        Apply();
    }
}
