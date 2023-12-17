using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AlterarCenas : MonoBehaviour
{
    public void AlterarCena(string nome = "MenuPrincipal")
    {
        SceneManager.LoadScene(nome);
    }
}
