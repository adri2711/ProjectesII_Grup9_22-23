using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StreakCounter : MonoBehaviour
{
    private int streak = 0;
    private StreakDisplay display;
    private void Start()
    {
        display = GetComponent<StreakDisplay>();
        GameEvents.instance.enterCorrectLetter += Add;
        GameEvents.instance.enterWrongLetter += End;
        display.UpdateScore(streak);
    }
    private void Add(int p)
    {
        streak++;
        display.UpdateScore(streak);
        display.ScoreIncreaseDisplay(1);
    }
    private void End(int p)
    {
        streak = 0;
        display.UpdateScore(streak);
    }
    public int GetCurrentStreak()
    {
        return streak;
    }
}
