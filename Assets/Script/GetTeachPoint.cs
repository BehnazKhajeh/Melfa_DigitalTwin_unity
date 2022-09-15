using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetTeachPoint : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public List<float> GetPoint()
    {
        List<float> point=new List<float>();
        point.Add(transform.position.x);
        point.Add(transform.position.y);
        point.Add(transform.position.z);
        return point;
    }
}
