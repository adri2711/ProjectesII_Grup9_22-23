using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Popup : Interruption
{
    [Space(10)] [Header("Escape Events")]
    [SerializeField] private int closeCharges = 0;
    [Space(10)] [Header("Close:")]
    [SerializeField] private bool escapeOnClose = false;
    [SerializeField] private float escapeSpeed = 10f;
    [SerializeField] private float escapeDuration = 0.3f;
    [Space(10)] [Header("Grow:")]
    [SerializeField] private bool growOnClose = false;
    [SerializeField] private Vector2 growFactor = new Vector2(1.3f,1.3f);
    [Space(10)] [Header("Flip:")]
    [SerializeField] private bool flipOnClose = false;
    [SerializeField] private Vector2 flipAxis = Vector2.zero;

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
        if (closeCharges > 0)
        {
            closeCharges--;
            if (escapeOnClose)
            {
                Escape(escapeSpeed, escapeDuration);
            }
            if (growOnClose)
            {
                Grow(growFactor);
            }
            if (flipOnClose)
            {
                Flip(flipAxis);
            }
        }
        else
        {
            animator.Play("close");
            GameEvents.instance.PopupClose();
            base.Close();
        }
    }

    protected virtual void Escape(float speed, float duration)
    {
        SetRandomDirection();
        SetSpeedForDuration(speed, duration);
    }
    protected virtual void Grow(Vector2 factor)
    {
        GetComponentInChildren<Image>().rectTransform.sizeDelta *= factor;
    }
    protected virtual void Flip(Vector2 axis)
    {
        RectTransform buttonRect = GetComponentInChildren<Button>().GetComponent<Image>().rectTransform;
        SetPosition(GetPosition() + new Vector2(GetComponentInChildren<Image>().rectTransform.rect.width, GetComponentInChildren<Image>().rectTransform.rect.height) * (buttonRect.pivot * 2 - new Vector2(1,1)) * axis);
        buttonRect.anchorMin += axis * ((Vector2.one - buttonRect.anchorMin) * 2 - Vector2.one);
        buttonRect.anchorMax += axis * ((Vector2.one - buttonRect.anchorMax) * 2 - Vector2.one);
        buttonRect.pivot += axis * ((Vector2.one - buttonRect.pivot) * 2 - Vector2.one);
    }
}
