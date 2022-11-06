using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level0 : Level
{
    public override void LevelStart()
    {
        base.LevelStart();
        GameEvents.instance.enterCorrectLetter += CorrectLetter;
        TimerManager.instance.Setup(levelTime);
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
