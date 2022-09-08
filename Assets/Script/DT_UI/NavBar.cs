using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavBar : MonoBehaviour
{
    public List<GameObject> Layouts;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ChangeLayout(GameObject btn)
    {   
       
        for(int i=0;Layouts.Count>i;i++){
        if(Layouts[i].name==btn.name)
        {
            if( Layouts[i].activeSelf){
              Layouts[i].SetActive(false);
            }else{
                Layouts[i].SetActive(true);
            }
        }else{
           
            Layouts[i].SetActive(false);
        }
        
        }
    }
}
