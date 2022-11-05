using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MultipartText
{
    protected List<TextPart> parts = new List<TextPart>();
    string formattedText = "";
    string unformattedText = "";

    public void SetPartText(string pid, string text)
    {
        int p = GetIndexFromId(pid);
        if (p >= 0)
        {
            SetPartText(p, text);
        }
    }
    public void SetPartText(int p, string text)
    {
        parts[p].text = text;
        UpdateText();
    }
    public string GetPartText(string pid)
    {
        int p = GetIndexFromId(pid);
        if (p >= 0)
        {
            return GetPartText(p);
        }
        return null;
    }
    public string GetPartText(int p)
    {
        return parts[p].text;
    }

    public void AddPart(TextPart newPart)
    {
        parts.Add(newPart);
        UpdateText();
    }
    public virtual void AddPart(TextPart newPart, int index)
    {
        parts.Insert(index, newPart);
        UpdateText();
    }
    public void SetPartPos(string pid, int index)
    {
        int p = GetIndexFromId(pid);
        if (p >= 0)
        {
            SetPartPos(p, index);
        }
    }
    public virtual void SetPartPos(int p, int index)
    {
        TextPart temp = parts[p];
        parts.RemoveAt(p);
        parts.Insert(index, temp);
        UpdateText();
    }

    protected int GetIndexFromId(string pid)
    {
        int p = -1;
        for (int i = 0; i < parts.Count; i++)
        {
            if (parts[i].id == pid)
            {
                p = i;
                break;
            }
        }
        return p;
    }

    protected void UpdateText()
    {
        formattedText = "";
        foreach (TextPart part in parts)
        {
            formattedText += part.GetFormattedText();
        }

        unformattedText = "";
        foreach (TextPart part in parts)
        {
            unformattedText += part.text;
        }
    }

    //public string GetTextSegment(int s, int l)
    //{
    //    int i = 0, j = 0;
    //    bool end = false;
    //    while (i < parts.Count && !end)
    //    {
    //        while (j < parts[i].text.Length)
    //        {
    //            end = j >= s + l;
    //            j++;
    //        }
    //        i++;
    //    }
    //}
    public string GetFullFormattedText()
    {
        return formattedText;
    }
    public string GetFullUnformattedText()
    {
        return unformattedText;
    }
}
