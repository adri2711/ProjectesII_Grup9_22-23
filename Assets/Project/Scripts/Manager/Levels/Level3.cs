using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level3 : Level
{
    public override void LevelStart()
    {
        base.LevelStart();
        GameEvents.instance.popupClose += ActivateLevel;
        TimerManager.instance.MakeVisible();
    }
    public override void LevelUpdate()
    {
        base.LevelUpdate();
    }
    public override void LevelFinish()
    {
        TimerManager.instance.Pause();
        IntManager.instance.Deactivate();
        IntManager.instance.DestroyAllInterruptions();
        base.LevelFinish();
    }
    protected override void ActivateLevel()
    {
        LevelState.parserActive = true;
        TimerManager.instance.Activate();
        IntManager.instance.Activate();
        GameEvents.instance.popupClose -= ActivateLevel;
    }
}
