using System.Collections;
using System.Collections.Generic;
using UnityEngine;
     
    public class CameraMovment : MonoBehaviour {
     
        public Vector3 offset=new Vector3(0f,5f,0f);
        public Vector2 ZoomOffset=new Vector2(5f,20f);
        
        public float zoomspeed = 5.0f;
        public float rotatespeed = 10.0f;
        public GameObject targetobject;
        
        private Vector3 point;
     float dist ;
        void Start()
        {
            point = targetobject.transform.position+offset;
           transform.LookAt(point);
        }
     
        void Update () {
            //Rotate the Entire Camera around the relevant Object
       
      
     
            if (Input.GetKey(KeyCode.D))
            {
        
            //    transform.position += transform.right * zoomspeed * Time.deltaTime;
                  transform.RotateAround(point, new Vector3(0.0f, -1.0f, 0.0f), 20 * Time.deltaTime * rotatespeed);
     
            //Find the distance between starting and end points when zoom the camera
           
         
            }
     
            if (Input.GetKey(KeyCode.A))
     
            {
            //    transform.position -= transform.right * zoomspeed * Time.deltaTime;
           transform.RotateAround(point, new Vector3(0.0f, -1.0f, 0.0f), 20 * Time.deltaTime * -rotatespeed);
     
            //Find the distance between starting and end points when zoom the camera
            
            }
     
            //Zoom in and Zoom Out
            if (Input.GetKey(KeyCode.W))
            {
                 dist = Vector3.Distance(transform.position, point);
                
                           if (dist>ZoomOffset.x)
             {
                      transform.position += transform.forward * zoomspeed * Time.deltaTime;
             }
                  
                    
                
         
            }
     
            if (Input.GetKey(KeyCode.S))
            {  dist = Vector3.Distance(transform.position, point);
            
                                if (dist<ZoomOffset.y)
             {
                    transform.position -= transform.forward * zoomspeed * Time.deltaTime;
             }
                
            }
        }
    }
