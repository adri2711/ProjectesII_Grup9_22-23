using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParserEvents : MonoBehaviour
{
    private ParserManager parserManager;
    void Start()
    {
        parserManager = GetComponent<ParserManager>();

        GameEvents.instance.streakFreeKeys += FreeKeys;
    }
    private void OnDestroy()
    {
        GameEvents.instance.streakFreeKeys -= FreeKeys;
    }
    public void FreeKeys(int amount)
    {
        parserManager.AddFreeKeys(amount, true);
    }
}
