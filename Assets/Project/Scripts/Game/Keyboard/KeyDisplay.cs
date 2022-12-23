using Mono.Cecil.Cil;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyDisplay : MonoBehaviour
{
    enum KeyState
    {
        NEUTRAL,
        WRONG,
        CORRECT,
        NEXT,
        DETACHED
    }

    KeyState currState = KeyState.NEUTRAL;
    KeyState prevState = KeyState.NEUTRAL;
    public bool freeKey = false;

    [SerializeField] private Material freeKeyMaterial;
    [SerializeField] private Material heldMaterial;
    private Color32 neutral = new Color32(255,255,255,255);
    private Color32 correct = new Color32(100,150,70,255);
    private Color32 wrong = new Color32(150,70,70,255);
    private Color32 next = new Color32(150,255,255,255);
    private Color32 free = new Color32(250, 210, 30, 255);

    [SerializeField] Image image;

    public void PushCorrectLetter()
    {
        UpdateState();
        currState = KeyState.CORRECT;
        UpdateKey();
    }
    public void PushWrongLetter()
    {
        UpdateState();
        currState = KeyState.WRONG;
        UpdateKey();
    }
    public void NextLetter()
    {
        UpdateState();
        if (currState == KeyState.NEUTRAL)
        {
            currState = KeyState.NEXT;
            UpdateKey();
        }
    }
    public void UnhighlightKey()
    {
        if (currState == KeyState.NEXT)
        {
            currState = KeyState.NEUTRAL;
            UpdateKey();
        }
    }
    public void ResetLetter()
    {
        if (prevState == KeyState.WRONG || prevState == KeyState.CORRECT || (prevState == KeyState.NEXT && currState == KeyState.CORRECT))
        {
            prevState = KeyState.NEUTRAL;
        }
        currState = prevState;
        UpdateKey();
    }

    private void UpdateState()
    {
        prevState = currState;
    }
    public void UpdateKey()
    {
        bool inRoot = GetComponent<KeyPlacement>().inRoot;
        if (!inRoot)
        {
            currState = KeyState.DETACHED;
        }
        else if (currState == KeyState.DETACHED)
        {
            currState = KeyState.NEUTRAL;
        }
        switch (currState)
        {
            case KeyState.NEUTRAL:
                image.color = neutral;
                if (freeKey)
                {
                    image.material = freeKeyMaterial;
                }
                else
                {
                    image.material = null;
                }
                break;
            case KeyState.DETACHED:
                image.color = neutral;
                image.material = heldMaterial;
                break;
            case KeyState.WRONG:
                image.color = wrong;
                break;
            case KeyState.CORRECT:
                image.color = correct;
                break;
            case KeyState.NEXT:
                image.color = next;
                break;
        }
        
    }
}
