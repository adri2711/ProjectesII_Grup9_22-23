using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public abstract class GameState : MonoBehaviour
{
    protected List<string> scenes = new List<string>();
    public abstract void Enter();
    public abstract void Exit();
    public abstract void UpdateState();
    protected void LoadScenes()
    {
        foreach (string s in scenes)
        {
            if (!SceneManager.GetSceneByName(s).isLoaded)
                SceneManager.LoadSceneAsync(s, LoadSceneMode.Additive);
        }
    }
    protected void UnloadScenes(bool wait = false)
    {
        foreach (string s in scenes)
        {
            if (SceneManager.GetSceneByName(s).isLoaded)
                SceneManager.UnloadSceneAsync(s);
        }
        while (wait && CheckUnloaded()) { }
    }
    protected bool CheckLoaded()
    {
        foreach (string s in scenes)
        {
            if (!SceneManager.GetSceneByName(s).isLoaded)
            {
                return false;
            }
        }
        return true;
    }
    protected bool CheckUnloaded()
    {
        foreach (string s in scenes)
        {
            if (SceneManager.GetSceneByName(s).isLoaded)
            {
                return false;
            }
        }
        return true;
    }
}
   
