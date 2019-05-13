/// Platformer Girl
/// CS 4332.501 - Intro to Programming Video Games
/// Assignment 2
/// 
/// Author: Jimmy Nguyen
/// Email: Jimmy@Jimmyworks.net

using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using PlatformerGirl;

/// <summary>
/// UI Manager singleton component for managing UI interactions
/// </summary>
public class UI_Manager : MonoBehaviour {
    // Class singleton
    private static UI_Manager singleton;
    // Text elements for displaying coin and life count
    public Text coinText, livesText;
    // Game objects for panels the UI manager needs to control
    public GameObject coinCounter, livesCounter, splash, gameOver, winPanel;
    // Reference to the player character
    public PlayerCharacter player;

	/// <summary>
    /// Start
    /// </summary>
	void Start () {
        if(singleton != null)
            return;
        singleton = this;
	}

    /// <summary>
    /// Update UI elements
    /// </summary>
    public static void updateUI()
    {
        updateLives();
        updateCoins();
    }

    /// <summary>
    /// Update player life count display
    /// </summary>
    public static void updateLives()
    {
        singleton.livesText.text = "x " + singleton.player.lives;
    }

    /// <summary>
    /// Update coin count display
    /// </summary>
    public static void updateCoins()
    {
        singleton.coinText.text = "x " + singleton.player.coins;
    }

    /// <summary>
    /// Show all the UI elements
    /// </summary>
    /// <param name="on"></param>
    public static void activateUI(bool on)
    {
        singleton.livesCounter.SetActive(on);
        singleton.coinCounter.SetActive(on);
    }

    /// <summary>
    /// Routine when stage intro ends
    /// </summary>
    public static void exitOpening()
    {
        activateUI(true);
        singleton.splash.SetActive(false);
    }
    
    /// <summary>
    /// UI events when game over
    /// </summary>
    /// <param name="on"></param>
    public static void activateGameOver(bool on)
    {
        singleton.gameOver.SetActive(on);
    }

    /// <summary>
    /// Ui events when game won
    /// </summary>
    public static void playWin()
    {
        singleton.winPanel.SetActive(true);
    }

}
