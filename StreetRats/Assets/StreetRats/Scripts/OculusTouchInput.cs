using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus;

public class OculusTouchInput : MonoBehaviour {

    private GameObject player;
    private Motorcycle motorcycle;
    private Transform parentTransform, tiltAxis;
    private Rigidbody parentRigidbody;
    private float lastRotation, nextRotation;
    public float turnSensitivity = 0.1f;
    public float minRotation = -60;
    public float maxRotation = 60;
    public float topSpeed, acceleration, brakingPower, gravitationalForce;
    public float turningFactor;

    private Vector3 nextVelocity;
    private Vector3 LTouchPos, RTouchPos, ZRef;
    private Quaternion RTouchRot;
    private float turning;

    //public Transform sphere1, sphere2;
    

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        parentTransform = player.transform;
        parentRigidbody = player.GetComponent<Rigidbody>();
        motorcycle = player.GetComponent<Motorcycle>();
        tiltAxis = motorcycle.TiltAxis;

        lastRotation = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // Update Oculus Touch Controller values
        OVRInput.Update();

        LTouchPos = OVRInput.GetLocalControllerPosition(OVRInput.Controller.LTouch);
        RTouchPos = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch);
        RTouchRot = OVRInput.GetLocalControllerRotation(OVRInput.Controller.RTouch);

        float angle = Vector2.Angle(new Vector2(LTouchPos.x, LTouchPos.z - 1) - new Vector2(LTouchPos.x, LTouchPos.z), new Vector2(RTouchPos.x, RTouchPos.z) - new Vector2(LTouchPos.x, LTouchPos.z));

        if(angle != 0)
        {
            if (angle < 10)
                angle = 10;
            else if (angle > 170)
                angle = 170;
        }

        turning = (angle - 90) * turnSensitivity;

        Debug.Log("Angle: " + angle);

        // Reset next velocity
        nextVelocity = Vector3.zero;


        lastRotation = tiltAxis.rotation.eulerAngles.x;
        if (lastRotation > 180)
            lastRotation -= 360;


        nextRotation = lastRotation;
        //Debug.Log("Last rotation: " + lastRotation + " Mouse X is: " + mouseX);

        if (turning != 0)
        {
            nextRotation += turning;

            if (nextRotation < minRotation)
            {
                nextRotation = minRotation;
            }

            else if (nextRotation > maxRotation)
                nextRotation = maxRotation;
        }

        if (OVRInput.Get(OVRInput.Button.One))
        {
            Debug.Log("Accelerating...");
            nextVelocity += parentTransform.forward * acceleration * Time.deltaTime;

        }

        if (OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger) > 0)
        {
            Debug.Log("Braking...");

            Vector3 back = -parentTransform.forward * brakingPower * Time.deltaTime;
            if (Vector3.Dot(parentRigidbody.velocity, parentTransform.forward) < back.magnitude)
                nextVelocity += Vector3.Dot(parentRigidbody.velocity, parentTransform.forward) * -parentTransform.forward.normalized;
            else
                nextVelocity += back;
        }

        if (OVRInput.Get(OVRInput.Button.Three))
        {
            Debug.Log("Sliding...");
        }

        if (OVRInput.Get(OVRInput.Button.Four))
        {
            Debug.Log("Jumping...");
        }

        // Apply forces
        //cc.Move(movement);
        //player.position += movement * Time.deltaTime;

    }

    private void FixedUpdate()
    {
        OVRInput.FixedUpdate();

        // Modify velocity
        parentRigidbody.velocity += Quaternion.AngleAxis(nextRotation * turningFactor, Vector3.up) * nextVelocity;

        float distanceTraveled = parentRigidbody.velocity.magnitude * Time.fixedDeltaTime;

        parentTransform.rotation *= Quaternion.AngleAxis(nextRotation * distanceTraveled * turningFactor, Vector3.up);
        
        //Debug.Log("Tilt ABS rotation x: " + tiltAxis.rotation.x + " y: " + tiltAxis.rotation.y + " z: " + tiltAxis.rotation.z);
        //Debug.Log("Tilt LOCAL rotation x: " + tiltAxis.localRotation.x + " y: " + tiltAxis.localRotation.y + " z: " + tiltAxis.localRotation.z);

        if (turning != 0)
            tiltAxis.localRotation = Quaternion.Euler(nextRotation, 90, tiltAxis.rotation.z);


    }
}
