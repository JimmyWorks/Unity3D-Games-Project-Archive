/// Platformer Girl
/// CS 4332.501 - Intro to Programming Video Games
/// Assignment 2
/// 
/// Author: Jimmy Nguyen
/// Email: Jimmy@Jimmyworks.net

using UnityEngine;

/// <summary>
/// Component responsible for animating the camera when the stage is loaded
/// </summary>
public class StagePreview : MonoBehaviour {
    // Center the camera will be rotating around
    public Transform center;
    // Offset distance, rotation speed, elevation speed, and max height for
    // the cylinder the camera will be rotating around
    public float offsetDistance, rotSpeed, elevationSpeed, maxHeight;
    // Updating radial angle where the camera is currently located on the circle
    private float angle = 0;
    // Updating height where the camera ia currently located on the cylinder
    private float height = 0;
    // Boolean indicating direction the camera is traveling
    private bool headingUp = true;
	
	/// <summary>
    /// Update
    /// </summary>
	void Update () {
        // On each update, lerp the angle towards 720 degrees
        angle = Mathf.Lerp(angle, 720, rotSpeed * Time.deltaTime);

        // If the angle is larger than 360, throttle it down a full 360 degrees
        if (angle > 360)
            angle -= 360;

        // If heading up
        if(headingUp)
        {
            // Lerp the height toward the max height + 10
            height = Mathf.Lerp(height, maxHeight + 10, elevationSpeed * Time.deltaTime);

            // Upon reaching max height, switch directions
            if(height > maxHeight)
            {
                headingUp = false;
            }
        }
        else
        {
            // Lerp the height toward the floor - 10
            height = Mathf.Lerp(height, -10, elevationSpeed * Time.deltaTime);

            // Upon reaching the floor, switch directions
            if (height < 0)
                headingUp = true;
        }

        // Update the position
        transform.position = center.position + Quaternion.AngleAxis(angle, Vector3.up) * Vector3.forward * offsetDistance;
        transform.position += Vector3.up * height;

        // Update the rotation so that the camera is looking in the right direction
        transform.LookAt(center.position + (Vector3.up * height));
	}
}
