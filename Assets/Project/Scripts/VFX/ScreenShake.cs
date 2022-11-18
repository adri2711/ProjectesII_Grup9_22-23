using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    [SerializeField] private AnimationCurve curve;
    [SerializeField] private float dur = 0.3f;
    [SerializeField] private float cooldown = 0.2f;
    private bool active = false;
    Vector3 startPos;
    private void Start()
    {
        startPos = transform.position;
        GameEvents.instance.enterWrongLetter += Shake;
    }
    private void Update()
    {
        if (TimerManager.instance != null && TimerManager.instance.IsActive() && TimerManager.instance.GetCurrTime() < TimerManager.instance.GetMaxTime() / 3)
        {
            if (!active)
            {
                StartCoroutine(ShakeCoroutine(1f, 0.2f));
                if (Random.Range(0,9) < 2)
                {
                    GameEvents.instance.LowTimeEffect();
                    StartCoroutine(ShakeCoroutine(0.4f, 2f));
                }
            }
        }
    }

    private void Shake(int c)
    {
        StartCoroutine(ShakeCoroutine(dur, 1f));
    }

    private IEnumerator ShakeCoroutine(float dur, float intensity)
    {
        float elapsedTime = 0f;

        while (elapsedTime < dur)
        {
            active = true;
            elapsedTime += Time.deltaTime;
            float strength = curve.Evaluate(elapsedTime / dur);
            transform.position = startPos + Random.insideUnitSphere * strength * intensity;
            yield return null;
        }

        transform.position = startPos;
        active = false;
    }
}
