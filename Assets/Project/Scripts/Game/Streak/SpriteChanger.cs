using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteChanger : MonoBehaviour
{
    public GameObject quarterSprite;
    public GameObject halfSprite;
    public GameObject threeQuartersSprite;
    public GameObject wholeSprite;
    public GameObject powerUpSprite;

    private void Start()
    {
        GameEvents.instance.streakFreeKeys += Special;
    }
    private void OnDestroy()
    {
        GameEvents.instance.streakFreeKeys -= Special;
    }
    private void Update()
    {
        if (InputChecker.freeKeys == 0)
        {
            ResetSpecial();
        }
    }
    public void None()
    {
        quarterSprite.SetActive(false);
        halfSprite.SetActive(false);
        threeQuartersSprite.SetActive(false);
        wholeSprite.SetActive(false);
        powerUpSprite.SetActive(false);
    }

    public void Quarter()
    {
        quarterSprite.SetActive(true);
        halfSprite.SetActive(false);
        threeQuartersSprite.SetActive(false);
        wholeSprite.SetActive(false);
        powerUpSprite.SetActive(false);
    }
    public void Half()
    {
        quarterSprite.SetActive(false);
        halfSprite.SetActive(true);
        threeQuartersSprite.SetActive(false);
        wholeSprite.SetActive(false);
        powerUpSprite.SetActive(false);
    }
    public void ThreeQuarters()
    {
        quarterSprite.SetActive(false);
        halfSprite.SetActive(false);
        threeQuartersSprite.SetActive(true);
        wholeSprite.SetActive(false);
        powerUpSprite.SetActive(false);
    }
    public void Whole()
    {
        quarterSprite.SetActive(false);
        halfSprite.SetActive(false);
        threeQuartersSprite.SetActive(false);
        wholeSprite.SetActive(true);
        powerUpSprite.SetActive(false);
    }
    private void Special(int number)
    {
        powerUpSprite.SetActive(true);
    }
    private void ResetSpecial()
    {
        powerUpSprite.SetActive(false);
    } 

}
