using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIControl : MonoBehaviour
{

    public static bool paused = false;
    private static bool pauseMenuActive = false;
    private static bool showingPauseMenu = false;
    public static StatsUI statsUI;
    [SerializeField] public Transform MenuInGame;
    [SerializeField] public Transform MenuPause;

    

    void Start(){
        if(MenuInGame == null) MenuInGame =  GameObject.FindWithTag("MenuInGame").transform;
        if(MenuPause == null) MenuPause =  GameObject.FindWithTag("MenuPause").transform;
        if(MenuPause != null) MenuPause.gameObject.SetActive(false);
    }

    void Update()
    {
       if (Input.GetKeyDown(KeyCode.Escape)) PauseGame(true);

        if(pauseMenuActive != showingPauseMenu ) efetivarMenuPause();
    }

    public static void PauseGame(bool showMenu = true)
    {
        paused = !paused;

        Time.timeScale = paused ? 0f : 1f;

        if(paused && showMenu) pauseMenuActive = true;
        else
        if(!paused && pauseMenuActive) pauseMenuActive = false;
    }

    public void ShowMenuInGame(bool show = true)
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


    public void efetivarMenuPause(){
        showingPauseMenu = pauseMenuActive;
        
        MenuPause.gameObject.SetActive(pauseMenuActive);
        
        ShowMenuInGame(!pauseMenuActive);
    }

}
