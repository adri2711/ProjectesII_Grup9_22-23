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
    public event Action<float> gainExtraTime;
    public event Action finishLevel;
    public event Action lose;
    public event Action activateLevel;

    public event Action<int> detachKey;
    public event Action<int> attachKey;
    public event Action nudgeKey;

    public event Action<int> streakFreeKeys;
    public event Action streakIncrease;
    public event Action brokenStreak;

    public event Action popupSpawn;
    public event Action popupClose;
    public event Action popupBounce;
    public event Action popupGrab;

    public event Action lowTimeTick;
    public event Action lowTimeEffect;

    public void AttachKey(int index)
    {
        if (attachKey != null)
        {
            attachKey(index);
        }
    }
    public void DetachKey(int index)
    {
        if (detachKey != null)
        {
            detachKey(index);
        }
    }
    public void NudgeKey()
    {
        if (nudgeKey != null)
        {
            nudgeKey();
        }
    }
    public void StreakFreeKeys(int amount)
    {
        if (streakFreeKeys != null)
        {
            streakFreeKeys(amount);
        }
    }
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
    public void PopupClose()
    {
        if (popupClose != null)
        {
            popupClose();
        }
    }
    public void PopupSpawn()
    {
        if (popupSpawn != null)
        {
            popupSpawn();
        }
    }
    public void PopupBounce()
    {
        if (popupBounce != null)
        {
            popupBounce();
        }
    }
    public void PopupGrab()
    {
        if (popupGrab != null)
        {
            popupGrab();
        }
    }
    public void LowTimeEffect()
    {
        if (lowTimeEffect != null)
        {
            lowTimeEffect();
        }
    }
    public void LowTimeTick()
    {
        if (lowTimeTick != null)
        {
            lowTimeTick();
        }
    }

    public void StreakIncrease() 
    { 
        if(streakIncrease != null)
        {
            streakIncrease();
        }
    }

    public void BrokenStreak()
    {
        if (brokenStreak != null)
        {
            brokenStreak();
        }
    }
}
