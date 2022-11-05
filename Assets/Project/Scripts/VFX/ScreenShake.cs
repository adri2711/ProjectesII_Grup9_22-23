using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    [SerializeField] private AnimationCurve curve;
    [SerializeField] private float dur = 0.3f;
    [SerializeField] private float cooldown = 0.2f;
    private bool active = false;
    private void Start()
    {
        GameEvents.instance.enterWrongLetter += Shake;
    }

    private void Shake(int c)
    {
        if (!active)
        {
            StartCoroutine(ShakeCoroutine(dur));
        }
    }

    private IEnumerator ShakeCoroutine(float dur)
    {
        active = true;
        Vector3 startPos = transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < dur)
        {
            elapsedTime += Time.deltaTime;
            float strength = curve.Evaluate(elapsedTime / dur);
            transform.position = startPos + Random.insideUnitSphere * strength;
            yield return null;
        }

        transform.position = startPos;

        yield return new WaitForSeconds(cooldown);

        active = false;
    }
}
