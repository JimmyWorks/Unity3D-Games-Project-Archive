using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour {

    public Slider volumeSlider;

    public void UpdateVolume()
    {
        //AudioListener.volume = volumeSlider.value;
        Debug.Log("Volume: " + volumeSlider.value);
    }
}
