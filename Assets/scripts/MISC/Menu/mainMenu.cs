using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Unity.Services.Authentication;
using Unity.Services.Core;

public class mainMenu : MonoBehaviour
{
    public Slider volume;
    public AudioMixer mixer;
    public GameObject[] toggleImages;
    public LeaderboardManager leaderboardManager;
    
    public void Quit()
    {
        Application.Quit();
    }

    void Awake()
    {
        PlayerPrefs.SetInt("time", 0);
        PlayerPrefs.SetInt("checkpoint", 0);
        if (PlayerPrefs.GetInt("Start") == 0)
        {
            Debug.Log("Resetting");
            PlayerPrefs.SetInt("quality", 0);
            PlayerPrefs.SetInt("volume ",0);
            PlayerPrefs.SetInt("Start", 1);
        }
    }

        public void openLink(string link)
    {
        Application.OpenURL(link);
    }

    public void UpdateValues()
    {
        mixer.SetFloat("volume", volume.value);
        PlayerPrefs.SetFloat("volume", volume.value);
    }

    private void Start()
    {
        volume.value = PlayerPrefs.GetFloat("volume");
        mixer.SetFloat("volume", PlayerPrefs.GetFloat("volume"));
        
        Time.timeScale = 1f;

        int cameraShake = PlayerPrefs.GetInt("cameraShake");
        if (cameraShake == 1)
        {
            toggleImages[0].SetActive(false);
        }
        else
        {
            toggleImages[0].SetActive(true);
        }
        
        int motionBlur = PlayerPrefs.GetInt("motionBlur");
        if (motionBlur == 1)
        {
            toggleImages[1].SetActive(false);
        }
        else
        {
            toggleImages[1].SetActive(true);
        }
        int debugInfo = PlayerPrefs.GetInt("debugInfo");
        if (debugInfo == 1)
        {
            toggleImages[5].SetActive(true);
        }
        else
        {
            toggleImages[5].SetActive(false);
        }

        int quality = PlayerPrefs.GetInt("quality");
        switch (quality)
        {
            case 0:
                toggleImages[2].SetActive(true);
                QualitySettings.SetQualityLevel(0);
                break;
            case 1:
                toggleImages[3].SetActive(true);
                QualitySettings.SetQualityLevel(1);
                break;
            case 2:
                toggleImages[4].SetActive(true);
                QualitySettings.SetQualityLevel(2);
                break;
            default:
                toggleImages[2].SetActive(true);
                QualitySettings.SetQualityLevel(0);
                PlayerPrefs.SetInt("quality", 0);
                break;
        }
    }

    
    
    public void loadLevel(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void cameraShake()
    {
        if (toggleImages[0].activeSelf)
        {
            toggleImages[0].SetActive(false);
            PlayerPrefs.SetInt("cameraShake", 1);
        }
        else
        {
            toggleImages[0].SetActive(true);
            PlayerPrefs.SetInt("cameraShake", 0);
        }
    }
    
    public void motionBlur()
    {
        if (toggleImages[1].activeSelf)
        {
            toggleImages[1].SetActive(false);
            PlayerPrefs.SetInt("motionBlur", 1);
        }
        else
        {
            toggleImages[1].SetActive(true);
            PlayerPrefs.SetInt("motionBlur", 0);
        }
    }
    
    public void debugInfo()
    {
        if (toggleImages[5].activeSelf)
        {
            toggleImages[5].SetActive(false);
            PlayerPrefs.SetInt("debugInfo", 0);
        }
        else
        {
            toggleImages[5].SetActive(true);
            PlayerPrefs.SetInt("debugInfo", 1);
        }
    }

    public void setQuality(int lvl)
    {
        QualitySettings.SetQualityLevel(lvl);
        PlayerPrefs.SetInt("quality", lvl);
    }
}
