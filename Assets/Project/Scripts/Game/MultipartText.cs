using Mono.Cecil;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using UnityEditor.ProjectWindowCallback;
using UnityEngine;

public class MultipartText
{
    protected List<TextPart> parts;
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
        foreach (TextPart part in parts)
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
        //Loop thru parts
        while (i < parts.Count && !end)
        {
            //Loop thru part's text
            int k = 0;
            while (k < parts[i].text.Length && !end)
            {
                //exit if end of word is found or if loop goes over leniency
                end = j >= s + l - wholeWordLeniency && (parts[i].text[k] == ' ' || j == l + s + wholeWordLeniency);

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
                    newPart = parts[i].GetFormattedText(s, k - s);
                else
                    newPart = parts[i].text.Substring(s, k - s);
            }
            else
            {
                if (format)
                    newPart = parts[i].GetFormattedText(0, k);
                else
                    newPart = parts[i].text.Substring(0, k);
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
