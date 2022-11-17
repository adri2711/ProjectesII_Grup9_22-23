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

    private HashSet<string> debuggedLabels = new HashSet<string>();

    public ParserMultipartText()
    {
    }
    public void Setup(string jsonText)
    {
        Debug.Log(jsonText);
        parts = JsonUtility.FromJson<TextParts>(jsonText);
        parts.value = parts.partArray.ToList();
        DebugParts("from json:");

        AddPart(new TextPart("bodyDefault", "", new Color(0.1f, 0.1f, 0.1f)),0);
        AddPart(new TextPart("wrong", "", new Color(1f, 1f, 1f), Modifiers.VISIBLE_SPACES, "bluescreen_flesh"),0);
        //AddPart(new TextPart("correct", "", new Color(0.3f, 0.8f, 0.4f)),0);
        AddPart(new TextPart("correct", "", new Color(1f, 1f, 1f), 0, "bluescreen_psych"),0);

        DebugParts("add parts:");

        UpdateText();
        UpdateIndexes();
    }

    public void DebugParts(string label)
    {
        if (!debuggedLabels.Contains(label))
        {
            Debug.Log(label);
            foreach (TextPart part in parts.value)
            {
                Debug.Log(part.text);
            }
            debuggedLabels.Add(label);
        }
    }

    public void AddCorrectLetter()
    {
        DebugParts("correct letter");
        Debug.Log("L");
        foreach (TextPart part in parts.value)
        {
            Debug.Log(part.text);
        }
        if (parts.value[wrongIndex].text.Length > 0)
        {
            MoveText(correctIndex, wrongIndex, parts.value[correctIndex].text.Length, 0, 1);
        }
        else
        {
            MoveText(correctIndex, currIndex, parts.value[correctIndex].text.Length, 0, 1);
        }
        UpdateIndexes();
    }
    public void WrongLetter()
    {
        if (parts.value[wrongIndex].text.Length == 0)
        {
            MoveText(wrongIndex, currIndex, 0, 0, 1);
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
