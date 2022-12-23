using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Popup : Interruption
{
    [SerializeField] private int closeEvents = 0;
    [SerializeField] private bool escapeOnClose = false;
    [SerializeField] private float escapeSpeed = 10f;
    [SerializeField] private float escapeDuration = 0.3f;

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
        if (closeEvents > 0)
        {
            closeEvents--;
            if (escapeOnClose)
            {
                Escape();
            }
        }
        else
        {
            animator.Play("close");
            GameEvents.instance.PopupClose();
            base.Close();
        }
    }

    protected virtual void Escape()
    {
        SetRandomDirection();
        SetSpeedForDuration(escapeSpeed, escapeDuration);
    }
}
