/* Tanks
 * Multiplayer Networking Game
 * Author: Jimmy Nguyen
 * Email: jimmy@jimmyworks.net
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Death Manager
 * Component attached to a Death Manager game object instantiated
 * dynamically by each player's Tank game object.  Contains a
 * reference to its Tank Player Input component to allow it to
 * call the Tank's respawn routine.
 */
public class DeathManager : MonoBehaviour {
    // Game over panel
    public GameObject panel;
    // Local player referenced player input
    private PlayerInput localPlayer;

    /// <summary>
    /// Start
    /// </summary>
    private void Start()
    {
        // Ensure game over panel is disabled
        panel.SetActive(false);
    }

    /// <summary>
    /// Set Local Player
    /// Allows tank game object to pass its reference
    /// </summary>
    /// <param name="pi"> Reference to owning tank's player input </param>
    public void SetLocalPlayer(PlayerInput pi)
    {
        localPlayer = pi;
    }

    /// <summary>
    /// On Death
    /// Display the game over UI upon death
    /// </summary>
    public void OnDeath()
    {
        panel.SetActive(true);
    }

    /// <summary>
    /// On Respawn
    /// Hides game over UI and calls owning player's tank to respawn
    /// </summary>
    public void OnRespawn()
    {
        panel.SetActive(false);
        localPlayer.CmdRevivePlayer();
    }
}
