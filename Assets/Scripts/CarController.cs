using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarController : MonoBehaviour
{
    public WheelCollider wheelColliderFL, wheelColliderFR;
    public WheelCollider wheelColliderRL, wheelColliderRR;
    public Transform wheelTransformFL, wheelTransformFR;
    public Transform wheelTransformRL, wheelTransformRR;

    public float maxSteerAngle = 30f;
    public float motorForce = 50f;
    public float maxSpeed = 180f;

    public Text speedLabel;

    private Rigidbody rb;
    private float horizontalInput;
    private float verticalInput;
    private float steeringAngle;
    private float speed;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        GetInput();
        Steer();
        Accelerate();
        UpdateWheelPoses();
    }

    private void Update()
    {
        Speedometer();
    }

    public void GetInput()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
    }

    private void Steer()
    {
        steeringAngle = maxSteerAngle * horizontalInput;
        wheelColliderFL.steerAngle = steeringAngle;
        wheelColliderFR.steerAngle = steeringAngle;
    }

    private void Accelerate()
    {
        wheelColliderFL.motorTorque = verticalInput * motorForce;
        wheelColliderFR.motorTorque = verticalInput * motorForce;
    }

    private void UpdateWheelPoses()
    {
        UpdateWheelPose(wheelColliderFL, wheelTransformFL);
        UpdateWheelPose(wheelColliderFR, wheelTransformFR);
        UpdateWheelPose(wheelColliderRL, wheelTransformRL);
        UpdateWheelPose(wheelColliderRR, wheelTransformRR);
    }

    private void UpdateWheelPose(WheelCollider wCollider, Transform wTransform)
    {
        Vector3 pos = wTransform.position;
        Quaternion quat = wTransform.rotation;

        wCollider.GetWorldPose(out pos, out quat);

        wTransform.position = pos;
        wTransform.rotation = quat;
    }

    private void Speedometer()
    {
        speed = rb.velocity.magnitude * 3.6f;
        if (speedLabel != null)
            speedLabel.text = ((int)speed) + " km/h";
    }
}
