using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Popup : Interruption
{
    protected void Start()
    {
        Spawn();
    }
    public override void Spawn()
    {
        id = "popup";
        base.Spawn();
        SetRandomDirection();
        GameEvents.instance.PopupSpawn();
    }

    public override void Close()
    {
        animator.Play("close");
        GameEvents.instance.PopupClose();
        base.Close();
    }
}
