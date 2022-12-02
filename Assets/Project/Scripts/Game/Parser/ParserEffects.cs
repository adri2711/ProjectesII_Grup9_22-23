using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.UI;

public class ParserEffects : MonoBehaviour
{
    [SerializeField] Transform correctParticles;
    private RectTransform parserTransform;
    private Vector2 charPos;
    private void Start()
    {
        parserTransform = transform.parent.GetComponentInChildren<Image>().rectTransform;
        ResetCharPos();
    }
    public void CorrectLetterEffect(int currLetterIndex)
    {
        Transform p = Instantiate(correctParticles, charPos, Quaternion.identity, transform);
        Destroy(p.gameObject, 1);
        charPos.x += 0.33f;
    }
    public void ChangeRenderSegment()
    {
        ResetCharPos();
    }
    private void ResetCharPos()
    {
        charPos = parserTransform.position;
        charPos.x -= parserTransform.sizeDelta.x / 106f;
    }
}
