using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomMaterialManager : MonoBehaviour
{
    public static CustomMaterialManager instance { get; private set; }
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
    private CustomEffectMaterial[] customEffectMaterials;
    public CustomEffectMaterial[] GetCustomEffectMaterials()
    {
        if (customEffectMaterials == null)
        {
            SetupCustomEffectMaterials();
        }
        return customEffectMaterials;
    }
    private void SetupCustomEffectMaterials()
    {
        var postMaterials = CustomPostProcessingMaterials.Instance.postMaterials;
        customEffectMaterials = new CustomEffectMaterial[postMaterials.Length];
        for (int i = 0; i < customEffectMaterials.Length; i++)
        {
            customEffectMaterials[i] = new CustomEffectMaterial(postMaterials[i]);
        }
    }
}
