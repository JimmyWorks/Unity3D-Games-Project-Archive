using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StreetRats;

public class WinnerBox : MonoBehaviour {

    public GameTransition nextScene;

    /// <summary>
    /// Handles event when player enters the victory box
    /// </summary>
    /// <param name="other"> Collider entering winner box </param>
    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.root.gameObject.CompareTag("Player"))
        {
            StartCoroutine(LoadNextScene());
        }
    }

    /// <summary>
    /// Coroutine which loads next scene
    /// </summary>
    /// <returns></returns>
    IEnumerator LoadNextScene()
    {
        yield return new WaitForSeconds(1f);
        CameraManager.DestroyCamera();
        SceneManager.ProcessEvent(nextScene);
    }

}
