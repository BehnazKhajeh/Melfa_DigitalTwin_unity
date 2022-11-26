using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyncWithMelfa : MonoBehaviour
{
    public float timeloop;
    public bool isSync=false;
    // Start is called before the first frame update
    void Start()
    {
        if(isSync){
        StartCoroutine(Delay(timeloop*2));
        }
    }

    IEnumerator Delay(float time)
{
    yield return new  WaitForSeconds(time);
              FindObjectOfType<MQTT_Manager>().
          Publishing("melfa/control/posf");
          StartCoroutine(Delay(timeloop));
}
}
