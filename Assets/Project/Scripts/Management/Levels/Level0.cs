using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level0 : Level
{
    public override void LevelStart()
    {
        base.LevelStart();
        GameEvents.instance.enterCorrectLetter += CorrectLetter;
        TimerManager.instance.Deactivate();
        parserActive = true;
    }
    public override void LevelUpdate()
    {
        base.LevelUpdate();
    }
    public override void LevelFinish()
    {
        base.LevelFinish();
    }
    private void CorrectLetter(int p)
    {
        ActivateLevel();
    }
    protected override void ActivateLevel()
    {
        GameEvents.instance.enterCorrectLetter -= CorrectLetter;
    }
}
