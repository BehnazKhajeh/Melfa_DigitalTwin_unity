using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RootMotion.FinalIK;
public class MovingTarget : MonoBehaviour
{
    public CCDIK cCDIK;
  
    public Vector3 point2go;
    Vector3 dir;
    public float speed;
    bool isMoving=false;
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
    // Update is called once per frame
    void Update()
    {   if(isMoving)
       {
        Moving(point2go);
       }
    }
   public void Moving(Vector3 p)
    {   
        dir= Vector3.MoveTowards(transform.position, p, Time.deltaTime * speed);
        transform.position = dir;
       Debug.Log(dir);
        if(dir==p)
        {
            isMoving=false;
            cCDIK.enabled=false;
            
        }
    }
}
