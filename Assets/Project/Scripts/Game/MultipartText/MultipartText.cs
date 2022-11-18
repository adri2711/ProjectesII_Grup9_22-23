using Mono.Cecil;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class TextParts
{
    public TextPart[] partArray;
    public List<TextPart> value = new List<TextPart>();
}

public class MultipartText
{
    public TextParts parts = new TextParts();
    private string formattedText = "";
    private string unformattedText = "";
    private string formattedRenderedText = "";
    private string unformattedRenderedText = "";
    private int wholeWordLeniency = 3;
    private int renderedSegmentStart = 0;
    private int renderedSegmentLength = 0;

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
        parts.value[p].text = text;
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
        return parts.value[p].text;
    }

    public void AddPart(TextPart newPart)
    {
        parts.value.Add(newPart);
        UpdateText();
    }
    public virtual void AddPart(TextPart newPart, int index)
    {
        parts.value.Insert(index, newPart);
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
        TextPart temp = parts.value[p];
        parts.value.RemoveAt(p);
        parts.value.Insert(index, temp);
        UpdateText();
    }
    public virtual void MoveText(int destIndex, int sourceIndex, int destStart = 0, int sourceStart = 0, int length = -1)
    {
        if (length < 0)
            length = parts.value[sourceIndex].text.Length;
        string dest = "";
        if (destStart > 0)
            dest += parts.value[destIndex].text.Substring(0, destStart);
        dest += parts.value[sourceIndex].text.Substring(sourceStart, length);
        dest += parts.value[destIndex].text.Substring(destStart, parts.value[destIndex].text.Length - destStart);
        parts.value[destIndex].text = dest;
        parts.value[sourceIndex].text = parts.value[sourceIndex].text.Remove(sourceStart, length);
        UpdateText();
    }

    protected int GetIndexFromId(string pid)
    {
        int p = -1;
        for (int i = 0; i < parts.value.Count; i++)
        {
            if (parts.value[i].id == pid)
            {
                p = i;
                break;
            }
        }
        return p;
    }
    public void SetRenderedSegment(int start, int length)
    {
        renderedSegmentStart = start;
        renderedSegmentLength = length;
        UpdateRenderedText();
    }
    protected virtual void UpdateRenderedText()
    {
        formattedRenderedText = UpdateRenderedText(renderedSegmentStart, renderedSegmentLength);
        unformattedRenderedText = UpdateRenderedText(renderedSegmentStart, renderedSegmentLength, false);
    }
    protected virtual void UpdateText()
    {
        formattedText = UpdateFullText();
        unformattedText = UpdateFullText(false);
        UpdateRenderedText();
    }
    private string UpdateFullText(bool format = true)
    {
        string ret = "";
        foreach (TextPart part in parts.value)
        {
            if (format)
                ret += part.GetFormattedText();
            else
                ret += part.text;
        }
        return ret;
    }
    private string UpdateRenderedText(int s, int l, bool format = true)
    {
        string ret = "";
        int i = 0, j = 0;
        bool end = false, start = false;
        //Loop thru parts.value
        while (i < parts.value.Count && !end)
        {
            //Loop thru part's text
            int k = 0;
            while (k < parts.value[i].text.Length && !end)
            {
                //exit if end of word is found or if loop goes over leniency
                end = j >= s + l - wholeWordLeniency && (parts.value[i].text[k] == ' ' || j == l + s + wholeWordLeniency);

                if (!end)
                {
                    j++;
                    k++;
                }
            }

            //Add part to returned text
            string newPart = "";
            if (j >= s && !start)
            {
                start = true;
                if (format)
                    newPart = parts.value[i].GetFormattedText(s, k - s);
                else
                    newPart = parts.value[i].text.Substring(s, k - s);
            }
            else
            {
                if (format)
                    newPart = parts.value[i].GetFormattedText(0, k);
                else
                    newPart = parts.value[i].text.Substring(0, k);
            }
            ret += newPart;

            i++;
        }
        return ret;
    }

    public string GetFullFormattedText()
    {
        return formattedText;
    }
    public string GetFullUnformattedText()
    {
        return unformattedText;
    }
    public string GetRenderedFormattedText()
    {
        return formattedRenderedText;
    }
    public int GetRenderedSegmentLength()
    {
        return unformattedRenderedText.Length;
    }
}
