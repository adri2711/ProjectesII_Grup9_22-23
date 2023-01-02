using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using System.IO;

[System.Serializable]
public class IntSpawnerArray
{
    public IntSpawner[] value;
}

public class IntManager : MonoBehaviour
{
    private int intCount = 0;
    public static IntManager instance { get; private set; }
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
        DontDestroyOnLoad(this.gameObject);
    }

    private bool active = false;
    [SerializeField] private Transform staticPopup;
    [SerializeField] private Transform bouncingPopup;

    [SerializeField] private List<Transform> interruptions = new List<Transform>();
    private IntSpawnerArray spawners = new IntSpawnerArray();

    void Start()
    {

    }
    public void Setup()
    {
        //Reset
        DestroyAllInterruptions();
        intCount = 0;
        spawners.value = new IntSpawner[0];

        //Read spawners from json
        string jsonPath = Application.streamingAssetsPath + "/Data/level" + LevelState.currLevel + "Interruptions.json";
        string jsonText = File.ReadAllText(jsonPath);
        spawners = JsonUtility.FromJson<IntSpawnerArray>(jsonText);

        //Set up read spawners
        foreach (IntSpawner readSpawner in spawners.value)
        {
            Transform intTransform = GetSpawnObject(readSpawner.objectName);
            if (intTransform != null)
            {
                readSpawner.spawnedObject = intTransform;
            }
        }

        //Special cases
        if (LevelState.currLevel == 2 || LevelState.currLevel == 3)
        {
            Transform intTransform = GameObject.Instantiate(staticPopup, new Vector2(0, 0), Quaternion.identity, GameObject.Find("popup").transform);
            intTransform.gameObject.GetComponent<Interruption>().SetPosition(new Vector2(15, 120));
        }
    }
    private Transform GetSpawnObject(string name)
    {
        foreach (Transform intTransform in interruptions)
        {
            if (intTransform.gameObject.name == name)
            {
                return intTransform;
            }
        }
        return null;
    }
    public void DestroyAllInterruptions()
    {
        foreach (Interruption obj in GetComponentsInChildren<Interruption>())
        {
            Destroy(obj.gameObject);
        }
    }
    void Update()
    {
        if (active)
        {
            foreach (IntSpawner intSpawner in spawners.value)
            {
                if (!intSpawner.running)
                {
                    StartCoroutine(intSpawner.SpawnThread());
                    intCount++;
                }
            }
        }
    }
    public void Activate()
    {
        active = true;
        foreach (IntSpawner intSpawner in spawners.value)
        {
            intSpawner.Activate();
        }
    }
    public void Deactivate()
    {
        active = false;
        foreach (IntSpawner intSpawner in spawners.value)
        {
            intSpawner.Deactivate();
        }
    }
    public int GetIntCount()
    {
        return intCount;
    }
}
