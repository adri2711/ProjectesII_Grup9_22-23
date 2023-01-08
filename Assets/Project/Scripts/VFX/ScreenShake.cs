using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    [SerializeField] private AnimationCurve curve;
    [SerializeField] private float dur = 0.3f;
    [SerializeField] private float lowTimeEffectDur = 0.4f;
    private float shakeTimer = 0;
    Vector3 startPos;
    private void Start()
    {
        startPos = transform.position;
        GameEvents.instance.enterWrongLetter += Shake;
    }
    private void Update()
    {
        if (TimerManager.instance != null && TimerManager.instance.IsActive() && TimerManager.instance.GetCurrTime() < TimerManager.instance.GetMaxTime() / 2)
        {
            shakeTimer += Time.deltaTime;
            if (shakeTimer >= dur)
            {
                shakeTimer = 0;
                if (RandomUtil.Roll(15f))
                {
                    GameEvents.instance.LowTimeEffect();
                    StartCoroutine(ShakeTransformUtil.ShakeCoroutine(transform, lowTimeEffectDur, 0.5f, startPos, curve));
                }
                if (TimerManager.instance.GetCurrTime() < TimerManager.instance.GetMaxTime() / 3)
                {
                    GameEvents.instance.LowTimeTick();
                }
            }
        }
    }

    private void Shake(int c)
    {
        StartCoroutine(ShakeTransformUtil.ShakeCoroutine(transform, dur, 2f, startPos, curve));
    }
}
