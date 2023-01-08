using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TimerManager : MonoBehaviour
{
    public static TimerManager instance { get; private set; }

    private Image timerDisplay;
    private float correctLetterReward;
    private float wrongLetterPenalty;
    private float closePopupReward;
    private bool active = false;
    private float maxTime;
    private float currTime;
    private int lives;
    private bool regenerating;
    private float regenerationSteps = 800;

    private Color32 baseColor = new Color32(170, 90, 70, 255);

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
        DontDestroyOnLoad(this.gameObject);
    }
    void Start()
    {
        timerDisplay = GetComponentInChildren<Image>();

        GameEvents.instance.activateLevel += Activate;
        GameEvents.instance.enterCorrectLetter += CorrectLetter;
        GameEvents.instance.enterWrongLetter += WrongLetter;
        GameEvents.instance.popupClose += ClosePopup;
    }
    public void Setup(float levelTime = 8f, float correctLetterReward = 0.3f, float wrongLetterPenalty = 0.2f, float closePopupReward = 0.5f, int lives = 1)
    {
        currTime = maxTime = levelTime;
        this.correctLetterReward = correctLetterReward;
        this.wrongLetterPenalty = wrongLetterPenalty;
        this.closePopupReward = closePopupReward;
        this.lives = lives;
        regenerating = false;
        UpdateDisplay();
    }
    void Update()
    { 
        if (active)
        {
            if (regenerating)
            {
                AddTime(maxTime / regenerationSteps);
            }
            else
            {
                SubtractTime(Time.deltaTime);
            }
        }
    }
    
    private void UpdateDisplay()
    {
        timerDisplay.fillAmount = currTime / maxTime;
        /*float greenOffset = 255f - baseColor.g;
        float blueOffset = 255f - baseColor.b;
        Color32 displayColor = baseColor;
        displayColor.g = (byte)(baseColor.g + (currTime / maxTime) * greenOffset);
        displayColor.b = (byte)(baseColor.b + (currTime / maxTime) * (blueOffset/4f));
        timerDisplay.color = displayColor;*/
    }

    public void Activate()
    {
        active = true;
        timerDisplay.enabled = true;
    }
    public void MakeVisible()
    {
        timerDisplay.enabled = true;
    }
    public void Deactivate()
    {
        active = false;
        timerDisplay.enabled = false;
    }
    public void Pause()
    {
        active = false;
    }
    private void CorrectLetter(int c)
    {
        if (active)
        {
            AddTime(correctLetterReward);
        }
    }
    private void WrongLetter(int c)
    {
        if (active)
        {
            SubtractTime(wrongLetterPenalty);
        }
    }
    private void ClosePopup()
    {
        if (active)
        {
            AddTime(closePopupReward);
        }
    }
    public void AddTime(float amount)
    {
        currTime += amount;
        if (currTime > maxTime)
        {
            if (!regenerating) {
                GameEvents.instance.GainExtraTime(currTime - maxTime);
            }
            regenerating = false;

            currTime = maxTime;
        }
        UpdateDisplay();
    }
    public void SubtractTime(float amount)
    {
        currTime -= amount;
        if (currTime <= 0)
        {
            currTime = 0;
            if (lives > 0)
            {
                regenerating = true;
                lives--;
            }
            else
            {
                GameEvents.instance.Lose();
            }
        }
        UpdateDisplay();
    }
    public void SetMaxTime(float newMaxTime)
    {
        maxTime = newMaxTime;
    }
    public float GetCurrTime()
    {
        return currTime;
    }
    public float GetMaxTime()
    {
        return maxTime;
    }
    public bool IsActive()
    {
        return active;
    }
}
