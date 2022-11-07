using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance { get; private set; }
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);

        GameEvents.instance.enterLetter += EnterLetterSound;
        GameEvents.instance.enterCorrectLetter += EnterCorrectLetterSound;
        GameEvents.instance.enterWrongLetter += EnterWrongLetterSound;
        GameEvents.instance.lose += LoseSound;
        GameEvents.instance.finishLevel += FinishLevelSound;
        GameEvents.instance.gainExtraTime += GainExtraTimeSound;
        
    }

    public void EnterLetterSound()
    {

    }
    public void EnterCorrectLetterSound(int i)
    {

    }
    public void EnterWrongLetterSound(int i)
    {

    }
    public void LoseSound()
    {

    }
    public void FinishLevelSound()
    {

    }
    public void GainExtraTimeSound(float f)
    {

    }
}
