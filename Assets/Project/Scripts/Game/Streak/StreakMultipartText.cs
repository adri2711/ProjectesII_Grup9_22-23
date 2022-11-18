using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StreakMultipartText : MultipartText
{
    private Color textColor = new Color(0.6f, 0.7f, 0.4f);
    private Color numberColor = new Color(0.8f, 0.8f, 0.5f);
    public void Setup()
    {
        AddPart(new TextPart("numberBody", "", numberColor), 0);
        AddPart(new TextPart("textBody", "Streak: ", textColor), 0);

    }

    public void UpdateScore(int score)
    {
        SetPartText("numberBody", score.ToString());
    }
}
