using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipalManager : MonoBehaviour
{
   [SerializeField] private string nomeLevelJogo;
   [SerializeField] GameObject painelMenuInicial;
   [SerializeField] GameObject painelOpcoes;
   [SerializeField] GameObject painelOpcoesInstruct;

   void start(){
    Time.timeScale = 1f;
    Cursor.lockState = CursorLockMode.Locked;
    Cursor.visible = false;
   }

    public void Jogar()
    {

        SceneManager.LoadScene(nomeLevelJogo);
    }

    public void AbrirOpcoes()
    {
        painelMenuInicial.SetActive(false);
        painelOpcoes.SetActive(true);
    }

    public void AbrirOpcoesInstrucao()
    {
        painelMenuInicial.SetActive(false);
        painelOpcoesInstruct.SetActive(true);
    }


    public void FecharOpcoes()
    {
        painelOpcoes.SetActive(false);
        painelOpcoesInstruct.SetActive(false);
        painelMenuInicial.SetActive(true);
        
    }

    public void SairJogo()
    {
        Debug.Log("Sair do Jogo");
        Application.Quit();
        
    }
}
