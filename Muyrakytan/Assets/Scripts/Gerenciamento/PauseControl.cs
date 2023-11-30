using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseControl : MonoBehaviour
{

    public static bool paused = false;

    void Update()
    {
       if (Input.GetKeyDown(KeyCode.Escape)) PauseGame(true);
    }

    public static void PauseGame(bool showMenu = true)
    {
        paused = !paused;

        Time.timeScale = paused ? 0f : 1f;

        if(paused && showMenu)
        {
            // mostrar o menu de pause
        }
    }
}
