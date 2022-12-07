using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class KeyboardManager : MonoBehaviour
{
    [SerializeField] private List<Key> keys;
    public static List<KeyRoot> keyRoots;
    private ParserManager pm;

    void Start()
    {
        pm = transform.parent.parent.gameObject.GetComponentInChildren<ParserManager>();

        GameEvents.instance.enterCorrectLetter += HighlightNextKey;
        GameEvents.instance.streakFreeKeys += FreeKeys;

        keyRoots = new List<KeyRoot>();
        for (int i = 0; i < keys.Count; i++)
        {
            keyRoots[i].pos = keys[i].transform.parent.transform.position;
            keyRoots[i].index = i;
        }

        HighlightNextKey(-1);
    }

    private void OnDestroy()
    {
        GameEvents.instance.enterCorrectLetter -= HighlightNextKey;
        GameEvents.instance.streakFreeKeys -= FreeKeys;
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
            int index = KeyboardUtil.CharToKeyboardPos(s[0]);
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
            int keyPos = KeyboardUtil.KeyCodeToKeyboardPos(e.keyCode);
            if (e.isKey && keyPos >= 0)
            {
                if (e.type == EventType.KeyDown)
                {
                    //Key pressed
                    int keyInt = KeyboardUtil.ProcessInputCode(e.keyCode);
                    if (keyInt >= 0)
                    {
                        if (pm.VerifyKey((char)keyInt))
                        {
                            keys[keyPos].PushCorrectLetter();
                        }
                        else
                        {
                            keys[keyPos].PushWrongLetter();
                        }
                    }
                }
                else if (e.type == EventType.KeyUp)
                {
                    //Key released
                    keys[keyPos].ResetLetter();
                }
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
}
