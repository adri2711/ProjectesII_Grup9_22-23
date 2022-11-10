using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    }

    private bool active = false;
    [SerializeField] private Transform staticPopup;
    [SerializeField] private Transform bouncingPopup;
    private IntSpawner[] spawners = new IntSpawner[0];

    void Start()
    {
        Array.Resize(ref spawners, 2);
        IntSpawner staticPopupSpawner = new IntSpawner(staticPopup, 10, 15, new Vector2(0,0), 200);
        spawners.SetValue(staticPopupSpawner, 0);
        IntSpawner bouncingPopupSpawner = new IntSpawner(bouncingPopup, 20, 20, new Vector2(0, 0), 0);
        spawners.SetValue(bouncingPopupSpawner, 1);
        if (GameManager.instance.GetCurrentLevel() == 1)
        {
            Transform intTransform = GameObject.Instantiate(staticPopup, new Vector2(0, 0), Quaternion.identity, GameObject.Find("IntManager").transform);
            intTransform.gameObject.GetComponent<Interruption>().SetPosition(new Vector3(-70,90,0));
        }
    }
    void Update()
    {
        intCount = this.transform.childCount;
        if (active)
        {
            foreach (IntSpawner intSpawner in spawners)
            {
                if (!intSpawner.running)
                {
                    StartCoroutine(intSpawner.SpawnThread());
                }
            }
        }
    }
    public void Activate()
    {
        active = true;
        foreach (IntSpawner intSpawner in spawners)
        {
            intSpawner.Activate();
        }
    }
    public void Deactivate()
    {
        active = false;
        foreach (IntSpawner intSpawner in spawners)
        {
            intSpawner.Deactivate();
        }
    }
    public int GetIntCount()
    {
        return intCount;
    }
}
