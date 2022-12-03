using UnityEngine;

[System.Serializable, CreateAssetMenu(fileName = "CustomPostProcessingMaterials", menuName = "Game/CustomPostProcessingMaterials")]
public class CustomPostProcessingMaterials : UnityEngine.ScriptableObject
{
    //---Your Materials---
    [SerializeField] private Material[] postMaterials;
    private CustomEffectMaterial[] customEffectMaterials;

    //---Accessing the data from the Pass---
    static CustomPostProcessingMaterials _instance;

    public static CustomPostProcessingMaterials Instance
    {
        get
        {
            if (_instance != null) return _instance;
            // TODO check if application is quitting
            // and avoid loading if that is the case

            _instance = UnityEngine.Resources.Load("CustomPostProcessingMaterials") as CustomPostProcessingMaterials;
            return _instance;
        }
    }

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
        customEffectMaterials = new CustomEffectMaterial[postMaterials.Length];
        for (int i = 0; i < customEffectMaterials.Length; i++)
        {
            customEffectMaterials[i] = new CustomEffectMaterial(postMaterials[i]);
        }
    }
}