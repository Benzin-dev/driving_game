using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheels : MonoBehaviour
{
    [Header("Suspention")]
    public float restLenght;
    public float springTravel;
    public float springStiffness;
    public float damperStiffness;

    [Header("Wheel")]
    public float wheelRadius;

    private Rigidbody rb;
    private float minLenght;
    private float maxLenght;
    private float lastLenght;
    private float springLenght;
    private float springForce;
    private float springVelocity;
    private float damperForce;


    private Vector3 suspentionForce;

    void Start()
    {
        rb = transform.root.GetComponent<Rigidbody>();

        minLenght = restLenght - springTravel;
        maxLenght = restLenght + springTravel;
    }

    void FixedUpdate()
    {
        if (Physics.Raycast(transform.position, -transform.up, out RaycastHit hit, maxLenght + wheelRadius))
        {
            lastLenght = springLenght;

            springLenght = hit.distance - wheelRadius;
            springLenght = Mathf.Clamp(springLenght, minLenght, maxLenght);

            springVelocity = (lastLenght - springLenght) / Time.fixedDeltaTime;
            springForce = springStiffness * (restLenght - springLenght);
            damperForce = damperStiffness * springVelocity;
            
            suspentionForce = (springForce + damperForce) * transform.up;
            rb.AddForceAtPosition(suspentionForce, hit.point);
        }
    }
}
