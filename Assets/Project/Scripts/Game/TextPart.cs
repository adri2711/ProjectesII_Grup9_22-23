using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TextPart : MonoBehaviour
{
    public string id = "invalid";
    public string text = "";
    public string format = "";
    public Color color; 

    public TextPart(string id, string text, Color color)
    {
        this.id = id;
        this.text = text;
        this.color = color;

        format += "<color=#" + color.ToHexString() + ">";
    }

    public string GetFormattedText()
    {
        return format + text;
    }
}
