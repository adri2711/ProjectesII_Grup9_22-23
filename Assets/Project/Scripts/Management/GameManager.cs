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
    private bool queueLoadLevel = false;
    private bool loading = false;
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

        //TEMP
        queueLoadLevel = true;
    }
    void Start()
    {
        GameEvents.instance.finishLevel += LevelFinish;
        GameEvents.instance.lose += LevelLose;
        gameState = GameState.LEVEL;

        if (!SceneManager.GetSceneByName("Sound").isLoaded)
            SceneManager.LoadSceneAsync("Sound", LoadSceneMode.Additive);
        if (!SceneManager.GetSceneByName("Fade").isLoaded)
            SceneManager.LoadSceneAsync("Fade", LoadSceneMode.Additive);
        if (!SceneManager.GetSceneByName("Interruptions").isLoaded)
            SceneManager.LoadSceneAsync("Interruptions", LoadSceneMode.Additive);
        if (!SceneManager.GetSceneByName("Timer").isLoaded)
            SceneManager.LoadSceneAsync("Timer", LoadSceneMode.Additive);
        if (!SceneManager.GetSceneByName("Pause").isLoaded)
            SceneManager.LoadSceneAsync("Pause", LoadSceneMode.Additive);
        if (!SceneManager.GetSceneByName("PostProcessing").isLoaded)
            SceneManager.LoadSceneAsync("PostProcessing", LoadSceneMode.Additive);
    }

    void Update()
    {
        //Level change
        if (!loading && (currLevel != prevLevel || queueLoadLevel))
        {
            StartCoroutine(LoadLevelThread(levelFadeoutTime));
            queueLoadLevel = false;
            prevLevel = currLevel;
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
    private void OnGUI()
    {
        Event e = Event.current;
        if (e.isKey && e.type == EventType.KeyDown)
        {
            if (e.keyCode == KeyCode.F1)
            {
                currLevel = (GetLevelCount() + currLevel - 1) % GetLevelCount();
            }
            else if (e.keyCode == KeyCode.F2) {
                currLevel = (GetLevelCount() + currLevel + 1) % GetLevelCount();
            }
            else if (e.keyCode == KeyCode.F3)
            {
                if (TimerManager.instance != null)
                {
                    TimerManager.instance.Deactivate();
                }
            }
        }
    }
    private void LevelStart()
    {
        if (currLevel < GetLevelCount())
        {
            if (!SceneManager.GetSceneByName("Streak").isLoaded)
                SceneManager.LoadSceneAsync("Streak", LoadSceneMode.Additive);
            if (!SceneManager.GetSceneByName("Parser").isLoaded)
                SceneManager.LoadScene("Parser", LoadSceneMode.Additive);
            levels[currLevel].LevelStart();
        }
        else
        {
            Application.Quit();
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
        queueLoadLevel = true;
        GlitchEffect.instance.Run(levelFadeoutTime, 1f);
    }
    private void LevelFinish()
    {
        currLevel++;
    }

    private IEnumerator LoadLevelThread(float t)
    {
        loading = true;
        yield return new WaitForSeconds(t);
        Fade.instance.FadeOutIn();
        yield return new WaitForSeconds(0.4f);

        while (TimerManager.instance == null || IntManager.instance == null) { }

        UnloadLevel();

        LevelStart();

        loading = false;
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
