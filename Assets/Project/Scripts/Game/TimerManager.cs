using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TimerManager : MonoBehaviour
{
    public static TimerManager instance { get; private set; }

    [SerializeField] Image timerDisplay;
    private float correctLetterReward = 0.3f;
    private float wrongLetterPenalty = 0.3f;
    private bool active = false;
    private float maxTime = 8f;
    private float currTime;

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
    }
    void Start()
    {
        GameEvents.instance.activateLevel += Activate;
        GameEvents.instance.enterCorrectLetter += CorrectLetter;
        GameEvents.instance.enterWrongLetter += WrongLetter;

        Setup(8f);
    }
    public void Setup(float levelTime)
    {
        currTime = maxTime = levelTime;
        UpdateDisplay();
    }
    void Update()
    { 
        if (active)
        {
            SubtractTime(Time.deltaTime);
        }
    }
    
    private void UpdateDisplay()
    {
        timerDisplay.fillAmount = currTime / maxTime;
        float greenOffset = 255f - baseColor.g;
        float blueOffset = 255f - baseColor.b;
        Color32 displayColor = baseColor;
        displayColor.g = (byte)(baseColor.g + (currTime / maxTime) * greenOffset);
        displayColor.b = (byte)(baseColor.b + (currTime / maxTime) * (blueOffset/4f));
        timerDisplay.color = displayColor;
    }

    public void Activate()
    {
        active = true;
    }
    public void Deactivate()
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
    public void AddTime(float amount)
    {
        currTime += amount;
        if (currTime > maxTime)
        {
            GameEvents.instance.GainExtraTime(currTime - maxTime);
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
            GameEvents.instance.Lose();
        }
        UpdateDisplay();
    }
    public void SetMaxTime(float newMaxTime)
    {
        maxTime = newMaxTime;
    }
}
