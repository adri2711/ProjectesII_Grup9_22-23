using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level2 : Level
{
    public override void LevelStart()
    {
        base.LevelStart();
        GameEvents.instance.popupClose += ActivateLevel;
        TimerManager.instance.Deactivate();
    }
    public override void LevelUpdate()
    {
        base.LevelUpdate();
    }
    public override void LevelFinish()
    {
        IntManager.instance.Deactivate();
        IntManager.instance.DestroyAllInterruptions();
        base.LevelFinish();
    }
    protected override void ActivateLevel()
    {
        parserActive = true;
        IntManager.instance.Activate();
        GameEvents.instance.popupClose -= ActivateLevel;
    }
}
