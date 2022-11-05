using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class ParserMultipartText : MultipartText
{
    private int correctIndex;
    private int wrongIndex;
    private int bodyIndex;
    private int currIndex;

    public ParserMultipartText()
    {
    }
    public void Setup(TextAsset jsonText)
    {
        parts = JsonUtility.FromJson<TextParts>(jsonText.text);
        parts.value = parts.partArray.ToList();

        AddPart(new TextPart("bodyDefault", "", new Color(0.1f, 0.1f, 0.1f)),0);
        AddPart(new TextPart("wrong", "", new Color(0.6f, 0.1f, 0.1f)),0);
        AddPart(new TextPart("correct", "", new Color(0.3f, 0.8f, 0.4f)),0);

        UpdateText();
        UpdateIndexes();
    }

    public void AddCorrectLetter()
    {
        char correctChar;
        if (parts.value[wrongIndex].text.Length > 0)
        {
            correctChar = parts.value[wrongIndex].text[0];
            parts.value[wrongIndex].text = parts.value[wrongIndex].text.Remove(0, 1);
        }
        else
        {
            correctChar = parts.value[currIndex].text[0];
            parts.value[currIndex].text = parts.value[currIndex].text.Remove(0, 1);
        }
        parts.value[correctIndex].text += correctChar;
        UpdateText();
        UpdateIndexes();
    }
    public void WrongLetter()
    {
        if (parts.value[wrongIndex].text.Length == 0)
        {
            parts.value[wrongIndex].text = parts.value[wrongIndex].text.Insert(0, parts.value[currIndex].text[0].ToString());
            parts.value[currIndex].text = parts.value[currIndex].text.Remove(0, 1);
            UpdateText();
            UpdateIndexes();
        }
    }
    public override void AddPart(TextPart newPart, int index)
    {
        base.AddPart(newPart, index);
        UpdateIndexes();
    }
    public override void SetPartPos(int p, int index)
    {
        base.SetPartPos(p, index);
        UpdateIndexes();
    }
    private void UpdateIndexes()
    {
        //find key part indexes
        correctIndex = GetIndexFromId("correct");
        wrongIndex = GetIndexFromId("wrong");
        bodyIndex = GetIndexFromId("bodyDefault");

        //find first non-empty part in text body
        int i = bodyIndex;
        currIndex = bodyIndex;
        while (i < parts.value.Count && parts.value[i].text.Length == 0)
        {
            i++;
            currIndex = i;
        }
    }
}
