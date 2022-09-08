using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation_Limit : MonoBehaviour
{
    [SerializeField] private Vector3 Axis;
    [SerializeField]private float Angle;
    private Transform Rotaion;
   
//    [SerializeField]  private Transform targetPos;


    // [SerializeField] 
    // Start is called before the first frame update
    void Start()
    {
        Rotaion =this.gameObject.GetComponent<Transform>();
        Debug.Log(transform.rotation.eulerAngles.y);
    }


    public void rotattate()
    {
        
        // transform.Rotate(Axis,Space.Self);
        transform.localRotation= Quaternion.Euler(Angle,transform.rotation.eulerAngles.y,transform.rotation.eulerAngles.z);
    }
    public void SetAngel()
    {
         transform.localRotation= Quaternion.Euler(transform.rotation.eulerAngles.x,Angle,transform.rotation.eulerAngles.z);
    }

    // Update is called once per frame
    void LateUpdate()
    {

// transform.localRotation= Quaternion.Euler(transform.rotation.eulerAngles.x,Angle,transform.rotation.eulerAngles.z);
//  Vector3 rot = Rotaion.rotation.eulerAngles;
//   Vector3 rot = Rotaion.rotation.eulerAngles;
//  rot = new Vector3(rot.x,rot.y+180,rot.z);
//  Rotaion.rotation = Quaternion.Euler(rot);
//  Rotaion.localRotation = Quaternion.Euler(new Vector3(Axis.x,Rotaion.rotation.y,Axis.z));
//  Debug.Log("Rotaion.rotation"+Rotaion.rotation);
// Debug.Log("Rotaion.localEulerAngles"+Rotaion.localEulerAngles);
// Debug.Log("Rotaion.localRotation"+Rotaion.localRotation);
// Debug.Log("Rotaion.eulerAngles"+Rotaion.eulerAngles);

//  transform.Rotate(Axis,Angle,Space.Self);
   transform.localRotation= Quaternion.Euler(Angle,transform.localEulerAngles.y,transform.localEulerAngles.z);
//  transform.RotateAround( new Vector3(transform.localRotation.x,Angle,transform.localRotation.z),Axis,Angle);

    }
}
