using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class ParserMultipartText : MultipartText
{
    private int correctIndex;
    private int bodyIndex;
    private int wrongIndex;

    public ParserMultipartText()
    {
    }
    public void Setup(TextAsset jsonText)
    {
        /*string t = "I love mutant fish!! It gets me harder than a deep sea rock.";
        AddPart(new TextPart("correct", "", new Color(0.3f, 0.8f, 0.4f)));
        AddPart(new TextPart("wrong", "", new Color(0.6f, 0.1f, 0.1f)));
        AddPart(new TextPart("bodyDefault", t, new Color(0.1f, 0.1f, 0.1f)));*/

        //TextPart tp = JsonUtility.FromJson<TextPart>("{\"id\": \"bodyDefault\",\"colorHex\": \"#000000\",\"flags\": 1,\"text\": \"I love mutant fish! \"}");
        //AddPart(tp);

        UpdateIndexes();
    }

    public void AddCorrectLetter()
    {
        char correctChar;
        if (parts[wrongIndex].text.Length > 0)
        {
            correctChar = parts[wrongIndex].text[0];
            parts[wrongIndex].text = parts[wrongIndex].text.Remove(0, 1);
        }
        else
        {
            
            correctChar = parts[bodyIndex].text[0];
            parts[bodyIndex].text = parts[bodyIndex].text.Remove(0, 1);
        }
        parts[correctIndex].text += correctChar;
        UpdateText();
    }
    public void WrongLetter()
    {
        if (parts[wrongIndex].text.Length == 0)
        {
            parts[wrongIndex].text = parts[wrongIndex].text.Insert(0, parts[bodyIndex].text[0].ToString());
            parts[bodyIndex].text = parts[bodyIndex].text.Remove(0, 1);
            UpdateText();
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
        correctIndex = GetIndexFromId("correct");
        wrongIndex = GetIndexFromId("wrong");
        bodyIndex = GetIndexFromId("bodyDefault");
    }
}
