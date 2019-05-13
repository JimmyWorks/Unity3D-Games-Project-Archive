/// Project: The Legend of Doggo
/// Author: Jimmy Nguyen
/// Email: tbn160230@utdallas.edu | Jimmy@Jimmyworks.net

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


/// <summary>
/// Start game component
/// </summary>
public class startGame : MonoBehaviour {

    // Method to load the first stage
	public void loadGame()
    {
        SceneManager.LoadScene("Stage_1");
    }
}
