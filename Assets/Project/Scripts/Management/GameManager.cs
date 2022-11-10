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
    private int currLevel = 1;
    private int prevLevel = 1;
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

        LevelStart();
    }

    void Update()
    {
        //Level change
        if (currLevel != prevLevel)
        {
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
            string sceneName = "Level" + currLevel;
            if (!SceneManager.GetSceneByName(sceneName).isLoaded)
                SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
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

    private IEnumerator ResetLevelThread(float t)
    {
        yield return new WaitForSeconds(t);
        Application.Quit();
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
        yield return new WaitForSeconds(1f);
        string sceneName = "Level" + currLevel;
        if (SceneManager.GetSceneByName(sceneName).isLoaded)
            SceneManager.UnloadSceneAsync(sceneName);
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

    public int GetCurrentLevel()
    {
        return currLevel;
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
