using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

public class KeyRoot : MonoBehaviour
{
    public int keyboardIndex { private set; get; }
    public int currIndex { private set; get; }
    public Vector2 pos;

    private InputChecker inputChecker;

    public KeyRoot Setup(int index)
    {
        name = "KeyRoot" + index;
        keyboardIndex = currIndex = index;
        pos = transform.localPosition;
        inputChecker = GetComponentInParent<KeyboardManager>().transform.parent.GetComponentInChildren<InputChecker>();
        return this;
    }
    public void DetachKey()
    {
        //Debug.Log("detach" + keyboardIndex + ": " + currIndex);
        currIndex = -1;

        inputChecker.DisableKey(keyboardIndex);
        GameEvents.instance.DetachKey(3);
    }
    public void AttachKey(int index)
    {
        currIndex = index;
        //Debug.Log("attach" + keyboardIndex + ": " + currIndex);

        inputChecker.AddKeyboardOverride(keyboardIndex, currIndex, false);
    }
    public bool HasKey()
    {
        return currIndex >= 0;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!HasKey())
        {
            var key = collision.GetComponent<KeyPlacement>();
            if (key != null && !key.inRoot)
            {
                if (!key.held && key.movementTime <= 0)
                {
                    key.rootPos = pos;
                    AttachKey(key.index);
                    key.AttachToRoot(keyboardIndex);
                }
            }
        }
    }
}
