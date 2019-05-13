/// Platformer Girl
/// CS 4332.501 - Intro to Programming Video Games
/// Assignment 2
/// 
/// Author: Jimmy Nguyen
/// Email: Jimmy@Jimmyworks.net

using UnityEngine;

/// <summary>
/// Component which makes a game object's collider fatal
/// </summary>
public class Fatal : MonoBehaviour {

    /// <summary>
    /// Trigger method which kills all objects that enter the collider
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.GetComponent<PlatformerGirl.PlayerCharacter>().kill();
    }
}
