using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIControl : MonoBehaviour
{

    public static bool paused = false;
    private static bool pauseMenuActive = false;
    public static StatsUI statsUI;
    public static Transform MenuInGame;
    public static Transform MenuPause;

    void Start(){
        MenuInGame =  GameObject.FindWithTag("MenuInGame").transform;
        MenuPause =  GameObject.FindWithTag("MenuPause").transform;
        if(MenuPause != null) MenuPause.gameObject.SetActive(false);
    }

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
            ShowMenuInGame(false);
            MenuPause.gameObject.SetActive(true);
            pauseMenuActive = true;
        }
        else
        if(!paused && pauseMenuActive)
        {
            MenuPause.gameObject.SetActive(false);
            ShowMenuInGame(true);
        }
    }

    public static void ShowMenuInGame(bool show = true)
    {
        MenuInGame.gameObject.SetActive(show);
    }

    public static void WinGame()
    {
        // PauseGame(false);



    }

    // Chamado em VidaPlayer / Morte()
    public static void GameOver()
    {
        PauseGame(false);


    }

    
    public static void AtivarDesativarUI(bool val=true){
        // statsUI.SetActive(valor);
    }


    


}
