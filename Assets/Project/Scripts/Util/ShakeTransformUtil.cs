using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class ShakeTransformUtil : MonoBehaviour
{
    public static IEnumerator ShakeCoroutine(Transform target, float dur, float intensity, Vector3 startPos, AnimationCurve curve = null)
    {
        float elapsedTime = 0f;

        while (elapsedTime < dur)
        {
            elapsedTime += Time.deltaTime;
            float strength = elapsedTime / dur;
            if (curve != null)
            {
                strength = curve.Evaluate(elapsedTime / dur);
            }
            target.localPosition = startPos + Random.insideUnitSphere * strength * intensity;
            yield return null;
        }

        target.localPosition = startPos;
    }
}
