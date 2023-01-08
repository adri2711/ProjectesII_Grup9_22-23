using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesktopManager : MonoBehaviour
{
    private LevelIcon[] icons;
    private LevelState levelState;
    void Start()
    {
        levelState = GameObject.Find("GameStates").GetComponentInChildren<LevelState>();
        UpdateLevelIcons();
    }
    public void UpdateLevelIcons()
    {
        icons = GetComponentsInChildren<LevelIcon>();
        for (int i = 0; i < levelState.levels.Length; i++)
        {
            if (levelState.levels[i].completed)
            {
                icons[i].SetCompleted();
            }
            else if (levelState.levels[i].unlocked)
            {
                icons[i].SetUnlocked();
            }
            else
            {
                icons[i].SetLocked();
            }
        }
    }
}
