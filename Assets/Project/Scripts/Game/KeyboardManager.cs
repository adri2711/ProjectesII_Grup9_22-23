using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

public class KeyboardManager : MonoBehaviour
{
    [SerializeField] List<Key> keys;

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
        string s = ParserManager.instance.GetChar(pos+1).ToString().ToUpper();
        int index = -1;
        if (s[0] >= 'A' && s[0] <= 'Z') {
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

    void OnGUI()
    {
        Event e = Event.current;
        if (e.isKey && KeyCodeToKeyboardPos(e.keyCode) >= 0)
        {
            if (e.type == EventType.KeyDown)
            {
                //Key pressed
                if (ParserManager.instance.VerifyKey(e.keyCode))
                {
                    keys[KeyCodeToKeyboardPos(e.keyCode)].PushCorrectLetter();
                }
                else
                {
                    keys[KeyCodeToKeyboardPos(e.keyCode)].PushWrongLetter();
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
