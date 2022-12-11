using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CustomEffectMaterial
{
    public Material material;
    public Dictionary<string, int> propertyIds = new Dictionary<string, int>();
    private static HashSet<string> internalProperties = new HashSet<string>() {"_MainTex"};

    public CustomEffectMaterial(Material m)
    {
        material = m;
        for (int i = 0; i < material.shader.GetPropertyCount(); i++)
        {
            string name = material.shader.GetPropertyName(i);
            if (!internalProperties.Contains(name))
            {
                propertyIds[name] = Shader.PropertyToID(name);
            }
        }
    }
}
