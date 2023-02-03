using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

 using UnityEngine.UI;
public class MessagePanelManager : MonoBehaviour
{
    public TextMeshProUGUI messageText;
     public GameObject messageBox;
     Image img;
    // Start is called before the first frame update
    void Start()
    {
        img= messageBox.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ShowMessage(string message,Color color,float duration,float font_size)
    { 
        img.color=new Color(0.5f,1,1,0.6f);
        messageText.text="";
        messageText.color=color;
        messageText.fontSize=font_size;
        messageText.text=message;
        StartCoroutine(FadedBox(duration));
    }
        public void ShowMessage(string message)
    { 
        img.color=new Color(0.5f,1,1,0.6f);
        messageText.text="";
        messageText.color=Color.red;
        messageText.fontSize=9;
        messageText.text=message;
        StartCoroutine(FadedBox(2f));
    }
    IEnumerator FadedBox(float Time)
    {

        //      while (Time>0)
        // {
        //     Image img= messageBox.GetComponent<Image>();
        //     img.color=new Color(0,0,0,0);
        //     yield return new WaitForSeconds(1f);
        //     Time--;
        // }

        
        yield return new WaitForSeconds(Time);
        img.color=new Color(0,0,0,0);
        messageText.color=new Color(0,0,0,0);  
        messageText.text="";
    }
}
