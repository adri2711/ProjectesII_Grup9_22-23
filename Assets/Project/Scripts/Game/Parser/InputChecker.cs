using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class InputChecker : MonoBehaviour
{
    Dictionary<char, HashSet<char>> overrides;
    int freeKeys = 0;
    private void Start()
    {
        overrides = new Dictionary<char, HashSet<char>>();
        AddOverride('e', 'r');
    }
    public bool Do(char c, string text, int currLetterIndex)
    {
        if (currLetterIndex < text.Length)
        {
            if (freeKeys > 0)
            {
                freeKeys--;
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
    public void AddOverride(char target, char newOverride, bool additive = true)
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
