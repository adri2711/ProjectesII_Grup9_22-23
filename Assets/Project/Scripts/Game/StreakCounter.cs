using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StreakCounter : MonoBehaviour
{
    private int streak = 0;
    private void Start()
    {
        GameEvents.instance.enterCorrectLetter += Add;
        GameEvents.instance.enterWrongLetter += End;
    }
    private void Add(int p)
    {
        streak++;
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
