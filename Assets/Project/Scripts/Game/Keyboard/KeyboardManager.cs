using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class KeyboardManager : MonoBehaviour
{
    [SerializeField] public List<KeyDisplay> keys;
    private ParserManager pm;
    private KeyboardEvents keyboardEvents;

    void Start()
    {
        pm = transform.parent.GetComponentInChildren<ParserManager>();
        keyboardEvents = GetComponent<KeyboardEvents>();

        GameEvents.instance.enterCorrectLetter += HighlightNextKey;
        GameEvents.instance.streakFreeKeys += FreeKeys;

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
                            keyboardEvents.PressLetter(keyPos, true);
                        }
                        else
                        {
                            keys[keyPos].PushWrongLetter();
                            keyboardEvents.PressLetter(keyPos, false);
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
        foreach (KeyDisplay key in keys)
        {
            key.UnhighlightKey();
        }
    }
    public void FreeKeys(int amount)
    {
        foreach (KeyDisplay key in keys)
        {
            key.freeKey = true;
            key.UpdateKey();
        }
    }
    private void ResetFreeKeys()
    {
        foreach (KeyDisplay key in keys)
        {
            key.freeKey = false;
            key.UpdateKey();
        }
    }
}
