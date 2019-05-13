using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseAndKeyboardInput : MonoBehaviour {

    private GameObject player;
    private Motorcycle motorcycle;
    private Transform parentTransform, tiltAxis;
    private Rigidbody parentRigidbody;

    private float lastRotation, nextRotation;
    public float minRotation = -60;
    public float maxRotation = 60;
    public float topSpeed, acceleration, brakingPower, gravitationalForce;
    public float turningFactor;

    private Vector3 nextVelocity;
    private float mouseX;

	// Use this for initialization
	void Start () {

        player = GameObject.FindWithTag("Player");
        Debug.Log("Found player:" + player);
        parentTransform = player.transform;
        parentRigidbody = player.GetComponent<Rigidbody>();
        motorcycle = player.GetComponent<Motorcycle>();
        Debug.Log(motorcycle);
        tiltAxis = motorcycle.TiltAxis;

        lastRotation = 0;
	}
	
	// Update is called once per frame
	void Update () {

        // Reset next velocity
        nextVelocity = Vector3.zero;

        // 
        mouseX = Input.GetAxis("Mouse X");
        lastRotation = tiltAxis.rotation.eulerAngles.x;
        if (lastRotation > 180)
            lastRotation -= 360;


        nextRotation = lastRotation;
        //Debug.Log("Last rotation: " + lastRotation + " Mouse X is: " + mouseX);

        if (mouseX != 0)
        {
             nextRotation += mouseX;

            if (nextRotation < minRotation)
            {
                nextRotation = minRotation;
            }

            else if (nextRotation > maxRotation)
                nextRotation = maxRotation;
        }

        if (Input.GetKey(KeyCode.A))
        {
            Debug.Log("Accelerating...");
            nextVelocity += parentTransform.forward * acceleration * Time.deltaTime;

        }

        if (Input.GetKey(KeyCode.S))
        {
            Debug.Log("Braking...");

            Vector3 back = -parentTransform.forward * brakingPower * Time.deltaTime;
            if (Vector3.Dot(parentRigidbody.velocity, parentTransform.forward) < back.magnitude)
                nextVelocity += Vector3.Dot(parentRigidbody.velocity, parentTransform.forward) * -parentTransform.forward.normalized;
            else
                nextVelocity += back;
        }

        if (Input.GetKey(KeyCode.D))
        {
            Debug.Log("Sliding...");
        }

        if (Input.GetKey(KeyCode.Space))
        {
            Debug.Log("Jumping...");
        }

        // Apply forces
        //cc.Move(movement);
        //player.position += movement * Time.deltaTime;

    }

    private void FixedUpdate()
    {
        // Modify velocity
        parentRigidbody.velocity += Quaternion.AngleAxis(nextRotation*turningFactor, Vector3.up) * nextVelocity;

        float distanceTraveled = parentRigidbody.velocity.magnitude * Time.fixedDeltaTime;


            parentTransform.rotation *= Quaternion.AngleAxis(nextRotation * distanceTraveled * turningFactor, Vector3.up);

        if(mouseX != 0)
            tiltAxis.localRotation = Quaternion.Euler(nextRotation, 90, tiltAxis.rotation.z);
            
        

    }
}
