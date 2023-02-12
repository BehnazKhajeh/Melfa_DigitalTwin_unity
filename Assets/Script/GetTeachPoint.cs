using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetTeachPoint : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject RoboCenter;
    public Transform ToolSCenter;
    Vector3 CenterPos;
        void Start()
    {
         CenterPos=RoboCenter.GetComponent<Transform>().position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public List<float> GetPoint()
    {
        
        List<float> point=new List<float>();
        point.Add(transform.position.x-CenterPos.x);
        point.Add(transform.position.z-CenterPos.z);
        point.Add(transform.position.y-CenterPos.y);
        return point;
    }
}
