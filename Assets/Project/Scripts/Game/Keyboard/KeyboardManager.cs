using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

public class KeyboardManager : MonoBehaviour
{
    private bool caps;
    private bool control;
    [SerializeField] List<Key> keys;
    [SerializeField] ParserManager pm;

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
        caps = (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.LeftShift));

        GameEvents.instance.enterCorrectLetter += HighlightNextKey;

        HighlightNextKey(-1);
    }

    void Update()
    {
        
    }

    int KeyCodeToKeyboardPos(KeyCode code)
    {
        if (code >= KeyCode.A && code <= KeyCode.Z)
        {
            return (int)(code - KeyCode.A);
        }
        else if (code == KeyCode.Space)
        {
            return 26;
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
