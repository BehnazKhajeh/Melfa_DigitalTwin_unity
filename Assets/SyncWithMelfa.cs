using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyncWithMelfa : MonoBehaviour
{
    public float timeloop;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Delay(timeloop*2));
    }

    IEnumerator Delay(float time)
{
    yield return new  WaitForSeconds(time);
              FindObjectOfType<MQTT_Manager>().
          Publishing("melfa/control/posf");
          StartCoroutine(Delay(timeloop));
}
}
