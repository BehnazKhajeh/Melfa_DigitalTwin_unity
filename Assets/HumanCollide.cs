using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanCollide : MonoBehaviour
{
    public float lifeTime=20f;
    public Material MaterialRef;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
     void OnCollisionEnter(Collision other) {
        Debug.Log("collide");
        if(other.gameObject.tag=="Human")
        {
     GetComponent<Renderer>().material = MaterialRef;
        Debug.Log("COLLLIDE HUMANNN");
        }
    }
    IEnumerator life()
    {
       yield return new WaitForSeconds(lifeTime);
       Destroy(this.gameObject);
    
    }
}
