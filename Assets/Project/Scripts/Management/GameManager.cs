using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public enum GameState
    {
        MAINMENU,
        SELECT,
        LEVEL
    }

    bool loadMutex = false;
    private GameState gameState = GameState.MAINMENU;
    private int currLevel = 0;
    private int prevLevel = 0;
    private bool queueStartLevel = false;
    private float levelFadeoutTime = 3f;
    [SerializeField] private Level[] levels;

    public static GameManager instance { get; private set; }
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
        DontDestroyOnLoad(this.gameObject);
    }
    void Start()
    {
        GameEvents.instance.finishLevel += LevelFinish;
        GameEvents.instance.lose += LevelLose;
        gameState = GameState.LEVEL;

        if (!SceneManager.GetSceneByName("Sound").isLoaded)
            SceneManager.LoadScene("Sound", LoadSceneMode.Additive);
        if (!SceneManager.GetSceneByName("Fade").isLoaded)
            SceneManager.LoadScene("Fade", LoadSceneMode.Additive);

        //TEMP
        queueStartLevel = true;
    }

    void Update()
    {
        //Level change
        if (currLevel != prevLevel || queueStartLevel)
        {
            queueStartLevel = false;
            LevelStart();
        }
        prevLevel = currLevel;

        switch (gameState)
        {
            case GameState.MAINMENU:
                
                break;
            case GameState.SELECT:
                
                break;
            case GameState.LEVEL:
                LevelUpdate();
                break;
        }
    }

    private void LevelStart()
    {
        if (currLevel < GetLevelCount())
        {
            if (!SceneManager.GetSceneByName("Parser").isLoaded)
                SceneManager.LoadScene("Parser", LoadSceneMode.Additive);
            levels[currLevel].LevelStart();
        }
    }
    private void LevelUpdate()
    {
        if (currLevel < GetLevelCount())
        {
            levels[currLevel].LevelUpdate();
        }
    }

    private void LevelLose()
    {
        StartCoroutine(ResetLevelThread(2f));
    }
    private void LevelFinish()
    {
        if (!loadMutex)
        {
            StartCoroutine(NextLevelThread(levelFadeoutTime));
        }
    }

    private IEnumerator NextLevelThread(float t)
    {
        loadMutex = true;
        yield return new WaitForSeconds(t);
        Fade.instance.FadeOutIn();
        yield return new WaitForSeconds(0.4f);

        UnloadLevel();

        if (currLevel < levels.Length)
        {
            currLevel++;
        }
        else
        {
            Application.Quit();
        }
        loadMutex = false;
    }
    private IEnumerator ResetLevelThread(float t)
    {
        loadMutex = true;
        yield return new WaitForSeconds(t);
        Fade.instance.FadeOutIn();
        yield return new WaitForSeconds(0.4f);

        UnloadLevel();
        queueStartLevel = true;

        loadMutex = false;
    }

    private void UnloadLevel()
    {
        if (SceneManager.GetSceneByName("Parser").isLoaded)
            SceneManager.UnloadSceneAsync("Parser");

        while (SceneManager.GetSceneByName("Parser").isLoaded) { }
    }

    public int GetCurrentLevelNum()
    {
        return currLevel;
    }
    public Level GetCurrentLevel()
    {
        return levels[currLevel];
    }
    public int GetLevelCount()
    {
        return levels.Count();
    }
    public GameState GetGameState()
    {
        return gameState;
    }
}
