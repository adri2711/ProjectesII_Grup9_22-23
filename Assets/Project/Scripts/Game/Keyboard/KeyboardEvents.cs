using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardEvents : MonoBehaviour
{
    private KeyboardManager keyboard;
    [SerializeField] private Transform explosionParticles;
    [SerializeField] private float correctKeyLaunchChance = 10;
    [SerializeField] private float wrongKeyLaunchChance = 2;
    void Start()
    {
        keyboard = GetComponent<KeyboardManager>();

        if (GameManager.instance.GetCurrentLevelNum() == 1)
        {
            StartCoroutine(ScrambleKeysDelay(10));
        }
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
    private void LaunchKey(int index, bool particles = true)
    {
        if (index < 0) return;

        KeyPlacement key = keyboard.keys[index].GetComponent<KeyPlacement>();
        if (!key.detachable || !key.inRoot) return;
            
        key.DetachFromRoot();
        key.Launch();

        if (particles)
        {
            Transform p = Instantiate(explosionParticles, key.transform.position, Quaternion.identity, transform);
            Destroy(p.gameObject, 1);
        }
    }
    private IEnumerator ScrambleKeysDelay(int amount)
    {
        yield return new WaitForEndOfFrame();
        ScrambleKeys(amount);
    }
    public void ScrambleKeys(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            LaunchKey(UnityEngine.Random.Range(0, 26), false);
        }
    }
}
