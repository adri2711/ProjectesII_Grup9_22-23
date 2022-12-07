using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class CustomEffect<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] protected VolumeProfile[] profiles;
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
        GetComponent<Volume>().profile = profiles[index];
        End();
    }

    public virtual void Run(float dur = 0)
    {
        GetComponent<Volume>().profile = profiles[index];
        foreach (VolumeComponent component in profiles[index].components)
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
        GetComponent<Volume>().profile = profiles[index];
        foreach (VolumeComponent component in profiles[index].components)
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

    protected void SetParameter<PT>(PT val, int paramIndex, int component = 0)
    {
        var param = profiles[index].components[component].parameters[paramIndex];
        if (param != null)
        {
            VolumeParameter<PT> p = new VolumeParameter<PT>();
            p.Override(val);
            profiles[index].components[component].parameters[paramIndex].SetValue(p);
        }
    }
}
