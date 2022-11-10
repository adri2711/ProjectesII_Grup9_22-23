using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Popup : Interruption
{
    public float baseSpeed = 0f;
    public float bounce = 1f;
    protected float addSpeed;
    protected Vector2 dir;

    private void Start()
    {   
        Spawn();
    }
    public override void Spawn()
    {
        id = "Popup";
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

    private void FixedUpdate()
    {
        if (baseSpeed > 0f)
        {
            MovementUpdate();
        }
    }
    protected void MovementUpdate()
    {
        Vector3 movement = dir * (baseSpeed + addSpeed);

        addSpeed -= 0.1f;
        if (addSpeed < 0)
        {
            addSpeed = 0;
        }

        Vector2 edges = new Vector2(GetComponent<RectTransform>().rect.width, GetComponent<RectTransform>().rect.height);
        Vector2 popupPos = GetComponentInChildren<Image>().transform.localPosition;
        Vector2 popupSize = new Vector2(GetComponentInChildren<Image>().rectTransform.rect.x, GetComponentInChildren<Image>().rectTransform.rect.y);
        if (Math.Abs(popupPos.x) + popupSize.x >= edges.x && Math.Ceiling(dir.normalized.x) == Math.Ceiling(popupPos.normalized.x))
        {
            dir.x = -dir.x;
            addSpeed += bounce;
            GameEvents.instance.PopupBounce();
        }
        else if (Math.Abs(popupPos.y) + popupSize.y >= edges.y && Math.Ceiling(dir.normalized.y) == Math.Ceiling(popupPos.normalized.y))
        {
            dir.y = -dir.y;
            addSpeed += bounce;
            GameEvents.instance.PopupBounce();
        }

        GetComponentInChildren<Image>().transform.localPosition += movement;
    }
    protected void SetRandomDirection()
    {
        dir = new Vector2(UnityEngine.Random.Range(-1f,1f), UnityEngine.Random.Range(-1f,1f)).normalized;
    }
}
