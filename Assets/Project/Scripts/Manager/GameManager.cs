using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameState[] gameStates;
    private Dictionary<string, GameState> states = new Dictionary<string, GameState>();
    public string currState { get; private set; }

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
        foreach (GameState state in gameStates)
        {
            states.Add(state.name, state);
        }

        SetGameState("Level");

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
        states[currState].UpdateState();
    }
    private void OnGUI()
    {
        Event e = Event.current;
        if (e.isKey && e.type == EventType.KeyDown)
        {
            LevelState level = states["Level"] as LevelState;
            if (e.keyCode == KeyCode.F1)
            {
                LevelState.currLevel = (level.GetLevelCount() + LevelState.currLevel - 1) % level.GetLevelCount();
            }
            else if (e.keyCode == KeyCode.F2) {
                LevelState.currLevel = (level.GetLevelCount() + LevelState.currLevel + 1) % level.GetLevelCount();
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
    public string GetGameState()
    {
        return currState;
    }
    public void SetGameState(string newState)
    {
        if (currState != null)
        {
            states[currState].Exit();
        }
        currState = newState;
        states[currState].Enter();
    }
}
