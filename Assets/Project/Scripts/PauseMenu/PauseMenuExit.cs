using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuExit : MonoBehaviour
{
 public void QuitGame()
    {
        GetComponent<PauseMenu>().Resume();
        GameManager.instance.SetGameState("Desktop");
    }
}
