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
    private void LaunchKey(int index)
    {
        if (index < 0) return;

        KeyPlacement key = keyboard.keys[index].GetComponent<KeyPlacement>();
        if (!key.detachable) return;
            
        key.DetachFromRoot();
        key.Launch();
    }
}
