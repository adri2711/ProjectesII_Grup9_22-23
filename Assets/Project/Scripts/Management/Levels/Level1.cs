using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level1 : Level
{
    public override void LevelStart()
    {
        base.LevelStart();
        GameEvents.instance.popupClose += ActivateLevel;
    }
    public override void LevelUpdate()
    {
        base.LevelUpdate();
    }
    public override void LevelFinish()
    {
        TimerManager.instance.Deactivate();
        IntManager.instance.Deactivate();
        base.LevelFinish();
    }
    protected override void ActivateLevel()
    {
        TimerManager.instance.Activate();
        IntManager.instance.Activate();
    }
}
