using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class Level : MonoBehaviour
{
    public bool completed = false;
    public bool unlocked = false;

    [SerializeField] protected float levelTime = 8f;
    [SerializeField] protected float correctLetterReward = 0.3f;
    [SerializeField] protected float wrongLetterPenalty = 0.2f;
    [SerializeField] protected float closePopupReward = 1f;

    public virtual void LevelStart()
    {
        GameEvents.instance.lose += LevelExit;
        GameEvents.instance.finishLevel += LevelWin;

        TimerManager.instance.Setup(levelTime, correctLetterReward, wrongLetterPenalty, closePopupReward);
        IntManager.instance.Setup();
    }
    public virtual void LevelUpdate()
    {

    }
    public virtual void LevelWin()
    {
        GameEvents.instance.finishLevel -= LevelWin;
        LevelExit();
    }
    public virtual void LevelExit()
    {
        GameEvents.instance.lose -= LevelExit;
        LevelState.parserActive = false;
    }
    protected abstract void ActivateLevel();
    public float GetLevelTime()
    {
        return levelTime;
    }
    public bool isParserActive()
    {
        return LevelState.parserActive;
    }
}
