using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntManager : MonoBehaviour
{
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
        IntSpawner staticPopupSpawner = new IntSpawner(staticPopup, 10, 15, new Vector2(0,0), 50);
        spawners.SetValue(staticPopupSpawner, 0);
        IntSpawner bouncingPopupSpawner = new IntSpawner(bouncingPopup, 20, 20, new Vector2(0, 0), 0);
        spawners.SetValue(bouncingPopupSpawner, 1);
    }
    void Update()
    {
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
    }
    public void Deactivate()
    {
        active = false;
    }
}
