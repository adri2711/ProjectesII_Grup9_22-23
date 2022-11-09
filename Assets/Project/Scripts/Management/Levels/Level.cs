using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
