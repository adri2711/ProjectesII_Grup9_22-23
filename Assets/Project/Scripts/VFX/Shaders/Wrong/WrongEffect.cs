using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class WrongEffect : CustomEffect<WrongEffect>
{
    [SerializeField] private float dur = 0.2f;
    protected override void Start()
    {
        base.Start();
        GameEvents.instance.enterWrongLetter += WrongLetter;
    }

    private void WrongLetter(int p)
    {
        Run(dur);
    }
}
