using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartState : GameState
{
    public StartState()
    {
        scenes.Add("MainMenu");
    }

    public override void Enter()
    {
        LoadScenes();
    }

    public override void Exit()
    {
        UnloadScenes();
    }

    public override void UpdateState()
    {

    }
}
