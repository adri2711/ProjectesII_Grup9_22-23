using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntManager : MonoBehaviour
{
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
        foreach (IntSpawner intSpawner in spawners)
        {
            if (!intSpawner.running)
            {
                StartCoroutine(intSpawner.SpawnThread());
            }
        }
    }
}
