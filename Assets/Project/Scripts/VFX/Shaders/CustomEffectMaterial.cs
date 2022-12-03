using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CustomEffectMaterial
{
    public Material material;
    public int[] propertyIds;

    public CustomEffectMaterial(Material m)
    {
        material = m;
        foreach (string a in m.shader.GetPropertyAttributes(0)) {
            Debug.Log(a);
        }
    }
}
