using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ParserEvents : MonoBehaviour
{
    private ParserManager parserManager;
    void Start()
    {
        parserManager = GetComponent<ParserManager>();

        GameEvents.instance.streakFreeKeys += FreeKeys;
        GameEvents.instance.enterWrongLetter += WrongLetter;
    }
    private void OnDestroy()
    {
        GameEvents.instance.streakFreeKeys -= FreeKeys;
        GameEvents.instance.enterWrongLetter -= WrongLetter;
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
    public void WrongLetter(int p)
    {
        if (GameManager.instance.GetCurrentLevelNum() != 3) return;
        RectTransform parserRect = GetComponentInChildren<Image>().rectTransform;
        parserRect.rotation = new Quaternion(0, 1f - parserRect.rotation.y, 0, 0);
    }
}
