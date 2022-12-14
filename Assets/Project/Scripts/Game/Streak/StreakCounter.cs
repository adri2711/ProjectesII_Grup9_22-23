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
    private SpriteChanger spriteChanger;
    private void Start()
    {
        spriteChanger = GetComponent<SpriteChanger>();
        display = GetComponent<StreakDisplay>();
        GameEvents.instance.enterCorrectLetter += Add;
        GameEvents.instance.enterWrongLetter += End;
        display.UpdateScore(streak);
        spriteChanger.None();
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
            if(streak % 10 == 0)
            {
                GameEvents.instance.StreakIncrease();
            }
            if (streak % 50 == 0)
            {
                GameEvents.instance.StreakFreeKeys(3);
            }
        }

        switch (streak)
        {
            case 10:
                spriteChanger.Quarter();
                break;
            case 20:
                spriteChanger.Half();
                break;
            case 30:
                spriteChanger.ThreeQuarters();
                break;
            case 40:
                spriteChanger.Whole();
                break ;
            default:
                break;
        }
    }
    private void End(int p)
    {
        increaseAmount = 1;

        SpeedEffect.instance.End();
        if (streak >= 40)
        {
            GameEvents.instance.BrokenStreak();
        }
        streak = score = 0;
        display.UpdateScore(score);
        spriteChanger.None();
    }
}
