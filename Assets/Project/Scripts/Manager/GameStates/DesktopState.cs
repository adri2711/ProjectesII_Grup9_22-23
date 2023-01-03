using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DesktopState : GameState
{
    public override void Enter()
    {
        if (!SceneManager.GetSceneByName("Desktop").isLoaded)
            SceneManager.LoadScene("Desktop", LoadSceneMode.Additive);
    }

    public override void Exit()
    {
        if (SceneManager.GetSceneByName("Desktop").isLoaded)
            SceneManager.UnloadSceneAsync("Desktop");
    }

    public override void UpdateState()
    {

    }
}
