using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class CustomEffect<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] protected VolumeProfile[] levels;
    protected int index = 0;
    public static T instance { get; private set; }
    protected virtual void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
            return;
        }
        instance = this as T;
        DontDestroyOnLoad(this.gameObject);
    }
    protected virtual void Start()
    {
        GetComponent<Volume>().profile = levels[index];
        End();
    }

    public virtual void Run(float dur = 0)
    {
        GetComponent<Volume>().profile = levels[index];
        foreach (VolumeComponent component in levels[index].components)
        {
            component.SetAllOverridesTo(true);
        }

        if (dur > 0)
        {
            StartCoroutine(RunForSeconds(dur));
        }
    }
    public virtual void End()
    {
        GetComponent<Volume>().profile = levels[index];
        foreach (VolumeComponent component in levels[index].components)
        {
            component.SetAllOverridesTo(false);
        }
    }
    protected IEnumerator RunForSeconds(float dur)
    {
        yield return new WaitForSeconds(dur);
        PostRunForSeconds();
    }
    protected virtual void PostRunForSeconds()
    {
        End();
    }
}
