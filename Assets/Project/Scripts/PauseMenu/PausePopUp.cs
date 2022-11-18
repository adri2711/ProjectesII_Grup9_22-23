using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausePopUp : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public static bool gameIsPaused;

    void Start()
    {
        InitialSetup();
    }

     void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused)
            {
                Resume();
            } else
            {
                Pause();
            }
        }
    }

    void InitialSetup()
    {
            gameIsPaused = false;
            pauseMenuUI.SetActive(false);
}

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }
}
