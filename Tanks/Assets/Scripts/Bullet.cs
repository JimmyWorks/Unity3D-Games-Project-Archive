/* Tanks
 * Multiplayer Networking Game
 * Author: Jimmy Nguyen
 * Email: jimmy@jimmyworks.net
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

/* Bullet
 * Component attached to the Bullet game object to handle
 * collisions which are managed on server-side only.
 */
public class Bullet : NetworkBehaviour {

    /// <summary>
    /// On Collision Enter
    /// </summary>
    /// <param name="collision"> Collision </param>
    private void OnCollisionEnter(Collision collision)
    {
        // Only handle on server-side
        if(isServer)
        {
            // Destroy the bullet game object
            Destroy(gameObject);

            // If the collided object has a health component
            Health hp = collision.gameObject.GetComponent<Health>();
            if (hp != null)
            {
                // Apply damage
                hp.takeDamage();
            }
        }
        
    }
}
