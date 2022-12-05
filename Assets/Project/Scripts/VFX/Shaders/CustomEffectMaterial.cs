using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CustomEffectMaterial
{
    public Material material;
    //public int[] propertyIds;

    public CustomEffectMaterial(Material m)
    {
        material = m;
    }
}
