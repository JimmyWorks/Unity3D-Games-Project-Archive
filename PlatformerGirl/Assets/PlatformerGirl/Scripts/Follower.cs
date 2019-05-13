/// Platformer Girl
/// CS 4332.501 - Intro to Programming Video Games
/// Assignment 2
/// 
/// Author: Jimmy Nguyen
/// Email: Jimmy@Jimmyworks.net

using UnityEngine;

/// <summary>
/// Follower script to anchor an object to another
/// </summary>
public class Follower : MonoBehaviour {

    // Transform being followed
    public Transform leader;
    // Offset variables
    public float x, y, z;
	
	/// <summary>
    /// Update this objects with respect to the leader
    /// </summary>
	void Update () {
        transform.position = leader.position + new Vector3(x, y, z);
        transform.LookAt(leader);
        transform.Rotate(Vector3.right, -15f);
	}
}
