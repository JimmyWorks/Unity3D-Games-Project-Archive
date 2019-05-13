using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterState : MonoBehaviour {

	public int lives;
    public float jumpForce;
    public float speed;

    public Vector3 lastSavePoint;
    private GameObject level;
    public bool isDead { get; private set; }
    public bool isWinner { get; private set; }
    public bool isLoser { get; private set; }

    private void Start()
    {
        isWinner = isDead = isLoser = false;
        level = GameObject.FindGameObjectWithTag("LevelDetails");

        Transform[] transforms = level.GetComponentsInChildren<Transform>();
        lastSavePoint = transforms[1].position;

        spawnPlayer();
    }

    private void spawnPlayer()
    {
        transform.position = lastSavePoint;
    }

    private void Update()
    {
        if(isDead)
        {
            Debug.Log("Player died...\n");
            //wait 3 seconds
            if(lives >= 0)
            {
                Debug.Log("Respawning...\n");
                isDead = false;
                spawnPlayer();
            }
            else
            {
                Debug.Log("GAME OVER\n");
                isLoser = true;
            }
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Lethal")
        {
            isKilled();
        }
        else if(other.tag == "Respawn")
        {
            Debug.Log("Checkpoint!\n");
            lastSavePoint = other.GetComponentInParent<Transform>().position;
        }
        else if(other.tag == "Finish")
        {
            Debug.Log("You Win!\n");
            isWinner = true;
        }

    }

    public void isKilled()
    {
        Debug.Log("The player is killed...\n");
        isDead = true;
        //play dead animation + sounds
        lives--;
    }
}
