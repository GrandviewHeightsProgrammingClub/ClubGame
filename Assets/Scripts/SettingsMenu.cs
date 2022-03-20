using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    public GameObject settingsMenuUI;
    public GameObject pauseMenuUI;

    public AudioMixer audioMixer;


    public void SetFullScreen(bool isFullscreen)
    {
        Debug.Log("FullScreen Pressed");
        Screen.fullScreen = isFullscreen;
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }

    public void SetBrightness(float brightness)
    {
        Debug.Log("Brightness slider not implemented. Value: " + brightness); 
    }

    public void Back()
    {
        settingsMenuUI.SetActive(false);
        pauseMenuUI.SetActive(true);
    }
}
