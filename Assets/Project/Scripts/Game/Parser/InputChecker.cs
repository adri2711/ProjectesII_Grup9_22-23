using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputChecker : MonoBehaviour
{
    Dictionary<char, HashSet<char>> overrides;
    public static int freeKeys = 0;
    private void Start()
    {
        overrides = new Dictionary<char, HashSet<char>>();
    }
    public void FullDebug()
    {
        Debug.Log("---Full Debug---");
        foreach(var ov in overrides)
        {
            Debug.Log("overrides -" + ov.Key + ":");
            foreach(var o in ov.Value)
            {
                Debug.Log(o+",");
            }
        }
        Debug.Log("----------------");
    }
    public bool Do(char c, string text, int currLetterIndex)
    {
        if (currLetterIndex < text.Length)
        {
            if (freeKeys > 0)
            {
                return true;
            }
            else if (overrides.ContainsKey(text[currLetterIndex]))
            {
                return overrides[text[currLetterIndex]].Contains(c);
            }
            else
            {
                return c == text[currLetterIndex];
            }
        }
        return false;
    }
    public void EnterCorrectLetter()
    {
        if (freeKeys > 0)
        {
            freeKeys--;
        }
    }
    public void DisableKey(int target)
    {
        var targetChar = KeyboardUtil.KeyboardPosToChar(target);
        for (int i = 0; i < targetChar.Length; i++)
        {
            RemoveOverrides(targetChar[i]);
            AddOverride(targetChar[i], (char)0, false);
        }
    }
    public void AddKeyboardOverride(int target, int newOverride, bool additive = false)
    {
        var targetChar = KeyboardUtil.KeyboardPosToChar(target);
        var overrideChar = KeyboardUtil.KeyboardPosToChar(newOverride);
        for (int i = 0; i < Mathf.Min(targetChar.Length,overrideChar.Length); i++)
        {
            RemoveOverrides(targetChar[i]);
            AddOverride(targetChar[i], overrideChar[i], additive);
        }
    }
    public void AddOverride(char target, char newOverride, bool additive = false)
    {
        if (!overrides.ContainsKey(target))
        {
            overrides.Add(target, new HashSet<char>());
        }

        overrides[target].Add(newOverride);
        if (additive)
        {
            overrides[target].Add(target);
        }
    }
    public void RemoveOverrides(char target)
    {
        if (overrides.ContainsKey(target))
        {
            overrides.Remove(target);
        }
    }
    public void AddFreeKeys(int amount, bool overwrite = false)
    {
        if (overwrite)
        {
            freeKeys = 0;
        }
        freeKeys += amount;
    }
}
