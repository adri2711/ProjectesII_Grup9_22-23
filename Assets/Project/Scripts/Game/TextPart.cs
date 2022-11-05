using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum Modifiers { 
    BOLD = 1,
    ITALIC = 2,
    STRIKETHROUGH = 4
}

[System.Serializable]
public class TextPart
{
    private string[] startMods = { "<b>", "<i>", "<s>" };
    private string[] endMods = { "</b>", "</i>", "</s>" };

    public string id = "invalid";
    public string text = "";
    public string preFormat;
    public string postFormat;
    public string colorHex;
    public Color color;
    public uint flags;

    public TextPart(string id, string text, Color color, Modifiers mods = 0)
    {
        this.id = id;
        this.text = text;
        this.color = color;
        this.flags = (uint)mods;

        GenerateFormat();
    }
    public TextPart()
    {
    }
    public void GenerateFormat()
    {
        preFormat = "";
        postFormat = "";

        //Colors
        if (colorHex != null)
        {
            UnityEngine.ColorUtility.TryParseHtmlString(colorHex, out color);
            colorHex = null;
        }
        preFormat += "<color=#" + color.ToHexString() + ">";

        //Modifiers
        uint mods = this.flags;
        int i = 0;
        while (mods > 0)
        {
            if (mods % 2 == 1)
            {
                if (i < 10)
                {
                    preFormat += startMods[i];
                    postFormat += endMods[i];
                }
            }
            mods >>= 1;
            i++;
        }
    }

    public string GetFormattedText()
    {
        if (preFormat == null)
        {
            GenerateFormat();
        }
        return preFormat + text + postFormat;
    }
    public string GetFormattedText(int s, int l)
    {
        if (preFormat == null)
        {
            GenerateFormat();
        }
        return preFormat + text.Substring(s ,l) + postFormat;
    }
}
