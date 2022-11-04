using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum Modifiers { 
    BOLD = 1,
    ITALIC = 2,
    STRIKETHROUGH = 4
}

public class TextPart
{
    private string[] startMods = { "<b>", "<i>", "<s>" };
    private string[] endMods = { "</b>", "</i>", "</s>" };


    public string id = "invalid";
    public string text = "";
    public string preFormat = "";
    public string postFormat = "";
    public Color color;
    private uint modifiers;

    public TextPart(string id, string text, Color color, Modifiers mods = 0)
    {
        this.id = id;
        this.text = text;
        this.color = color;
        this.modifiers = (uint)mods;

        GenerateFormat();
    }

    private void GenerateFormat()
    {
        //Colors
        preFormat += "<color=#" + color.ToHexString() + ">";

        //Modifiers
        uint mods = this.modifiers;
        int i = 0;
        while (mods > 0)
        {
            if ((mods >> i) % 2 == 1)
            {
                if (i < 10)
                {
                    //preFormat += startMods[i];
                    //postFormat += endMods[i];
                }
            }
            i++;
        }
    }

    public string GetFormattedText()
    {
        return preFormat + text + postFormat;
    }
}
