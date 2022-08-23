using System.Collections;
using System.Collections.Generic;
using UnityEngine;
     
    public class CameraMovment : MonoBehaviour {
     
        public float zoomspeed = 5.0f;
        public GameObject targetobject;
        public float rotatespeed = 10.0f;
        private Vector3 point;
     float dist ;
        void Start()
        {
            point = targetobject.transform.position;
            transform.LookAt(point);
        }
     
        void Update () {
            //Rotate the Entire Camera around the relevant Object
      
     
     
            if (Input.GetKey(KeyCode.D))
            {
            //    transform.position += transform.right * zoomspeed * Time.deltaTime;
                  transform.RotateAround(point, new Vector3(0.0f, -1.0f, 0.0f), 20 * Time.deltaTime * rotatespeed);
     
            //Find the distance between starting and end points when zoom the camera
             dist = Vector3.Distance(transform.position, point);
            }
     
            if (Input.GetKey(KeyCode.A))
     
            {
            //    transform.position -= transform.right * zoomspeed * Time.deltaTime;
           transform.RotateAround(point, new Vector3(0.0f, -1.0f, 0.0f), 20 * Time.deltaTime * -rotatespeed);
     
            //Find the distance between starting and end points when zoom the camera
             dist = Vector3.Distance(transform.position, point);
            }
     
            //Zoom in and Zoom Out
            if (Input.GetKey(KeyCode.W))
            {
               
                    transform.position += transform.forward * zoomspeed * Time.deltaTime;
                    
                
         
            }
     
            if (Input.GetKey(KeyCode.S))
            {
               
                    transform.position -= transform.forward * zoomspeed * Time.deltaTime;
              
                
            }
        }
    }
