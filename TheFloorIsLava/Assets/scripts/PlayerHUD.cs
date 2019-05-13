using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHUD : MonoBehaviour {

    public bool DEBUG;
    public CharacterState playerState;
    private Text playerLifeDisplayText;
    private Transform[] transforms;
    private Transform playerlifeTF;
    private Transform victoryDisplayTF;
    private Text victoryDisplay;
    private Transform checkpointTF;
    private Transform gameoverTF;
    private Text checkpointDisplay;
    private GameObject player;
    public int life_count;

    public Color checkpointcolor;

    private int checkpointCount;
    private float checkpointFlashInterval = 0;
    private bool checkpointOn;

    private int victoryCount;
    private float victoryFlashInterval = 0;


    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        playerState = player.GetComponent<CharacterState>();
        transforms = transform.GetComponentsInChildren<Transform>();
        if(DEBUG) Debug.Log("Transforms:\n");
        foreach(Transform tr in transforms)
        {
            if (DEBUG) Debug.Log(tr);

            if(tr.name == "LifeDisplay")
            {
                if (DEBUG) Debug.Log("Found life display\n");
                playerlifeTF = tr;
                playerLifeDisplayText = playerlifeTF.GetComponentInChildren<Text>();
            }
            else if(tr.name == "VictoryDisplay")
            {
                if (DEBUG) Debug.Log("Found victory display\n");
                victoryDisplayTF = tr;
                victoryDisplay = victoryDisplayTF.GetComponentInChildren<Text>();
                victoryDisplayTF.gameObject.SetActive(false);
            }
            else if (tr.name == "CheckpointDisplay")
            {
                if (DEBUG) Debug.Log("Found checkpoint display\n");
                checkpointTF = tr;
                checkpointDisplay = checkpointTF.GetComponentInChildren<Text>();
                checkpointOn = false;
                checkpointTF.gameObject.SetActive(false);
            }
            else if (tr.name == "GameOverDisplay")
            {
                if (DEBUG) Debug.Log("Found gameover display\n");
                gameoverTF = tr;
                gameoverTF.gameObject.SetActive(false);
            }
        }


    }
	
	// Update is called once per frame
	void Update () {
        life_count = playerState.lives;
        playerLifeDisplayText.text = "Lives: " + life_count;

        if(playerState.isWinner)
        {
            victoryDisplayTF.gameObject.SetActive(true);

            victoryFlashInterval += Time.deltaTime;
            if (victoryFlashInterval > 0.2)
            {
                if (victoryDisplay.color == Color.black)
                    victoryDisplay.color = Color.white;
                else victoryDisplay.color = Color.black;

                victoryFlashInterval = 0f;
                victoryCount++;
            }

            if(victoryCount == 10)
            {
                loadMainMenu();
            }

        }
        else if (playerState.isLoser)
        {
            gameoverTF.gameObject.SetActive(true);
            StartCoroutine(ReloadScene());
        }

        if (checkpointOn)
        {
            if(DEBUG) Debug.Log("Triggered update on checkpoint");
            checkpointFlashInterval += Time.deltaTime;
            if (checkpointFlashInterval > 0.2)
            {
                if (checkpointDisplay.color == Color.black)
                    checkpointDisplay.color = Color.white;
                else checkpointDisplay.color = Color.black;

                checkpointFlashInterval = 0f;
                checkpointCount++;
            }

            if (checkpointCount == 5)
            {
                checkpointOn = false;
                checkpointTF.gameObject.SetActive(false);
            }
        }
        
        
	}

    private void loadMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    IEnumerator ReloadScene()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void checkpointON()
    {
        checkpointFlashInterval = 0f;
        checkpointOn = true;
        checkpointTF.gameObject.SetActive(true);
    }

}
