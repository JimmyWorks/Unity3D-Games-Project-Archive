/// Project: The Legend of Doggo
/// Author: Jimmy Nguyen
/// Email: tbn160230@utdallas.edu | Jimmy@Jimmyworks.net

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Simple Controller
/// Simple controller component attached to the main,
/// player-controlled character.  This controller class
/// checks and handles player input.  Character state
/// is also managed and the Animator component updated
/// accordingly.
/// </summary>
public class SimpleController : MonoBehaviour
{

    // Character state: if the character is grounded
    private bool isGrounded = false;
    // Character state: if the character is sleeping
    private bool isSleeping = false;
    // Character lateral movement speed
    public float speed = 1;
    // Character jump force magnitude
    public float jumpForce = 1;
    // Character animator
    private Animator animator;
    // Accumulated time since last movement
    public float idleTimer = 0f;
    // Max idle time before sleep animation played
    public float idleTriggerTime = 5f;
    // Distance from center of character to bottom of feet
    private float raycastGroundedDistance = 1.1f;

    /// <summary>
    /// Start initialization routine
    /// </summary>
    void Start()
    {
        // Get animator and set the character as facing forward (right)
        animator = GetComponent<Animator>();
        animator.SetBool("isForward", true);
    }

    /// <summary>
    /// Update initialization routine
    /// </summary>
    void Update()
    {

        // Update the grounded state before proceeding
        updateGrounded();

        // Initialize lateral movement and jumping delta
        float lateralMovement = 0f;
        float jumping = 0f;

        // If left input, update delta value
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            lateralMovement--;
        }

        // If right input, update delta value
        if (Input.GetKey(KeyCode.RightArrow))
        {
            lateralMovement++;
        }

        // If able to jump, increment jumping
        if (Input.GetKey(KeyCode.UpArrow) && isGrounded)
        {
            jumping++;
        }

        // If jumping, update state and animator
        if (jumping > 0)
        {
            isGrounded = false;
            animator.SetBool("isGrounded", false);
        }

        // If moving laterally, update animator
        if (lateralMovement > 0)
        {
            animator.SetBool("isForward", true);
            animator.SetBool("isMoving", true);
        }
        else if (lateralMovement < 0)
        {
            animator.SetBool("isForward", false);
            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }

        // If no movement, update the sleep timer
        if (jumping == 0 && lateralMovement == 0)
        {
            if (!isSleeping)
                idleTimer += Time.deltaTime;
        }
        else
        {
            isSleeping = false;
            idleTimer = 0;
        }

        // If no sleeping and idle time exceeded, put the player to sleep
        if (!isSleeping && idleTimer > idleTriggerTime)
        {
            animator.SetTrigger("isSleeping");
            isSleeping = true;
        }

        // Finally, update the player's position
        transform.position += new Vector3(lateralMovement * speed, jumping * jumpForce);

    }

    /// <summary>
    /// Checks and updates if the game object is grounded
    /// </summary>
    private void updateGrounded()
    {
        //Debug.DrawLine(transform.position, transform.position + new Vector3(0f, -1 * raycastGroundedDistance), Color.cyan);

        // Raycast down from center of character a set distance to check if feet touching ground
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, raycastGroundedDistance, LayerMask.GetMask("Ground"));

        // If no collision with ground objects, object is not grounded
        if (hit.collider != null)
        {
            Debug.Log("Is grounded");
            isGrounded = true;
            animator.SetBool("isGrounded", true);
        } // Otherwise, grounded
        else
        {
            Debug.Log("Not grounded");
            isGrounded = false;
            animator.SetBool("isGrounded", false);
        }
    }

}
