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
            int indexInput = KeyboardUtil.CharToKeyboardPos(s[0]);
            if (indexInput >= 0)
            {
                keys[indexInput].NextLetter();
            }
        }
    }

    void OnGUI()
    {
        if (LevelState.parserActive && !PauseMenu.gameIsPaused)
        {
            Event e = Event.current;
            int indexInput = KeyboardUtil.KeyCodeToKeyboardPos(e.keyCode);
            int indexRoot = KeyboardRoots.FindRoot(indexInput);
            if (e.isKey && indexRoot >= 0)
            {
                if (e.type == EventType.KeyDown)
                {
                    //Key pressed
                    int indexChecked = KeyboardUtil.ProcessInputCode(e.keyCode);
                    if (indexChecked >= 0)
                    {
                        if (pm.VerifyKey((char)indexChecked))
                        {
                            keys[indexRoot].PushCorrectLetter();
                            keyboardEvents.PressLetter(indexRoot, true);
                        }
                        else
                        {
                            keys[indexRoot].PushWrongLetter();
                            keyboardEvents.PressLetter(indexRoot, false);
                        }
                    }
                }
                else if (e.type == EventType.KeyUp)
                {
                    //Key released
                    keys[indexRoot].ResetLetter();
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
