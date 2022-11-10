using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class Level : MonoBehaviour
{
    [SerializeField] protected float levelTime = 8f;
    public virtual void LevelStart()
    {
        GameEvents.instance.lose += LevelFinish;
        GameEvents.instance.finishLevel += LevelFinish;
    }
    public virtual void LevelUpdate()
    {

    }
    public virtual void LevelFinish()
    {
    }
    protected abstract void ActivateLevel();
    public float GetLevelTime()
    {
        return levelTime;
    }
}
