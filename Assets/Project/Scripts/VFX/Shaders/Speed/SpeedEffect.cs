using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class SpeedEffect : MonoBehaviour
{
    [SerializeField] private VolumeProfile[] levels;
    private int index;
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
        End();
    }

    public void Run(float percentage, float dur = 0)
    {
        index = (int)(Mathf.Min(percentage, 1f) * (levels.Length - 1));
        GetComponent<Volume>().profile = levels[index];
        levels[index].components[0].SetAllOverridesTo(true);

        if (dur > 0)
        {
            StartCoroutine(RunForSeconds(dur));
        }
    }
    public void End()
    {
        levels[index].components[0].SetAllOverridesTo(false);
    }
    private IEnumerator RunForSeconds(float dur)
    {
        yield return new WaitForSeconds(dur);
        End();
    }
}
