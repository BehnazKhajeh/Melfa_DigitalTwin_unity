using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RootMotion.FinalIK;
    /// <summary>
    /// Handle IK
    /// </summary>
    /// <remarks>
      /// target is target to move
      /// Moving system by arrow key and rshift , rCNTRL (XYZ)
    /// </remarks>
public class IKExample : MonoBehaviour
{
   [SerializeField] private Transform target;


   public float speed = 1.0f;
    
   private CCDIK ik;
   Vector3 dir;
    // Start is called before the first frame update
    void Start()
    {
        ik=this.gameObject.GetComponent<CCDIK>();
        dir=target.position;
    }
    /// <remarks>
      /// target is target to move
      /// Moving system by arrow key and rshift , rCNTRL (XYZ)
    /// </remarks>
    public void MovingTarget()
    {
        if(Input.GetKey(KeyCode.UpArrow))
        {
           dir=new Vector3(dir.x,dir.y,dir.z+0.05f);
                 FindObjectOfType<MovingTarget>().SetToGO(dir,speed);
        }
           if(Input.GetKey(KeyCode.DownArrow))
        {
           dir=new Vector3(dir.x,dir.y,dir.z-0.05f);
              FindObjectOfType<MovingTarget>().SetToGO(dir,speed);
        }
           if(Input.GetKey(KeyCode.LeftArrow))
        {
           dir=new Vector3(dir.x-0.05f,dir.y,dir.z);
              FindObjectOfType<MovingTarget>().SetToGO(dir,speed);
        }
           if(Input.GetKey(KeyCode.RightArrow))
        {
           dir=new Vector3(dir.x+0.05f,dir.y,dir.z);
              FindObjectOfType<MovingTarget>().SetToGO(dir,speed);
        }
           if(Input.GetKey(KeyCode.RightShift))
        {
           dir=new Vector3(dir.x,dir.y+0.05f,dir.z);
               FindObjectOfType<MovingTarget>().SetToGO(dir,speed);
        }
           if(Input.GetKey(KeyCode.RightControl))
        {
            dir=new Vector3(dir.x,dir.y-0.05f,dir.z);
               FindObjectOfType<MovingTarget>().SetToGO(dir,speed);
        }
      //   Debug.Log(dir);
    }

    /// <remarks>
    /// Enable or Disable CCDIK
    /// </remarks>
    public void SetIKActivity(bool enable)
    {
        ik.enabled=enable;
     
    }
    // Update is called once per frame
    void Update()
    {
        // Debug.Log(ik.solver.bones[0].weight);
        // Debug.Log(ik.solver.bones[0].transform.localRotation);
        // Joint1.localRotation=ik.solver.bones[0].transform.localRotation;
        // Debug.Log(ik.solver.bones[1].weight);
        // Debug.Log(ik.solver.bones[2].weight);
        // Debug.Log(ik.solver.bones[3].weight);
        // Debug.Log(ik.solver.bones[4].weight);
        // Debug.Log(ik.solver.bones[5].weight);
        MovingTarget();
    }
}
