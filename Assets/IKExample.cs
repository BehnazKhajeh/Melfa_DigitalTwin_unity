using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RootMotion.FinalIK;

public class IKExample : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField]private Vector3 Axis;
    [SerializeField]private float Angle;
    [SerializeField]private Transform Joint1;
    public float speed = 1.0f;
    
   private CCDIK ik;
   
    // Start is called before the first frame update
    void Start()
    {
        ik=this.gameObject.GetComponent<CCDIK>();
       
       
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(ik.solver.bones[0].weight);
        Debug.Log(ik.solver.bones[0].transform.localRotation);
        Joint1.localRotation=ik.solver.bones[0].transform.localRotation;
        Debug.Log(ik.solver.bones[1].weight);
        Debug.Log(ik.solver.bones[2].weight);
        Debug.Log(ik.solver.bones[3].weight);
        Debug.Log(ik.solver.bones[4].weight);
        Debug.Log(ik.solver.bones[5].weight);
    }
}
