using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StreakCounter : MonoBehaviour
{
    private int streak = 0;
    private int score = 0;
    private int increaseAmount = 1;
    [SerializeField] private AnimationCurve incraseCurve;
    private StreakDisplay display;
    private void Start()
    {
        display = GetComponent<StreakDisplay>();
        GameEvents.instance.enterCorrectLetter += Add;
        GameEvents.instance.enterWrongLetter += End;
        display.UpdateScore(streak);
    }
    private void OnDestroy()
    {
        GameEvents.instance.enterCorrectLetter -= Add;
        GameEvents.instance.enterWrongLetter -= End;
    }
    private void Add(int p)
    {
        streak++;
        score += increaseAmount;
        display.UpdateScore(score);
        display.ScoreIncreaseDisplay(increaseAmount);

        if (streak > 20)
        {
            float percentage = Mathf.Min(((float)streak - 20f) / 40f, 1f);
            SpeedEffect.instance.Run(percentage);
        }

        if (streak > 0)
        {
            if (streak % 4 == 0)
            {
                increaseAmount = (int)incraseCurve.Evaluate(score);
            }
            if (streak % 50 == 0)
            {
                GameEvents.instance.StreakFreeKeys(3);
            }
        }
    }
    private void End(int p)
    {
        streak = 0;
        score = 0;
        increaseAmount = 1;
        display.UpdateScore(score);

        SpeedEffect.instance.End();
    }
}
