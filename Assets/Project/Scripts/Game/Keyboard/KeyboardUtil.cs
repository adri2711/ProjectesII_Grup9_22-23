using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardUtil : MonoBehaviour
{
    public static int KeyCodeToKeyboardPos(KeyCode code)
    {
        if (code >= KeyCode.A && code <= KeyCode.Z)
        {
            return (int)(code - KeyCode.A);
        }
        else
        {
            switch (code)
            {
                case KeyCode.Space:
                    return 26;
                case KeyCode.KeypadEnter:
                case KeyCode.Return:
                    return 27;
                case KeyCode.Plus:
                case KeyCode.KeypadPlus:
                    return 28;
                case KeyCode.LeftCurlyBracket:
                    return 29;
                case KeyCode.RightCurlyBracket:
                    return 30;
                case KeyCode.Semicolon:
                case KeyCode.Comma:
                    return 31;
                case KeyCode.Colon:
                case KeyCode.Period:
                case KeyCode.KeypadPeriod:
                    return 32;
                case KeyCode.LeftShift:
                case KeyCode.RightShift:
                    return 33;
                case KeyCode.LeftControl:
                case KeyCode.RightControl:
                    return 34;
                case KeyCode.LeftAlt:
                case KeyCode.RightAlt:
                    return 35;
                case KeyCode.AltGr:
                    return 36;
            }
        }
        return -1;
    }
    public static int CharToKeyboardPos(char c)
    {
        if (c >= 'A' && c <= 'Z')
        {
            return (int)(c - 'A');
        }
        else if (c >= 'a' && c <= 'z')
        {
            return (int)(c - 'a');
        }
        else
        {
            switch (c)
            {
                case ' ':
                    return 26;
                case '+':
                    return 28;
                case '{':
                    return 29;
                case '}':
                    return 30;
                case ';':
                case ',':
                    return 31;
                case ':':
                case '.':
                    return 32;
            }
        }
        return -1;
    }
    public static string KeyCodesToString(Queue<KeyCode> input)
    {
        string ret = "";
        foreach (KeyCode code in input)
        {
            ret += code.ToString();
        }
        return ret;
    }

    public static int ProcessInputCode(KeyCode code)
    {
        char codeChar = (char)code;
        string input = Input.inputString;
        string inputLower = input.ToLower();
        for (int i = 0; i < inputLower.Length; i++)
        {
            if (inputLower[i] == codeChar)
                return (int)input[i];
        }
        return -1;
    }
}
