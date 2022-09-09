using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ListPanelUI_Manager : MonoBehaviour
{
  [SerializeField]
    private GameObject pointListPanel;
    public ScrollRect sc;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void refresh_Panel()
    {
                foreach (Transform PointContainer in transform) {
     GameObject.Destroy(PointContainer.gameObject);
 }
    }
    public void Listing(Points_joint p){
 
        GameObject Lnew = Instantiate<GameObject>(pointListPanel, transform);
        Lnew.GetComponent<PointListContainerManager>().initiat_container(p.joints.joint,p.joints.joint ,p.name);
        sc.verticalScrollbar.value=1;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
