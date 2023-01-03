using System.Collections;
using System.Collections.Generic;
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
    [SerializeField] private Level[] levels;

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
        UnloadLevel();
    }

    public override void UpdateState()
    {
        //Level change
        if (!loading && (currLevel != prevLevel || queueLoadLevel))
        {
            StartCoroutine(LoadLevelThread(0));
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
        StartCoroutine(ExitLevelThread(levelFadeoutTime));
    }

    private void LoadLevel()
    {
        levels[currLevel].LevelStart();
    }
    private IEnumerator LoadLevelThread(float t)
    {
        loading = true;
        yield return new WaitForSeconds(t);
        if (!SceneManager.GetSceneByName("Streak").isLoaded)
            SceneManager.LoadSceneAsync("Streak", LoadSceneMode.Additive);
        if (!SceneManager.GetSceneByName("Parser").isLoaded)
            SceneManager.LoadScene("Parser", LoadSceneMode.Additive);
        yield return new WaitForSeconds(0.4f);

        while (TimerManager.instance == null || IntManager.instance == null) { }
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
        if (SceneManager.GetSceneByName("Parser").isLoaded)
            SceneManager.UnloadSceneAsync("Parser");
        if (SceneManager.GetSceneByName("Streak").isLoaded)
            SceneManager.UnloadSceneAsync("Streak");

        while (SceneManager.GetSceneByName("Parser").isLoaded || SceneManager.GetSceneByName("Streak").isLoaded) { }
    }

    public int GetCurrentLevelNum()
    {
        return currLevel;
    }
    public int GetLevelCount()
    {
        return levels.Length;
    }
}
