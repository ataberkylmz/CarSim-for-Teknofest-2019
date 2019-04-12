using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public WheelCollider w_frontDriver;
    public WheelCollider w_frontPassenger;
    public WheelCollider w_backDriver;
    public WheelCollider w_backPassenger;

    public Transform t_frontDriver;
    public Transform t_frontPassenger;
    public Transform t_backDriver;
    public Transform t_backPassenger;

    private float horizontalInput;
    private float verticalInput;
    private bool handBrake;
    private float steeringAngle;
    public float maxSteeringAngle = 30.0f;
    public float torque = 50.0f;

    public void UpdateWheelPose (WheelCollider _wCollider, Transform _wTransform) {
        Vector3 _pos = _wTransform.position;
        Quaternion _quat = _wTransform.rotation;

        _wCollider.GetWorldPose(out _pos, out _quat);

        _wTransform.position = _pos;
        _wTransform.rotation = _quat;

    }

    private void FixedUpdate() {
        // Get the input from the user such as left, right, gas, brake and hand brake.
        // Input.GetAxis returns a float value between 0 and 1.0f, Input.GetKey returns a bool.
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        handBrake = Input.GetKey(KeyCode.Space);

        // Set the current steering angle with respect to max angle and user input.
        // Then update the WheelColliders' steering angles.
        steeringAngle = maxSteeringAngle * horizontalInput;
        w_frontDriver.steerAngle = steeringAngle;
        w_frontPassenger.steerAngle = steeringAngle;

        // If hand brake is not applied, apply the torque to the WheelColliders, else apply brake torque.
        if(handBrake != true) {
            w_frontDriver.motorTorque = torque * verticalInput;
            w_frontPassenger.motorTorque = torque * verticalInput;
            w_frontDriver.brakeTorque = 0;
            w_frontPassenger.brakeTorque = 0;
            w_backDriver.brakeTorque = 0;
            w_backPassenger.brakeTorque = 0;
        }
        else {
            w_frontDriver.motorTorque = 0;
            w_frontPassenger.motorTorque = 0;
            w_frontDriver.brakeTorque = torque * 1000;
            w_frontPassenger.brakeTorque = torque * 1000;
            w_backDriver.brakeTorque = torque * 1000;
            w_backPassenger.brakeTorque = torque * 1000;
        }

        // Update all the wheel positions according to the WheelColliders' positions.
        UpdateWheelPose(w_frontDriver, t_frontDriver);
        UpdateWheelPose(w_frontPassenger, t_frontPassenger);
        UpdateWheelPose(w_backDriver, t_backDriver);
        UpdateWheelPose(w_backPassenger, t_backPassenger);

        // Calculate the Km/h.
        // WARNING! -THIS APPROACH MIGH BE WRONG AS WE DO NOT KNOW THE WHEEL RADIUS' UNIT!
        // IT IS ASSUMED TO BE IN MILLIMETER(MM) THEN CONVERTED TO CANTIMETER(CM) IN THE CALCULATION!.
        //Debug.Log("Km/h: " + (w_backDriver.rpm * w_backDriver.radius * 100 * 0.001885));
        //Debug.Log("RPM: " + w_backDriver.rpm);
        //Debug.Log("Steering Angle: " + steeringAngle);
    }

    public float getSteeringAngle()
    {
        return steeringAngle;
    }
}       
