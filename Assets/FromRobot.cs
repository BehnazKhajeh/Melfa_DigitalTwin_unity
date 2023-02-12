using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FromRobot : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Melfa;
public TextMesh dist;
 public float movespeed = 5.0f;
Vector3 melfaPosInxyz;
    void Start()
    {
         melfaPosInxyz=Melfa.GetComponent<Transform>().position;
        Vector2 melfaPos=new Vector2(melfaPosInxyz.x,melfaPosInxyz.y);
        // Vector3 HumanPos=this.gameObject.GetComponent<Transform>().position;
    }
 

    // Update is called once per frame
    void Update()
    {
                 if (Input.GetKey(KeyCode.K))
            {
                transform.position += transform.right * movespeed * Time.deltaTime;
            }
                             if (Input.GetKey(KeyCode.J))
            {
                 transform.position -= transform.right * movespeed * Time.deltaTime;
            }
                                         if (Input.GetKey(KeyCode.M))
            {  transform.position += transform.forward * movespeed * Time.deltaTime;

            }                             if (Input.GetKey(KeyCode.N))
            {  transform.position -= transform.forward * movespeed * Time.deltaTime;

            }
           

     float distx = transform.position.x - melfaPosInxyz.x;
float distz = transform.position.z - melfaPosInxyz.z;
        // Debug.Log(distx +"     "+distz);
        dist.text="Distance From Robot(cm) is X:"+_2cm(distx).ToString()+" and Y:"+_2cm(distz).ToString();
    }

    public float _2cm(float dist)
    {
        float tmp_abs=Mathf.Abs(dist);
        float tmpceil=Mathf.Floor(tmp_abs);
        float f= (Mathf.Ceil((tmp_abs-tmpceil)*100)/100)+tmpceil;
        return f*10;
    }
}
