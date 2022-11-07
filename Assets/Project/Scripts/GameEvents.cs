using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static GameEvents instance { get; private set; }

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

    public event Action<int> enterCorrectLetter;
    public event Action<int> enterWrongLetter;
    public event Action enterLetter;
    public event Action finishLevel;
    public event Action lose;
    public event Action activateLevel;
    public event Action<float> gainExtraTime;

    public void EnterCorrectLetter(int pos)
    {
        if (enterCorrectLetter != null)
        {
            enterCorrectLetter(pos);
        }
    } 
    public void EnterWrongLetter(int pos)
    {
        if (enterWrongLetter != null)
        {
            enterWrongLetter(pos);
        }
    } 
    public void EnterLetter()
    {
        if (enterLetter != null)
        {
            enterLetter();
        }
    } 
    public void FinishLevel()
    {
        if (finishLevel != null)
        {
            finishLevel();
        }
    } 
    public void Lose()
    {
        if (lose != null)
        {
            lose();
        }
    }
    public void ActivateLevel()
    {
        if (activateLevel != null)
        {
            activateLevel();
        }
    }
    public void GainExtraTime(float amount)
    {
        if (gainExtraTime != null)
        {
            gainExtraTime(amount);
        }
    }
}
