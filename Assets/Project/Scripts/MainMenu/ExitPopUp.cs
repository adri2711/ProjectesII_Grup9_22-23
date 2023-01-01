using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitPopUp : MonoBehaviour
{
    public bool showAgain = true;
    public GameObject exitPopUp;

    public void DontShowAgain()
    {
        showAgain = false;
    }

    public void SetActive()
    {
        if (showAgain)
            exitPopUp.SetActive(true);
        else
            QuitGame();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
