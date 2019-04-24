using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignController : MonoBehaviour
{
    public Camera cam;
    public Renderer[] renderer;
    public GameObject car;
    public string type;

    public void Start()
    {
        car = GameObject.Find("Car");
        cam = GameObject.Find("DashCam").GetComponent<Camera>();
        renderer = this.gameObject.GetComponentsInChildren<Renderer>();
    }

    public void Update()
    {
        if(renderer[1].isVisible)
        {
            RaycastHit raycast;
            Vector3 direction = cam.transform.position - this.transform.position;
            
            if (Physics.Raycast(transform.position, direction, out raycast))
            {
                if (raycast.collider.name == "Car" && raycast.distance < 20.0f && Vector3.Angle(car.transform.position, direction) > (90.0f+45.0f))
                {
                    Debug.DrawRay(transform.position, direction, Color.red);
                    Debug.Log("SIGN IS VISIBLE! Type: " + type + " DISTANCE: " + raycast.distance);
                }
            }
        }
    }
}
