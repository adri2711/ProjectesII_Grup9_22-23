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
    public override void LevelExit()
    {
        GameEvents.instance.popupClose -= ActivateLevel;
        IntManager.instance.Deactivate();
        IntManager.instance.DestroyAllInterruptions();
        base.LevelExit();
    }
    protected override void ActivateLevel()
    {
        LevelState.parserActive = true;
        IntManager.instance.Activate();
        GameEvents.instance.popupClose -= ActivateLevel;
    }
}
