using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class StreakCounter : MonoBehaviour
{
    private int streak = 0;
    private StreakDisplay display;
    private void Start()
    {
        display = GetComponent<StreakDisplay>();
        GameEvents.instance.enterCorrectLetter += Add;
        GameEvents.instance.enterWrongLetter += End;
    }
    private void Add(int p)
    {
        streak++;
        display.UpdateScore(streak);
    }
    private void End(int p)
    {
        streak = 0;
    }
    public int GetCurrentStreak()
    {
        return streak;
    }
}