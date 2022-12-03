using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using UnityEngine;

public class KeyboardManager : MonoBehaviour
{
    private bool caps;
    private bool control;
    [SerializeField] List<Key> keys;
    ParserManager pm;

    private enum Keys
    {
        q,
        w,
        e,
        r,
        t,
        y,
        u,
        i,
        o,
        p,
        a,
        s,
        d,
        f,
        g,
        h,
        j,
        k,
        l,
        z,
        x,
        c,
        v,
        b,
        n,
        m

    }

    void Start()
    {
        pm = transform.parent.parent.gameObject.GetComponentInChildren<ParserManager>();

        caps = (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.LeftShift));

        GameEvents.instance.enterCorrectLetter += HighlightNextKey;
        GameEvents.instance.streakFreeKeys += FreeKeys;

        HighlightNextKey(-1);
    }

    private void OnDestroy()
    {
        GameEvents.instance.enterCorrectLetter -= HighlightNextKey;
        GameEvents.instance.streakFreeKeys -= FreeKeys;
    }

    int KeyCodeToKeyboardPos(KeyCode code)
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

    string KeyCodesToString(Queue<KeyCode> input)
    {
        string ret = "";
        foreach (KeyCode code in input)
        {
            ret += code.ToString();
        }
        return ret;
    }

    void HighlightNextKey(int pos)
    {
        if (InputChecker.freeKeys == 0)
        {
            ResetFreeKeys();
        }
        ResetKeyHighlight();
        if (pos < pm.GetTextSize() - 1)
        {
            string s = pm.GetChar(pos + 1).ToString().ToUpper();
            int index = -1;
            if (s[0] >= 'A' && s[0] <= 'Z')
            {
                index = s[0] - 'A';
            }
            else if (s[0] == ' ')
            {
                index = 26;
            }
            if (index >= 0)
            {
                keys[index].NextLetter();
            }
        }
    }
    private void ResetKeyHighlight()
    {
        foreach (Key key in keys)
        {
            key.UnhighlightKey();
        }
    }
    public void FreeKeys(int amount)
    {
        foreach (Key key in keys)
        {
            key.freeKey = true;
            key.UpdateKey();
        }
    }
    private void ResetFreeKeys()
    {
        foreach (Key key in keys)
        {
            key.freeKey = false;
            key.UpdateKey();
        }
    }

    void OnGUI()
    {
        if (GameManager.instance.GetCurrentLevel().isParserActive() && !PauseMenu.gameIsPaused)
        {
            Event e = Event.current;
            if (e.isKey && KeyCodeToKeyboardPos(e.keyCode) >= 0)
            {
                ProcessModifierKeys(e);
                if (e.type == EventType.KeyDown)
                {
                    //Key pressed
                    int keyInt = ProcessInputCode(e.keyCode);
                    if (keyInt >= 0)
                    {
                        if (pm.VerifyKey((char)keyInt))
                        {
                            keys[KeyCodeToKeyboardPos(e.keyCode)].PushCorrectLetter();
                        }
                        else
                        {
                            keys[KeyCodeToKeyboardPos(e.keyCode)].PushWrongLetter();
                        }
                    }
                }
                else if (e.type == EventType.KeyUp)
                {
                    //Key released
                    keys[KeyCodeToKeyboardPos(e.keyCode)].ResetLetter();
                }
            }
        }
    }

    public int ProcessInputCode(KeyCode code)
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

    void ProcessModifierKeys(Event e)
    {
        if (e.keyCode == KeyCode.LeftShift || e.keyCode == KeyCode.RightShift)
            caps = !caps;
        else if (e.keyCode == KeyCode.LeftControl || e.keyCode == KeyCode.RightControl)
            control = !control;
    }
}
