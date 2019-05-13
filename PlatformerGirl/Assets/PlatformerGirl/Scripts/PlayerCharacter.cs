/// Platformer Girl
/// CS 4332.501 - Intro to Programming Video Games
/// Assignment 2
/// 
/// Author: Jimmy Nguyen
/// Email: Jimmy@Jimmyworks.net

using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PlatformerGirl
{
    /// <summary>
    /// Player Character which handles input and player state
    /// </summary>
    public class PlayerCharacter : MonoBehaviour
    {
        // Character controller for this player character
        private CharacterController cc;
        // Reference to main camera transform
        private Transform mainCamTransform;
        // Jump force, gravitational force, walk speed, and run speed
        public float jumpForce, gravitationalForce, walkSpeed, runSpeed;
        // The animator for this character
        public Animator anim;
        // Grounded checking distance
        public float distance;
        // Movement vector updated each update
        private Vector3 movement;
        // Upward force updated each update
        private float upwardForce;
        // Collected coin count
        private int coinCount = 0;
        // Remaining lives
        private int _lives = 3;
        // Boolean showing if player is alive
        private bool alive;
        // Spawn position
        public Transform spawnPoint;
        // Boolean showing if game over
        public bool gameOver = false;
        
        // Coin and lives count read interface
        public int coins { get { return coinCount; } }
        public int lives { get { return _lives; } }

        /// <summary>
        /// 
        /// </summary>
        void Start()
        {
            alive = true; // Set player to living
            cc = GetComponent<CharacterController>(); // Assign character controller
            UI_Manager.updateUI(); // Update UI
            transform.position = spawnPoint.position; // Update position to spawn position
            GameObject camera = Instantiate((GameObject)Resources.Load("MultiplayerCamera"));
            camera.transform.parent = transform.root;
            mainCamTransform = camera.transform;
        }

        /// <summary>
        /// Public method for triggering win condition
        /// </summary>
        public void Win()
        {
            UI_Manager.playWin();
            gameOver = true;
        }

        /// <summary>
        /// Update 
        /// </summary>
        void Update()
        {
            // Handle game over condition, if game over
            if (gameOver)
            {
                if(Input.GetKey(KeyCode.Return))
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                }
            }
            else
            {
                // Reset the movement to be calculated for this update
                movement = new Vector3(0, 0, 0);
                float magnitude = 0;

                // Check for player input if the character is alive
                if (alive)
                {
                    // Apply player input
                    if (Input.anyKey)
                    {
                        // Grab the axis components
                        float horizontal = Input.GetAxis("Horizontal");
                        float vertical = Input.GetAxis("Vertical");
                        float multiplier = 0;

                        // Apply multiplier if the player is running, otherwise, use walking multiplier
                        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
                            multiplier = runSpeed;
                        else
                            multiplier = walkSpeed;

                        // Calculate the lateral movement based on direction the camera is facing
                        Vector3 forward = new Vector3(mainCamTransform.forward.x, 0, mainCamTransform.forward.z).normalized;
                        Vector3 right = new Vector3(mainCamTransform.right.x, 0, mainCamTransform.right.z).normalized;
                        Vector3 lateralUpdate = (forward * vertical + right * horizontal).normalized * multiplier;
                        magnitude = lateralUpdate.magnitude;

                        // Apply the lateral movement to the next update
                        movement += lateralUpdate;

                        // If there is movement, rotate the character to look the direction she is moving
                        if (movement.magnitude != 0)
                            transform.rotation = Quaternion.LookRotation(movement);

                        // If the character is grounded and player wants to jump
                        if (anim.GetBool("Grounded") && Input.GetButton("Jump"))
                        {
                            upwardForce = jumpForce; // Apply jump force
                        }
                    }

                    // Check and update if player grounded
                    if (Physics.Raycast(transform.position, Vector3.down, distance))
                    {
                        anim.SetBool("Grounded", true);
                    }
                    else
                        anim.SetBool("Grounded", false);

                    // Check and update if player is stationary, walking, or running
                    if (magnitude == 0)
                    {
                        anim.SetInteger("Movement", 0);
                    }
                    else if (magnitude > (runSpeed + walkSpeed) / 2)
                    {
                        anim.SetInteger("Movement", 2);
                    }
                    else
                        anim.SetInteger("Movement", 1);

                    // Apply upward force to movement vector
                    movement += Vector3.up * upwardForce;

                    // On each update, gradually lerp upward force down to zero for smooth jump
                    if (upwardForce != 0)
                    {
                        if (upwardForce < gravitationalForce)
                            upwardForce = 0;
                        else
                            upwardForce = Mathf.Lerp(upwardForce, 0, 5 * Time.deltaTime);
                    }
                }
            
            }

            // Apply gravity
            movement += Vector3.down * gravitationalForce;

            // Move the player
            cc.Move(movement * Time.deltaTime);
        }

        /// <summary>
        /// Public interface allowing player to gain a coin
        /// </summary>
        public void gainCoin()
        {
            coinCount++;
            UI_Manager.updateCoins();
        }

        /// <summary>
        /// Trigger for picking up collectibles
        /// </summary>
        /// <param name="hit"></param>
        private void OnTriggerEnter(Collider hit)
        {
            if (hit.transform.root.gameObject.layer == 9)
            {
                hit.gameObject.GetComponentInParent<Collectible>().collected(this);

            }
        }

        /// <summary>
        /// Public interface for fatal events to kill the player
        /// </summary>
        public void kill()
        {
            alive = false;
            _lives--;
            upwardForce = 0;
            anim.SetTrigger("Death");

            StartCoroutine(playerDies());
        }

        /// <summary>
        /// Respawn the player
        /// </summary>
        /// <returns></returns>
        private IEnumerator playerDies()
        {
            // Pause for death animation
            yield return new WaitForSeconds(3);

            // If no more lives...
            if (_lives < 0)
            {
                // Game over
                UI_Manager.activateGameOver(true);
                gameOver = true;
            }
            else
            {
                UI_Manager.updateLives();  // update lives
                transform.position = spawnPoint.position; // update position
                anim.SetTrigger("Respawn"); // update animation
                alive = true; // set player back to living
            }
        }
    }
}
