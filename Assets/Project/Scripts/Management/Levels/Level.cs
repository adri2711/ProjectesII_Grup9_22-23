using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Level : MonoBehaviour
{
    [SerializeField] protected float levelTime = 8f;
    public virtual void LevelStart()
    {

    }
    public virtual void LevelUpdate()
    {

    }
    protected abstract void ActivateLevel();
}
