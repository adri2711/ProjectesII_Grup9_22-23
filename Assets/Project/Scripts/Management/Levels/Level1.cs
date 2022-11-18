using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level1 : Level
{
    public override void LevelStart()
    {
        base.LevelStart();
        parserActive = true;
        GameEvents.instance.enterCorrectLetter += CorrectLetter;
        TimerManager.instance.MakeVisible();
    }
    public override void LevelUpdate()
    {
        base.LevelUpdate();
    }
    public override void LevelFinish()
    {
        TimerManager.instance.Pause();
        base.LevelFinish();
    }
    private void CorrectLetter(int p)
    {
        ActivateLevel();
    }
    protected override void ActivateLevel()
    {
        TimerManager.instance.Activate();
        GameEvents.instance.enterCorrectLetter -= CorrectLetter;
    }
}
