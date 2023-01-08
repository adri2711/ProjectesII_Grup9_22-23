using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level1 : Level
{
    public override void LevelStart()
    {
        base.LevelStart();
        GameEvents.instance.enterCorrectLetter += CorrectLetter;
        TimerManager.instance.Deactivate();
        LevelState.parserActive = true;
    }
    public override void LevelUpdate()
    {
        base.LevelUpdate();
    }
    public override void LevelExit()
    {
        GameEvents.instance.enterCorrectLetter -= CorrectLetter;
        base.LevelExit();
    }
    private void CorrectLetter(int p)
    {
        GameEvents.instance.enterCorrectLetter -= CorrectLetter;
        ActivateLevel();
    }
    protected override void ActivateLevel()
    {
    }
}
