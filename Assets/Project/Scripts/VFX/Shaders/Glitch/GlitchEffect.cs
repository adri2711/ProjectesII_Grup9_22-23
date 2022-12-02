using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class GlitchEffect : MonoBehaviour
{
    [SerializeField] private VolumeProfile on;
    [SerializeField] private VolumeProfile off;
    private float lowTimeEffectDuration = 0.2f;
    public static GlitchEffect instance { get; private set; }
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }
    private void Start()
    {
        GameEvents.instance.lowTimeEffect += LowTimeEffect;
    }

    public void Run(float dur = 0)
    {
        GetComponent<Volume>().profile = on;
        if (dur > 0)
        {
            StartCoroutine(RunForSeconds(dur));
        }
    }
    public void End()
    {
        GetComponent<Volume>().profile = off;
    }
    private IEnumerator RunForSeconds(float dur)
    {
        yield return new WaitForSeconds(dur);
        End();
    }
    private void LowTimeEffect()
    {
        Run(lowTimeEffectDuration);
    }
}