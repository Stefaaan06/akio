using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerUIManager : MonoBehaviour
{
    public GameObject pauseMenu;

    public buttonHover[] hoverEffects;
    
    public GameObject continueButton;
    public Stopwatch timer;

    
    bool dead = false;
    public bool paused = false;


    public void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    public void death()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        timer.saveTime();
        pause(true); // Pass true to keep the cursor visible
        continueButton.SetActive(false);
        dead = true;
    }

        
    public void restart()
    {
        paused = false;
        PlayerPrefs.SetInt("time", 0);
        PlayerPrefs.SetInt("checkpoint", 0);
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    public void checkpoint()
    {
        paused = false;
        if(PlayerPrefs.GetInt("checkpoint") != 0)
        {
            timer.saveTime();
        }
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void backToMenu()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void pause(bool showCursor = true)
    {
        paused = true;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = showCursor; // Set cursor visibility based on the parameter

        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }
    
    public void continueLevel()
    {
        paused = false;
        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
        pauseMenu.SetActive(false);

        foreach (buttonHover b in hoverEffects)
        {
            b.reset();
        }
        
        Time.timeScale = 1f;
    }

    public void Update()
    {
        if(dead) return;
        if (Input.GetKeyDown(KeyCode.Escape) && !pauseMenu.activeSelf)
        {
            pause();
        }
        else if(Input.GetKeyDown(KeyCode.Escape))
        {
            continueLevel();
        }
        
        if(Input.GetKeyDown(KeyCode.R))
        {
            restart();
        }
        if(Input.GetKeyDown(KeyCode.C))
        {
            checkpoint();
        }
    }
}
