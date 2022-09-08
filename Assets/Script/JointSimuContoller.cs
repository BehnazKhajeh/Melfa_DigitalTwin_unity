using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;
public class JointSimuContoller : MonoBehaviour
{
    [SerializeField ] private Text jointName;
    //  [SerializeField ] private Text jointAngleText;
    [SerializeField ] private InputField jointAngle;
     [SerializeField ] private Slider jointSlider;
    private Vector2 Anglelimit;
     int num;
     float AngleValue;
    // Start is called before the first frame update
  void Start()
    {   
       
              int.TryParse( Regex.Replace(jointName.text, "[^0-9]",""),out num);
              Anglelimit=FindObjectOfType<JointMovment>().GetAngleLimit(num-1);
              jointSlider.minValue=Anglelimit.x;
               jointSlider.maxValue=Anglelimit.y;
              
    }
public void OnSliderChange()
{
    AngleValue=jointSlider.value;
    jointAngle.text=jointSlider.value.ToString();
}
public void OnInputfeildChange()
{    
    float jointAngleVal;
     float.TryParse( jointAngle.text,out jointAngleVal);
    if(jointAngleVal > Anglelimit.y || jointAngleVal < Anglelimit.x)
     {
        jointAngle.textComponent.color=Color.red;
        AngleValue=jointSlider.value;
     }else{
          jointAngle.textComponent.color=Color.black;
        AngleValue=jointAngleVal;
        jointSlider.value=AngleValue;}
      
    
}
   void Update() {
      if(!FindObjectOfType<JointMovment>().Get_Simu_State())
      {FindObjectOfType<JointMovment>().SetJointAngle(num-1,AngleValue);}
    
   }
}
