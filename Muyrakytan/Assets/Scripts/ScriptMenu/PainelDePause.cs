using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class NewBehaviourScript : MonoBehaviour
{
    public bool isPaused;
    [Header("Paineis e Menu")]
    public GameObject pausePainel;
    public string cenaPause;

    void Start(){
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
    } 
    void Update()
    {
      if(!isPaused){
           
      }
      if(Input.GetKeyDown(KeyCode.Escape)) {
            PauseJogo();
      } 
    }

    void TogglePause()
    {
        if (isPaused)
        {
            ResumeGame();
        }
        else
        {
            PauseJogo();
        }
    }

    public void PauseJogo(){
        if(isPaused){
            isPaused = false;
            Time.timeScale = 1f;
            pausePainel.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else{
            isPaused = true;
            Time.timeScale = 0f;
            pausePainel.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f;
        pausePainel.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void RetContinuar()
    {
        ResumeGame();
    }



     public void ExibiMenu(){
       SceneManager.LoadScene(cenaPause);
    }
}
