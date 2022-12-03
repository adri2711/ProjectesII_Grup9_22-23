using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class WrongEffect : MonoBehaviour
{
    [SerializeField] private VolumeProfile vol;
    private float duration = 0.2f;
    public static WrongEffect instance { get; private set; }
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
        GameEvents.instance.enterWrongLetter += WrongLetter;
        GetComponent<Volume>().profile = vol;
        End();
    }

    public void Run(float dur = 0)
    {
        vol.components[0].SetAllOverridesTo(true);
        if (dur > 0)
        {
            StartCoroutine(RunForSeconds(dur));
        }
    }
    public void End()
    {
        vol.components[0].SetAllOverridesTo(false);
    }
    private IEnumerator RunForSeconds(float dur)
    {
        yield return new WaitForSeconds(dur);
        End();
    }
    private void WrongLetter(int p)
    {
        Run(duration);
    }
}
