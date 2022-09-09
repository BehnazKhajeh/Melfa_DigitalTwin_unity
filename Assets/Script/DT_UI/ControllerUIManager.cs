using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControllerUIManager : MonoBehaviour
{    public Dropdown myDropdown;
public GameObject jointP;
public GameObject XYZP;
public GameObject JointSliderPanel;
    int n;
   public Text percentage;
   public Slider OVRDSlider;
   public float slider_Cooldown=0.5f;
   bool isSliderChange=false;
   float prevSlider=-1;
    // Start is called before the first frame update
    void Start()
    {
             myDropdown.onValueChanged.AddListener(delegate {
         myDropdownValueChangedHandler(myDropdown);
     });
    }

   void Update() {
    
      percentage.text = OVRDSlider.value+" %";
      if(prevSlider!=OVRDSlider.value)
      {
        
        prevSlider=OVRDSlider.value;
        StartCoroutine(Pub_OVRD());

      }

   }
   IEnumerator Pub_OVRD()
   {
   
    yield return new WaitForSeconds(slider_Cooldown);
     FindObjectOfType<MQTT_Manager>().
    Publishing_Payload("melfa/control/ovrd",OVRDSlider.value.ToString());
   }
 
 void Destroy() {
     myDropdown.onValueChanged.RemoveAllListeners();
 }
 
 private void myDropdownValueChangedHandler(Dropdown target) {
   
   switch(target.value)
   {
    case 0:
    jointP.SetActive(true);
    XYZP.SetActive(false);
    JointSliderPanel.SetActive(false);
        break;
            case 1:
    jointP.SetActive(false);
    XYZP.SetActive(true);
    JointSliderPanel.SetActive(false);
        break;
            case 2:
    jointP.SetActive(false);
    XYZP.SetActive(false);
    JointSliderPanel.SetActive(true);
        break;
    }

 }
 
 public void SetDropdownIndex(int index) {
     myDropdown.value = index;
 }

}
