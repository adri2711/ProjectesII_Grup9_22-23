using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class SpeedEffect : MonoBehaviour
{
    [SerializeField] private VolumeProfile[] levels;
    [SerializeField] private VolumeProfile off;
    public static SpeedEffect instance { get; private set; }
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
    }

    public void Run(float percentage, float dur = 0)
    {
        GetComponent<Volume>().profile = levels[(int)(Mathf.Min(percentage, 1f) * (levels.Length - 1))];
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
}
