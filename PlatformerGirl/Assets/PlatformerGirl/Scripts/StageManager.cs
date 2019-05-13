/// Platformer Girl
/// CS 4332.501 - Intro to Programming Video Games
/// Assignment 2
/// 
/// Author: Jimmy Nguyen
/// Email: Jimmy@Jimmyworks.net

using UnityEngine;

/// <summary>
/// Stage manager component which manages opening the scene
/// and coordinating UI and player character creation
/// </summary>
public class StageManager : MonoBehaviour {
    // player character
    private static GameObject character;
    // main camera
    public Camera maincam;
    // follower component of main camera
    private Follower follower;
    // stage preview component of main camera
    private StagePreview preview;

    /// <summary>
    /// On awake, get all camera components disabling the player follower script
    /// </summary>
    private void Awake()
    {
        character = GameObject.FindGameObjectWithTag("Player");
        follower = maincam.GetComponent<Follower>();
        preview = maincam.GetComponent<StagePreview>();
        follower.enabled = false;
    }

    public static GameObject GetPlayer()
    {
        return character;
    }

    /// <summary>
    /// Update routine
    /// If any input is read, disable the preview and enable the player follower script.
    /// Make the UI manager exit the opening routine.
    /// Set player active and destroy this script.
    /// </summary>
    void Update () {
		if(Input.anyKey)
        {
            if(preview != null)
            {
                preview.enabled = false;
                CameraController cc = maincam.GetComponent<CameraController>();
                if (cc != null)
                    Destroy(cc);
            }
   
            follower.enabled = true;
            UI_Manager.exitOpening();
            character.SetActive(true);
            Destroy(this);
        }
	}
}
