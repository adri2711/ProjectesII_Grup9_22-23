using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyMovement : MonoBehaviour
{
    private Vector2 rootPos;
    void Start()
    {
        rootPos = transform.parent.position;
    }
    public void SetRootPosition(Vector2 newRootPos)
    {
        rootPos = newRootPos;
    }
}
