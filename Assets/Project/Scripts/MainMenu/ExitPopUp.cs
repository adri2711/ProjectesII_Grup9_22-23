using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitPopUp : MonoBehaviour
{
    public bool showAgain = true;
    public GameObject exitPopUp;
    public AudioSource exitSound;

    public void DontShowAgain()
    {
        showAgain = false;
    }

    public void SetActive()
    {
        if (showAgain)
        {
            transform.GetChild(0).gameObject.SetActive(true);
            //exitPopUp.SetActive(true);
        }
        else
            QuitGame();
    }

    public void QuitGame()
    {
        StartCoroutine(ApplicationQuit());
    }

    private IEnumerator ApplicationQuit()
    {
        exitSound.Play();
        yield return new WaitWhile(() => exitSound.isPlaying);
    }
}
