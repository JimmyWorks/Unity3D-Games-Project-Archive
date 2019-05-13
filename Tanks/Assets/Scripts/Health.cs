/* Tanks
 * Multiplayer Networking Game
 * Author: Jimmy Nguyen
 * Email: jimmy@jimmyworks.net
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

/* Health
 * Health component attached to a player Tank game object
 * responsible for managing player health.  All Health
 * components are updated on the server and synchronized
 * on the clients.  Callbacks subscribed to Health
 * events are executed after the health has been updated
 * on the server and broadcasted back to the clients through
 * ClientRpc.
 */
public class Health : NetworkBehaviour {

    // Current health synchronized and hooked to OnChangeHealth function
    [SyncVar(hook = "OnChangeHealth")]
    public int currentHealth = _maxHealth;

    // Max health
    private const int _maxHealth = 3;

    // Events
    public delegate void EventHandler();
    public event EventHandler OnPlayerDeath;   // On player death event
    public event EventHandler OnPlayerDamage;  // On player damaged event
    public event EventHandler OnPlayerRevived; // On player revived event

    // Player alive flag
    [SyncVar]
    private bool alive = true;
    // Public accessor for max health
    public int maxHealth { get { return _maxHealth; } }

    /// <summary>
    /// Take Damage
    /// Deals damage to the current health of this object
    /// </summary>
    public void takeDamage()
    {
        // Update on server-side only
        if (!isServer)
            return;

        // If player alive...
        if(alive)
        {
            currentHealth--;

            // If dead
            if(currentHealth == 0)
            {
                // Set alive flag and execute client callbacks for death
                alive = false;
                RpcDied();
            }
        }    
    }

    /// <summary>
    /// Revive
    /// Revive the player game object
    /// </summary>
    public void revive()
    {
        // Update on server-side only
        if (!isServer)
            return;

        // Execute client callbacks for revive
        RpcRevive();

        // Reset flag and health counter
        alive = true;
        currentHealth = _maxHealth;
    }

    /// <summary>
    /// On Change Health
    /// Handles updates when the synchronized health counter changes
    /// </summary>
    /// <param name="health"></param>
    public void OnChangeHealth(int health)
    {
        // If health was not reset to the max value, i.e. a revive
        if(health != _maxHealth)
            OnPlayerDamage(); // Inflict damage
    }

    /// <summary>
    /// RPC Revive
    /// Have all clients execute for OnPlayerRevived event
    /// </summary>
    [ClientRpc]
    public void RpcRevive()
    {
        OnPlayerRevived();
    }

    /// <summary>
    /// RPC Died
    /// Have all clients execute for OnPlayerDeath event
    /// </summary>
    [ClientRpc]
    public void RpcDied()
    {
        OnPlayerDeath();
    }  
}
