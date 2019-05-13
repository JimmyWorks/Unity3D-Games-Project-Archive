/// Platformer Girl
/// CS 4332.501 - Intro to Programming Video Games
/// Assignment 2
/// 
/// Author: Jimmy Nguyen
/// Email: Jimmy@Jimmyworks.net

using PlatformerGirl;
using UnityEngine;

/// <summary>
/// Magic cube object which is the primary objective of the stage
/// </summary>
public class MagicCube : Collectible {

    /// <summary>
    /// Implementation after collection
    /// </summary>
    /// <param name="input"></param>
    public override void postCollection(PlayerCharacter input)
    {
        input.Win(); // Make the player win
        Destroy(transform.root.gameObject);
    }
}
