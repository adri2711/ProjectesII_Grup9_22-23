using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance { get; private set; }

    public AudioSource audioSource;

    public List<AudioClip> enterLetterClips;
    public AudioClip enterCorrectLetterClip;
    public AudioClip enterWrongLetterClip;
    public AudioClip loseClip;
    public AudioClip finishLevelClip;
    public AudioClip gainExtraTimeClip;

    public AudioSource bgAudioSource;
    public AudioClip backgroundMusic;

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
        
        bgAudioSource.clip = backgroundMusic;
        bgAudioSource.loop = true;
        bgAudioSource.Play();
    }

    public void EnterLetterSound()
    {
        int position = Random.Range(0, enterLetterClips.Count);
        audioSource.PlayOneShot(enterLetterClips[position]);
    }
    public void EnterCorrectLetterSound(int i)
    {
        audioSource.PlayOneShot(enterCorrectLetterClip, 1f);
    }
    public void EnterWrongLetterSound(int i)
    {
        audioSource.PlayOneShot(enterWrongLetterClip, 0.3f);
    }
    public void LoseSound()
    {
        audioSource.PlayOneShot(loseClip, 0.2f);
    }
    public void FinishLevelSound()
    {
        audioSource.PlayOneShot(finishLevelClip, 0.5f);
    }
    public void GainExtraTimeSound(float f)
    {
        //audioSource.PlayOneShot(gainExtraTimeClip);
    }
}
