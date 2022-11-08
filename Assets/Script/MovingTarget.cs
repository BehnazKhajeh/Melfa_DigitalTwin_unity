using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RootMotion.FinalIK;
public class MovingTarget : MonoBehaviour
{
    public CCDIK cCDIK;
    public Vector3 point2go;
    public Vector3 OffsetMin;
     public Vector3 OffsetMax;
     public GameObject controller_panel;
     
    // Vector3 dir;
     Vector3 dirTmp;
    public float speed;
    bool isMoving=false;
    bool simuToggle;
    // Start is called before the first frame update
    void Start()
    {
        // Moving(point2go);
    }
    public void SetToGO(Vector3 p,float s)
    {
        
        cCDIK.enabled=true;
        
        point2go=p;
        speed=s;
        isMoving=true;
    }
    public bool IsMoving()
    {
        return isMoving;
    }
    // public IEnumerator P_MOV(Vector3 p,float s)
    // {
            
    // }
    // Update is called once per frame
    void Update()
    {   if(isMoving)
       {
        Moving(point2go);
       }
    }
   public void Moving(Vector3 p)
    {  
 
           if(controller_panel.activeSelf)
            {
             
                simuToggle=FindObjectOfType<ControllerUIManager>().Get_Simu_State();
       
                       }
                       else{simuToggle=false;
                    
                       
                       } 
                     
        dirTmp= Vector3.MoveTowards(transform.position, p, Time.deltaTime * speed);
        // Debug.Log("DirTMP:"+dirTmp);
               if(dirTmp.x>OffsetMin.x && dirTmp.y>OffsetMin.y &&
        dirTmp.z>OffsetMin.z &&dirTmp.x<OffsetMax.x &&dirTmp.y<OffsetMax.y &&dirTmp.z<OffsetMax.z )
        {
           
       transform.position = dirTmp;
        }

        if(dirTmp==p)
        {
            isMoving=false;
            
            // cCDIK.enabled=simuToggle;
               
            
        }
        }

    
}
