using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Startup : MonoBehaviour
{
    public AudioSource startupAudio;
    public GameObject menu;
    public Image bootScreen;

    public Sprite bootScreenListing;
    public Sprite bootScreenListingFinished;
    public Sprite bootScreenFinished;

    void Start()
    {
        StartCoroutine(StartGame());
    }

    public IEnumerator StartGame()
    {
        startupAudio.Play();

        yield return new WaitForSeconds(0.5f);
        bootScreen.sprite = bootScreenListing;

        yield return new WaitForSeconds(2.5f);
        bootScreen.sprite = bootScreenListingFinished;

        yield return new WaitForSeconds(3.0f);
        bootScreen.sprite = bootScreenFinished;

        yield return new WaitForSeconds(2.0f);
        menu.SetActive(true);
    }
}
