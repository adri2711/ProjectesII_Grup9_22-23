using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameState : MonoBehaviour
{
    public abstract void Enter();
    public abstract void Exit();
    public abstract void UpdateState();
}
