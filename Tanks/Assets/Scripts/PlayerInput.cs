/* Tanks
 * Multiplayer Networking Game
 * Author: Jimmy Nguyen
 * Email: jimmy@jimmyworks.net
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

/* Player Input
 * Player Input component and primary controller for player Tanks
 * TODO: Need to refactor all non-input functionality out of this component
 */
public class PlayerInput : NetworkBehaviour {

    // Tank movement speed
    public int speed = 1;
    // Turret transform
    public Transform turret;
    // Tank body transform
    public Transform body;
    // Gravitational force 
    public float gravitationalForce = 1;
    // Character controller
    public CharacterController cc;
    // Cannon nose spawn point
    public Transform cannonNose;
    // Bullet prefab
    public GameObject bulletPrefab;
    // Firing cooldown time
    public float cdTime = 1f;
    // Bullet life time
    public float bulletTime = 3f;
    // Cooldown active timer
    private float cdTimer = 0f;
    // Fire key
    private KeyCode fireKey = KeyCode.Space;
    // Camera distance
    private float cameraDist;
    // Has focus bool
    private bool hasFocus = true;
    // Player Health component
    private Health playerHealth;
    // Spawn location
    private Vector3 spawnLocation;
    // Player movement vector
    Vector3 movement;
    // Player alive bool
    bool alive;
    // All renderer components in object
    Renderer[] renderers;
    // Bullet movement speed
    public float bulletSpeed;

    /// <summary>
    /// On Death
    /// Hides player tank when player dies 
    /// </summary>
    void OnDeath()
    {
        alive = false;
        gameObject.SetActive(false);
    }

    /// <summary>
    /// Cmd Revive Player
    /// Calls server to revive player health and respawn player tank
    /// </summary>
    [Command]
    public void CmdRevivePlayer()
    {
        playerHealth.revive();
        RpcRevivePlayer();
    }

    /// <summary>
    /// RPC Revive Player
    /// Server-to-client call to revive player tank and reset position
    /// </summary>
    [ClientRpc]
    private void RpcRevivePlayer()
    {
        alive = true;
        // Reposition tank to spawn location
        transform.position = spawnLocation;
        hasFocus = Application.isFocused;
        gameObject.SetActive(true);
    }

    /// <summary>
    /// Start
    /// </summary>
    private void Start()
    {
        // Get necessary components
        renderers = GetComponentsInChildren<Renderer>();
        playerHealth = GetComponent<Health>();

        // Subscribe to player health OnPlayerDeath event
        playerHealth.OnPlayerDeath += OnDeath;

        // Color local player and calculate camera distance
        ColorLocalPlayer();
        CalculateCameraDistance();

        // Save spawn location and set player to alive
        spawnLocation = transform.position;
        alive = true;
        
    }

    /// <summary>
    /// Color local player
    /// Colors the local player and creates the death UI notify game object
    /// </summary>
    private void ColorLocalPlayer()
    {
        if (isLocalPlayer)
        {
            // Change local unit color to blue
            foreach (Renderer rend in renderers)
            {
                rend.material.color = Color.blue;
            }

            // Create Death UI Notify for local unit death
            GameObject deathUI = (GameObject)Instantiate(Resources.Load("Prefabs/DeathNotify"));
            deathUI.GetComponent<DeathManager>().SetLocalPlayer(this);
            // Subscribe the death ui OnDeath callback to the OnPlayerDeath event
            playerHealth.OnPlayerDeath += deathUI.GetComponent<DeathManager>().OnDeath;
        }
    }

    /// <summary>
    /// Calculate camera distance
    /// Calculates the camera distance for handling mouse position to world space calculations
    /// </summary>
    private void CalculateCameraDistance()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0)), out hit))
        {
            // Camera distance to stage floor plane
            cameraDist = hit.distance;
        }
        else
        {
            Debug.LogError("Main camera not facing the stage!!!");
        }
    }

    /// <summary>
    /// On Application Focus
    /// </summary>
    /// <param name="focus"></param>
    private void OnApplicationFocus(bool focus)
    {
        hasFocus = focus;
    }

    /// <summary>
    /// Update 
    /// </summary>
    void Update()
    {
        if (!isLocalPlayer)
            return;

        // Reset the movement to be calculated for this update
        movement = new Vector3(0, 0, 0);
        float magnitude = 0;

        cdTimer = Mathf.Max(0, cdTimer - Time.deltaTime);

        // Check for player input if the character is alive
        if (alive)
        {
            // Only update the turret pointing direction if the window is in focus
            if(hasFocus)
            {
                Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cameraDist));
                Vector3 mouse = new Vector3(worldMousePos.x, turret.transform.position.y, worldMousePos.z);
                turret.transform.LookAt(mouse, Vector3.up);
            }
            
            // Apply player input
            if (Input.anyKey)
            {
                // If cooldown time is 0 and player input fire key
                if (cdTimer == 0 && Input.GetKey(fireKey))
                {
                    // Apply cooldown
                    cdTimer = cdTime;
                    // Execute firing of cannon
                    CmdFire();
                }

                // Grab the axis components
                float horizontal = Input.GetAxis("Horizontal");
                float vertical = Input.GetAxis("Vertical");
                float multiplier = 1;

                // Calculate the lateral movement based on direction the camera is facing
                Vector3 lateralUpdate = new Vector3(horizontal, 0, vertical).normalized * speed;

                // Apply the lateral movement to the next update
                movement += lateralUpdate;

                // If there is movement, rotate the character to look the direction she is moving
                if (movement.magnitude != 0)
                    body.rotation = Quaternion.LookRotation(movement);
            }
        }

        // Apply gravity
        movement += Vector3.down * gravitationalForce;

        // Move the player
        cc.Move(movement * Time.deltaTime);
    }

    /// <summary>
    /// Cmd Fire
    /// Calls the server to fire the cannon for this tank.
    /// </summary>
    [Command]
    void CmdFire()
    {
        // Instantiate a bullet
        GameObject bullet = Instantiate(
            bulletPrefab,
            cannonNose.position,
            cannonNose.rotation);

        // Add velocity to the bullet
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * bulletSpeed;

        // Spawn the bullet on the Clients
        NetworkServer.Spawn(bullet);

        // Destroy the bullet after 2 seconds
        Destroy(bullet, bulletTime);
    }
}
