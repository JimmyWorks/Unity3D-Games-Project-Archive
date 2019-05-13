using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour {

    // player character
    public GameObject character;
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
        follower = maincam.GetComponent<Follower>();
        preview = maincam.GetComponent<StagePreview>();
        follower.enabled = false;
    }

    /// <summary>
    /// Update routine
    /// If any input is read, disable the preview and enable the player follower script.
    /// Make the UI manager exit the opening routine.
    /// Set player active and destroy this script.
    /// </summary>
    void Update()
    {
        if (Input.anyKey)
        {
            preview.enabled = false;
            follower.enabled = true;
            UI_Manager.exitOpening();
            character.SetActive(true);
            Destroy(this);
        }
    }

}
