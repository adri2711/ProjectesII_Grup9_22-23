using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static GameEvents instance { get; private set; }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    public event Action<int> enterCorrectLetter;
    public event Action<int> enterWrongLetter;

    public void EnterCorrectLetter(int pos)
    {
        if (enterCorrectLetter != null)
        {
            enterCorrectLetter(pos);
        }
    } 
    public void EnterWrongLetter(int pos)
    {
        if (enterWrongLetter != null)
        {
            enterWrongLetter(pos);
        }
    } 
}
