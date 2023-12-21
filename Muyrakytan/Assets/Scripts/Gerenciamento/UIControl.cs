using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIControl : MonoBehaviour
{

    public static bool paused = false;
    private static bool pauseMenuActive = false;
    private static bool showingPauseMenu = false;
    public static bool endGame = false;
    public static StatsUI statsUI;
    [SerializeField] public GameObject MenuInGame = null;
    [SerializeField] public GameObject MenuPause = null;

    

    void Start(){
        if(MenuInGame == null) MenuInGame =  GameObject.FindWithTag("MenuInGame");
        if(MenuPause == null) MenuPause =  GameObject.FindWithTag("MenuPause");
        if(MenuPause != null) MenuPause.gameObject.SetActive(false);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && !endGame) PauseGame(true);

        if(pauseMenuActive != showingPauseMenu) efetivarMenuPause();
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
        MenuInGame.SetActive(show); 
    }

    public static void WinGame()
    {
        // PauseGame(false);
    }

    // Chamado em VidaPlayer / Morte()
    public static void GameOver()
    {
        PauseGame(false);
        endGame = true;
    }


    public void efetivarMenuPause(){
        showingPauseMenu = pauseMenuActive;
        
        MenuPause.SetActive(pauseMenuActive);
        
        ShowMenuInGame(!pauseMenuActive);
    }

    public void AlterarCena(string nome = "MenuPrincipal")
    {
        SceneManager.LoadScene(nome);
    }

}
