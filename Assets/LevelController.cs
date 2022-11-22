using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class LevelController : MonoBehaviour
{
    public GameObject player;
    private bool isGamePaused = false;
    public AudioMixer audioMixer;
    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            Cursor.lockState = CursorLockMode.None;
        }
        
        if (Input.GetKeyUp(KeyCode.LeftAlt) && !isGamePaused)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void OnPause()
    {
        player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        isGamePaused = true;
    }

    public void OnPlay()
    {
        player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        Cursor.lockState = CursorLockMode.Locked;
        isGamePaused = false;
    }
    
    public void QualitySettings(int quality)
    {
        switch (quality)
        {
            case 0:
                UnityEngine.QualitySettings.SetQualityLevel(0);
                break;
            case 1:
                UnityEngine.QualitySettings.SetQualityLevel(1);
                break;
            case 2:
                UnityEngine.QualitySettings.SetQualityLevel(2);
                break;
            case 3:
                UnityEngine.QualitySettings.SetQualityLevel(3);
                break;
        }
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("MasterVolume", volume);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
