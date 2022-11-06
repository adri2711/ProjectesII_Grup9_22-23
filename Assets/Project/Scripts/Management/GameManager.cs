using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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
        gameState = GameState.LEVEL;
        LevelStart();
    }

    void Update()
    {
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
