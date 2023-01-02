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
    }

    public override void UpdateState()
    {
        //Level change
        if (!loading && (currLevel != prevLevel || queueLoadLevel))
        {
            StartCoroutine(LoadLevelThread(levelFadeoutTime));
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
        queueLoadLevel = true;
        GlitchEffect.instance.Run(levelFadeoutTime, 1f);
    }
    private void LevelFinish()
    {
        currLevel++;
    }

    private void LoadLevel()
    {
        if (currLevel < GetLevelCount())
        {
            if (!SceneManager.GetSceneByName("Streak").isLoaded)
                SceneManager.LoadSceneAsync("Streak", LoadSceneMode.Additive);
            if (!SceneManager.GetSceneByName("Overlay").isLoaded)
                SceneManager.LoadSceneAsync("Overlay", LoadSceneMode.Additive);
            if (!SceneManager.GetSceneByName("Parser").isLoaded)
                SceneManager.LoadScene("Parser", LoadSceneMode.Additive);
            levels[currLevel].LevelStart();
        }
        else
        {
            Application.Quit();
        }
    }
    private IEnumerator LoadLevelThread(float t)
    {
        loading = true;
        yield return new WaitForSeconds(t);
        Fade.instance.FadeOutIn();
        yield return new WaitForSeconds(0.4f);

        while (TimerManager.instance == null || IntManager.instance == null) { }

        UnloadLevel();

        LoadLevel();

        loading = false;
    }

    private void UnloadLevel()
    {
        if (SceneManager.GetSceneByName("Parser").isLoaded)
            SceneManager.UnloadSceneAsync("Parser");
        if (SceneManager.GetSceneByName("Overlay").isLoaded)
            SceneManager.UnloadSceneAsync("Overlay");
        if (SceneManager.GetSceneByName("Streak").isLoaded)
            SceneManager.UnloadSceneAsync("Streak");

        while (SceneManager.GetSceneByName("Parser").isLoaded || SceneManager.GetSceneByName("Streak").isLoaded || SceneManager.GetSceneByName("Overlay").isLoaded) { }
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
