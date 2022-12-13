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
    private Transform pt;
    private Transform pts;
    private Vector2 charPos;
    private void Start()
    {
        GameEvents.instance.lowTimeTick += LowTimeTick;
        parserTransform = transform.parent.GetComponentInChildren<Image>().rectTransform;
        pt = transform.parent.GetComponentInChildren<Image>().transform;
        pts = transform.parent.GetComponentInChildren<Image>().transform;
        ResetCharPos();
    }
    private void OnDestroy()
    {
        GameEvents.instance.lowTimeTick -= LowTimeTick;
    }
    public void LowTimeTick()
    {
        StartCoroutine(ShakeTransformUtil.ShakeCoroutine(pt, 0.2f, 5f, pts.localPosition));
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
