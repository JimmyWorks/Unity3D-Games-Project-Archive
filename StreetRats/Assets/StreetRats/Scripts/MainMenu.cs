using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StreetRats;

public class MainMenu : MonoBehaviour {

    void Update()
    {
        if (Input.anyKey)
        {
            SceneManager.ProcessEvent(GameTransition.LOAD_LVL1);
        }
    }
}
