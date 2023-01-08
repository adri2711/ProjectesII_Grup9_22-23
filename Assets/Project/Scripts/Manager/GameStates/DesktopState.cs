using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DesktopState : GameState
{
    public DesktopState()
    {
        scenes.Add("Desktop");
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
