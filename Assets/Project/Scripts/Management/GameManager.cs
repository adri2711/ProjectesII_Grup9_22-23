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

    private GameState gameState = GameState.MAINMENU;
    private int currLevel = 0;
    private int prevLevel = 0;
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
        gameState = GameState.LEVEL;
        LevelStart();
    }

    void Update()
    {
        if (currLevel != prevLevel)
        {
            LevelStart();
        }
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
    private void LevelFinish()
    {
        StartCoroutine(NextLevelThread(levelFadeoutTime));
    }
    private IEnumerator NextLevelThread(float t)
    {
        yield return new WaitForSeconds(t);
        string sceneName = "Level" + currLevel;
        if (SceneManager.GetSceneByName(sceneName).isLoaded)
            SceneManager.UnloadSceneAsync(sceneName);
        currLevel++;
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
