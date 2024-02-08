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
    [SerializeField] public GameObject MenuOver = null;
    [SerializeField] public GameObject MenuWin = null;
    [SerializeField] public Transform CheckPoint = null;

    

    void Start(){
        if(MenuInGame == null) MenuInGame =  GameObject.FindWithTag("MenuInGame");
        if(MenuPause == null) MenuPause =  GameObject.FindWithTag("MenuPause");
        if(MenuOver == null) MenuOver =  GameObject.FindWithTag("MenuGameOver");
        if(MenuWin == null) MenuWin =  GameObject.FindWithTag("MenuWin");
        if(MenuPause != null) MenuPause.gameObject.SetActive(false);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && !endGame) PauseGame(true);

        if(pauseMenuActive != showingPauseMenu) efetivarMenuPause();
    }

    public static void PauseGame(bool showMenu = true)
    {
        if(endGame) return;
        paused = !paused;

        Time.timeScale = paused ? 0f : 1f;

        if(paused && showMenu) pauseMenuActive = true;
        else
        if(!paused && pauseMenuActive) pauseMenuActive = false;
    }

    //chamado pelo Baú
    public static void WinGame()
    {
        endGame = true;
        paused = true;
        Time.timeScale = 0f;

        GameObject.FindWithTag("GlobalConfig").GetComponent<UIControl>().ShowMenuWin(true);
    }

    // Chamado em VidaPlayer -> Morte()
    public static void GameOver()
    {
        endGame = true;
        paused = true;
        Time.timeScale = 0f;

        GameObject.FindWithTag("GlobalConfig").GetComponent<UIControl>().ShowMenuOver(true);
    }


    public void ShowMenuInGame(bool show = true){ MenuInGame.SetActive(show); }
    public void ShowMenuOver(bool show = true){ MenuOver.SetActive(show); }
    public void ShowMenuWin(bool show = true){ MenuWin.SetActive(show); }

    public void RenascerPlayerNoCheckPoint(){
        GameObject.FindWithTag("Player").transform.GetComponent<VidaPlayer>().ResetarPlayer();
        
        endGame = false;
        paused = pauseMenuActive = showingPauseMenu = false;
        Time.timeScale = 1f;

        ShowMenuOver(false);
    }

    public void efetivarMenuPause()
    {
        showingPauseMenu = pauseMenuActive;
        
        MenuPause.SetActive(pauseMenuActive);
        
        ShowMenuInGame(!pauseMenuActive);
    }

    public void AlterarCena(string nome = "MenuPrincipal")
    { 
        UIControl.ResetConfig();
        SceneManager.LoadScene(nome); 
    }

    public void RecarregarScene(){ AlterarCena(SceneManager.GetActiveScene().name); }

    public static void ResetConfig()
    {
        CicloDaNoite.noite = false;
        Time.timeScale = 1f;

        paused = pauseMenuActive = showingPauseMenu = false;
        endGame = false;
    }

}
