using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Key : MonoBehaviour
{
    enum KeyState
    {
        NEUTRAL,
        WRONG,
        CORRECT,
        NEXT
    }

    KeyState state = KeyState.NEUTRAL;
    private bool goBackToNext = false;
    

    private Color32 neutral = new Color32(255,255,255,255);
    private Color32 correct = new Color32(100,150,70,255);
    private Color32 wrong = new Color32(150,70,70,255);
    private Color32 next = new Color32(150,255,255,255);

    [SerializeField] Image image;

    public void PushCorrectLetter()
    {
        state = KeyState.CORRECT;
        UpdateKey();
    }
    public void PushWrongLetter()
    {
        if (state == KeyState.NEXT)
        {
            goBackToNext = true;
        }
        state = KeyState.WRONG;
        UpdateKey();
    }
    public void ResetLetter()
    {
        state = KeyState.NEUTRAL;

        if (goBackToNext)
        {  
            state = KeyState.NEXT;
        }
        goBackToNext = false;

        UpdateKey();
    }
    public void NextLetter()
    {
        if (state == KeyState.NEUTRAL)
        {
            state = KeyState.NEXT;
            UpdateKey();
        }
    }
    public void UnhighlightKey()
    {
        if (state == KeyState.NEXT)
        {
            state = KeyState.NEUTRAL;
            UpdateKey();
        }
    }
    public void UpdateKey()
    {
        if (image != null) // Fake af
        {
            switch (state)
            {
                case KeyState.NEUTRAL:
                    image.color = neutral;
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
}
