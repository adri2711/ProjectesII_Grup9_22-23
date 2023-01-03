using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameConstants : MonoBehaviour
{
    public static Vector2 screenBottomLeftCorner = new Vector2(-140, -85);
    public static Vector2 screenTopRightCorner = new Vector2(280, 215);
    public static Vector2 screenCenter = (screenTopRightCorner - screenBottomLeftCorner) / 2 + screenBottomLeftCorner;
}
