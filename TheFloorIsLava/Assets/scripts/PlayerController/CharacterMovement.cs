using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour {

    public bool DEBUG;

    public CharacterController cc;
    public EnvironmentProperties env;
    public float maxFallSpeed;

    public int awakeFramerate;
    public int framerate = 0;
    private int frameAcc = 0;
    private float deltaSubSeconds = 0f;
    public float deltaX = 0f;
    public float deltaY = 0f;
    public float deltaZ = 0f;

    // For debugging framerate-dependent behavior
    void Awake()
    {
    #if UNITY_EDITOR
        QualitySettings.vSyncCount = 0;  // VSync must be disabled
        Application.targetFrameRate = awakeFramerate;
    #endif
    }

    // Update is called once per frame
    void FixedUpdate () {
        if(DEBUG) Debug.Log("X: " + deltaX + " Y: " + deltaY + " Z: " + deltaZ + "\n");
        applyGravity();
        cc.Move(new Vector3(deltaX, deltaY, deltaZ));
        if (DEBUG) Debug.Log("post move X: " + deltaX + " Y: " + deltaY + " Z: " + deltaZ + "\n");

        updateFramerate();
        stopLateralMotion();
    }

    private void updateFramerate()
    {
        frameAcc++;
        deltaSubSeconds += Time.deltaTime;
        if (deltaSubSeconds > 1)
        {
            framerate = frameAcc;
            frameAcc = 0;
            deltaSubSeconds = 0f;
        }
    }

    private void stopLateralMotion()
    {
        deltaX = 0f;
        deltaZ = 0f;
    }

    void applyGravity()
    {
        deltaY -= env.gravity * Time.deltaTime;

        // Check for max fall speed
        if (deltaY < maxFallSpeed)
            deltaY = maxFallSpeed;
    }
}
