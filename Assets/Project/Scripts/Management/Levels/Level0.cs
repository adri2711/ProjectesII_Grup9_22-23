using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level0 : Level
{
    public override void LevelStart()
    {
        if (!SceneManager.GetSceneByName("Level0").isLoaded)
        SceneManager.LoadScene("Level0", LoadSceneMode.Additive);

        base.LevelStart();
        GameEvents.instance.enterCorrectLetter += CorrectLetter;
    }
    public override void LevelUpdate()
    {
        base.LevelUpdate();
    }
    private void CorrectLetter(int p)
    {
        ActivateLevel();
    }
    protected override void ActivateLevel()
    {
        TimerManager.instance.Activate();
    }
}
