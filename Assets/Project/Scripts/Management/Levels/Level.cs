using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class Level : MonoBehaviour
{
    [SerializeField] protected float levelTime = 8f;
    protected bool parserActive = false;
    public virtual void LevelStart()
    {
        GameEvents.instance.lose += LevelFinish;
        GameEvents.instance.finishLevel += LevelFinish;

        TimerManager.instance.Setup(levelTime);
        IntManager.instance.Setup();
    }
    public virtual void LevelUpdate()
    {

    }
    public virtual void LevelFinish()
    {
        parserActive = false;
    }
    protected abstract void ActivateLevel();
    public float GetLevelTime()
    {
        return levelTime;
    }
    public bool isParserActive()
    {
        return parserActive;
    }
}
