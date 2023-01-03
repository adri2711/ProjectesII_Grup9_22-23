using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class Level : MonoBehaviour
{
    [SerializeField] protected float levelTime = 8f;
    [SerializeField] protected float correctLetterReward = 0.3f;
    [SerializeField] protected float wrongLetterPenalty = 0.2f;
    [SerializeField] protected float closePopupReward = 1f;
    public bool completed { get; private set; } = false;

    public virtual void LevelStart()
    {
        GameEvents.instance.lose += LevelFinish;
        GameEvents.instance.finishLevel += LevelFinish;

        TimerManager.instance.Setup(levelTime, correctLetterReward, wrongLetterPenalty, closePopupReward);
        IntManager.instance.Setup();
    }
    public virtual void LevelUpdate()
    {

    }
    public virtual void LevelFinish()
    {
        completed = true;
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
