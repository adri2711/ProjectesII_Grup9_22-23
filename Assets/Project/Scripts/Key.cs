using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Key : MonoBehaviour
{
    [SerializeField] Image image;

    private Color empty = new Color32(255,255,255,255);
    private Color correct = new Color32(100,150,70,255);
    private Color wrong = new Color32(150,70,70,255);
    private Color next = new Color32(150,255,255,255);
    public void PushCorrectLetter()
    {
        image.color = correct;
    }
    public void PushWrongLetter()
    {
        image.color = wrong;
    }
    public void ResetLetter()
    {
        image.color = empty;
    }
    public void NextLetter()
    {
        image.color = next;
    }
}
