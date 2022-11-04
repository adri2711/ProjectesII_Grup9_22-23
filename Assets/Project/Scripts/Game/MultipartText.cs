using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MultipartText : MonoBehaviour
{
    protected List<TextPart> parts = new List<TextPart>();

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
    }
    public virtual void AddPart(TextPart newPart, int index)
    {
        parts.Insert(index, newPart);
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

    public virtual string GetFullFormattedText()
    {
        string ret = "";
        foreach (TextPart part in parts)
        {
            ret += part.GetFormattedText();
        }
        return ret;
    }

    public virtual string GetFullUnformattedText()
    {
        string ret = "";
        foreach (TextPart part in parts)
        {
            ret += part.text;
        }
        return ret;
    }
}
