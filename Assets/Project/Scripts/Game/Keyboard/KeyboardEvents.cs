using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardEvents : MonoBehaviour
{
    private KeyboardManager keyboard;
    [SerializeField] private float correctKeyLaunchChance = 10;
    [SerializeField] private float wrongKeyLaunchChance = 2;
    void Start()
    {
        keyboard = GetComponent<KeyboardManager>();
    }
    public void PressLetter(int index, bool correct)
    {
        float chance = correct ? correctKeyLaunchChance : wrongKeyLaunchChance;
        if (RandomUtil.Roll(chance))
        {
            StartCoroutine(LaunchDelay(index));
        }
    }
    private IEnumerator LaunchDelay(int index)
    {
        yield return new WaitForEndOfFrame();
        LaunchKey(index);
    }
    private void LaunchKey(int rootIndex)
    {
        KeyRoot root = KeyboardRoots.keyRoots[rootIndex];
        int index = root.currIndex;
        if (index >= 0)
        {
            KeyPlacement key = keyboard.keys[index].GetComponent<KeyPlacement>();
            if (key.detachable)
            {
                key.DetachFromRoot();
            }
        }
    }
}
