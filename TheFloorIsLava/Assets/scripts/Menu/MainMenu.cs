using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private string url = "http://www.jimmyworks.net/pages/games.php";


    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {

    #if (UNITY_EDITOR || DEVELOPMENT_BUILD)
        Debug.Log(this.name + " : " + this.GetType() + " : " + System.Reflection.MethodBase.GetCurrentMethod().Name);
    #endif
    #if (UNITY_EDITOR)
        UnityEditor.EditorApplication.isPlaying = false;
    #elif (UNITY_STANDALONE)
        Application.Quit();
    #elif (UNITY_WEBGL)
        Application.OpenURL(url);
    #endif

    }
}
