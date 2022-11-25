using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StreakMultipartText : MultipartText
{
    private Color textColor = new Color(0.9f, 0.8f, 0.3f);
    private Color numberColor = new Color(0.9f, 0.9f, 0.3f);
    public void Setup()
    {
        AddPart(new TextPart("numberBody", "", numberColor, 0, "jbm"), 0);
        AddPart(new TextPart("textBody", "Streak: ", textColor, 0, "jbm"), 0);

    }

    public void UpdateScore(int score)
    {
        SetPartText("numberBody", score.ToString());
    }
}
