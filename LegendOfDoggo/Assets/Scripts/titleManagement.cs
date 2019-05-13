/// Project: The Legend of Doggo
/// Author: Jimmy Nguyen
/// Email: tbn160230@utdallas.edu | Jimmy@Jimmyworks.net

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Title management component which manages the order of title screens.
/// </summary>
public class titleManagement : MonoBehaviour {

    // Sprite for logo
    public SpriteRenderer logo;
    // Main Menu parent game object
    public GameObject mainMenu;

	/// <summary>
    /// Start initialization routine
    /// Call intro coroutine
    /// </summary>
	void Start () {
        StartCoroutine(IntroCoroutine());
    }

    /// <summary>
    /// Intro coroutine which fades in/out the logo and activates start menu
    /// </summary>
    /// <returns></returns>
    private IEnumerator IntroCoroutine()
    {
        // Set background to black
        Camera.main.backgroundColor = Color.black;

        // Fade in the logo
        for (int i = 0; i < 100; i += 5)
        {
            logo.color = new Color(1f, 1f, 1f, i / 100.0f);
            yield return new WaitForSeconds(0.05f);
        }

        // Pause
        yield return new WaitForSeconds(1f);

        // Fade out the logo
        for (int i = 100; i > 0; i -= 5)
        {
            logo.color = new Color(255, 255, 255, i / 100.0f);
            yield return new WaitForSeconds(0.05f);
        }

        // Change background color and bring up the start menu
        Camera.main.backgroundColor = new Color(255f/255f, 248f/255f, 160f/255f, 1);
        mainMenu.SetActive(true);
    }
}
