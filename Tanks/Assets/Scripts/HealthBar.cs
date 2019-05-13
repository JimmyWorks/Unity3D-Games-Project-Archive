/* Tanks
 * Multiplayer Networking Game
 * Author: Jimmy Nguyen
 * Email: jimmy@jimmyworks.net
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* Health Bar
 * Component attached to the Health Bar UI canvas
 * which is positioned in world space to show the
 * player's current health.  Updates upon damage
 * taken and replenishes when player is spawned.
 */ 
public class HealthBar : MonoBehaviour {
    // Heart container parent panel
    public RectTransform panel;
    // Player Health component
    public Health playerHealth;
    // Heart container UI images
    private GameObject[] hearts;
    // Current active heart index
    private int activeIndex;

    /// <summary>
    /// Start Routine
    /// </summary>
    private void Start()
    {
        // Reference array to heart container UI image panels
        hearts = new GameObject[playerHealth.maxHealth];

        // Populate all hearts anew
        ReplenishHearts();

        // Subscribe to player damage and revive events
        playerHealth.OnPlayerDamage += OnDamage;
        playerHealth.OnPlayerRevived += ReplenishHearts;
    }

    /// <summary>
    /// Replenish Hearts
    /// Empties all children UI image objects in the parent panel and
    /// repopulates them with new full heart images for the max health.
    /// </summary>
    public void ReplenishHearts()
    {
        // Empty current children UI objects
        Transform[] children = panel.GetComponentsInChildren<Transform>();
        for (int i = 0; i < children.Length; i++)
        {
            if(children[i] != panel.transform)
                Destroy(children[i].gameObject);
        }

        // Populate the panel with new full heart images up to max health
        for (int i = 0; i < playerHealth.maxHealth; i++)
        {
            hearts[i] = (GameObject)Instantiate(Resources.Load("Prefabs/FullHeart"));
            hearts[i].transform.parent = panel.transform;
            hearts[i].transform.localPosition = Vector3.zero;
        }

        // Refresh current active heart index
        activeIndex = playerHealth.maxHealth - 1;

    }

    /// <summary>
    /// On Damage
    /// Updates the heart image to a blackened lost heart upon taking damage
    /// </summary>
    private void OnDamage()
    {
        Image img = hearts[activeIndex].GetComponent<Image>();
        img.sprite = Resources.Load<Sprite>("Sprites/lostheart");
        activeIndex = Mathf.Max(0, activeIndex-1);
    }
}
