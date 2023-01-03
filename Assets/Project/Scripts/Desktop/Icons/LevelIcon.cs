using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class LevelIcon : DesktopIcon
{
    public int level;
    protected override void ClickSelected()
    {
        base.ClickSelected();
        LevelState.currLevel = level;
        GameManager.instance.SetGameState("Level");
    }
}
