/// Platformer Girl
/// CS 4332.501 - Intro to Programming Video Games
/// Assignment 2
/// 
/// Author: Jimmy Nguyen
/// Email: Jimmy@Jimmyworks.net

using UnityEngine;

/// <summary>
/// Component which constantly makes its owner rotate
/// </summary>
public class Revolving : MonoBehaviour {
    // Set values for x, y and z axis rotation speed
    public float xRotationSpeed, yRotationSpeed, zRotationSpeed;
	
	/// <summary>
    /// Update
    /// </summary>
	void Update () {
        transform.Rotate(new Vector3(xRotationSpeed, xRotationSpeed, zRotationSpeed));
	}
}
