using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public Wheels[] wheels;

    [Header("Car Parameters")]
    public float wheelBase;
    public float rearTrack;
    public float turnRadius;

    [Header("Input")]
    public float steerInput;

    private float ackermannAngleLeft;
    private float ackermannAngleRight;


    void Update()
    {
        steerInput = Input.GetAxis("Horizontal");

        if (steerInput > 0)
        {
            // turn right
            ackermannAngleLeft = Mathf.Rad2Deg * Mathf.Atan(wheelBase / (turnRadius + (rearTrack / 2))) * steerInput;
            ackermannAngleRight = Mathf.Rad2Deg * Mathf.Atan(wheelBase / (turnRadius - (rearTrack / 2))) * steerInput;
        }
        else if (steerInput < 0)
        {
            // turn left
            ackermannAngleLeft = Mathf.Rad2Deg * Mathf.Atan(wheelBase / (turnRadius - (rearTrack / 2))) * steerInput;
            ackermannAngleRight = Mathf.Rad2Deg * Mathf.Atan(wheelBase / (turnRadius + (rearTrack / 2))) * steerInput;
        }
        else
        {
            ackermannAngleLeft = 0f;
            ackermannAngleRight = 0f;
        }

        foreach (Wheels w in wheels)
        {
            if (w.wheelFrontLeft)
                w.steerAngle = ackermannAngleLeft;
            if (w.wheelFrontRight)
                w.steerAngle = ackermannAngleRight;
        }
    }
}
