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
        StartCoroutine(FreeKeysThread((float)amount));
    }
    private IEnumerator FreeKeysThread(float t)
    {
        parserManager.AddFreeKeys(99999, true);
        yield return new WaitForSeconds(t);
        parserManager.AddFreeKeys(1, true);
    }
}
