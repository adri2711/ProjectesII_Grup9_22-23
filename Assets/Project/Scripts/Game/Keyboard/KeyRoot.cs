using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyRoot : MonoBehaviour
{
    public int index;
    public Vector2 pos;

    public bool HasKey()
    {
        return index >= 0;
    }
}
