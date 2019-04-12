using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIControler : MonoBehaviour
{
    public GameObject steeringAngleField;
    private float steeringAngle;

    // Update is called once per frame
    void Update()
    {
        steeringAngle = GameObject.Find("Car").GetComponent<CarController>().getSteeringAngle();
        steeringAngleField.GetComponent<Text>().text = steeringAngle.ToString();
    }
}
