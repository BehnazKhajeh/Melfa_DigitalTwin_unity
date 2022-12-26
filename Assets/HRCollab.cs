using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HRCollab : MonoBehaviour
{
    public bool isRecordingHR=false;
    public float delayOfInit=0.5f;
    public GameObject Sphere;
    // Start is called before the first frame update
    void Start()
    {
        if (isRecordingHR)
        {
            Debug.Log("STAART");
        StartCoroutine(Instantiation_OF_Sphere(delayOfInit));
        }
    }
    IEnumerator Instantiation_OF_Sphere(float delayOfInit)
   {

    yield return new WaitForSeconds(delayOfInit);
        initsp();

       
   }
   public void initsp()
   {
  if (isRecordingHR)
  {
    
     GameObject obj=Instantiate(Sphere,this.gameObject.transform.position,this.gameObject.transform.rotation,null);
    // obj.transform.parent = null;
    //  Debug.Log("STAART22");
   StartCoroutine(Instantiation_OF_Sphere(delayOfInit));
  }
      
   }
   public void setRecHR(bool f)
   {
    isRecordingHR=f;
   }

    // Update is called once per frame
    void Update()
    {
        
    }
}
