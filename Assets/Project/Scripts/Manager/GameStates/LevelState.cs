using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelState : GameState
{
    public static int currLevel = 0;
    public static bool parserActive = false;
    private int prevLevel = 0;
    private bool queueLoadLevel = false;
    private bool loading = false;
    private float levelFadeoutTime = 3f;
    public Level[] levels;

    public LevelState()
    {
        scenes.Add("Parser");
        scenes.Add("Streak");
    }

    public override void Enter()
    {
        GameEvents.instance.finishLevel += LevelFinish;
        GameEvents.instance.lose += LevelLose;
        queueLoadLevel = true;
    }

    public override void Exit()
    {
        GameEvents.instance.finishLevel -= LevelFinish;
        GameEvents.instance.lose -= LevelLose;
        levels[currLevel].LevelExit();
        TimerManager.instance.Deactivate();
        UnloadLevel();
    }

    public override void UpdateState()
    {
        //Level change
        if (!loading && (currLevel != prevLevel || queueLoadLevel))
        {
            LoadLevel();
            queueLoadLevel = false;
            prevLevel = currLevel;
        }

        if (currLevel < GetLevelCount())
        {
            levels[currLevel].LevelUpdate();
        }
    }

    private void LevelLose()
    {
        StartCoroutine(ExitLevelThread(levelFadeoutTime));
        GlitchEffect.instance.Run(levelFadeoutTime, 1f);
    }
    private void LevelFinish()
    {
        levels[currLevel].completed = true;
        if (currLevel < levels.Length - 1)
        {
            levels[currLevel + 1].unlocked = true;
        }
        StartCoroutine(ExitLevelThread(levelFadeoutTime));
    }

    private void LoadLevel()
    {
        LoadScenes();
        levels[currLevel].LevelStart();
    }
    private IEnumerator LoadLevelThread(float t)
    {
        loading = true;
        yield return new WaitForSeconds(t);
        
        yield return new WaitForSeconds(0.4f);
        LoadLevel();

        loading = false;
    }
    private IEnumerator ExitLevelThread(float t)
    {
        yield return new WaitForSeconds(t);
        GameManager.instance.SetGameState("Desktop");
    }

    private void UnloadLevel()
    {
        UnloadScenes();
    }

    public int GetCurrentLevelNum()
    {
        return currLevel;
    }
    public int GetLevelCount()
    {
        return levels.Length;
    }
    public bool IsCompleted(int index)
    {
        return levels[index].completed;
    }
    public bool IsUnlocked(int index)
    {
        return levels[index].unlocked;
    }
}
