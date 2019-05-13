/// Platformer Girl
/// CS 4332.501 - Intro to Programming Video Games
/// Assignment 2
/// 
/// Author: Jimmy Nguyen
/// Email: Jimmy@Jimmyworks.net

using UnityEngine;

/// <summary>
/// Abstract class for implementing collectible objects
/// </summary>
public abstract class Collectible : MonoBehaviour {

    // Locks object so it cannot be collected more than once
    private static readonly Object locker = new Object();

    // Abstract method allowing the type of collectible to implement unique behavior
    abstract public void postCollection(PlatformerGirl.PlayerCharacter input);

    /// <summary>
    /// Public interface allowing the object to be collected and implemented
    /// dynamically depending on the concrete class
    /// </summary>
    /// <param name="input"></param>
    public void collected(PlatformerGirl.PlayerCharacter input)
    {
        lock (locker)
        {
            // critical section
            postCollection(input);
        }

    }
}
