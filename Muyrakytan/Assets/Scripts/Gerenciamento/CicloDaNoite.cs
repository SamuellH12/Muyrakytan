using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CicloDaNoite : MonoBehaviour
{

    // receber como parâmetro os grupos dos ceus/estrelas
    // criar um script separado para elementos que trocam de cor, baseado no static Noite

    public static bool Noite = false;
    [SerializeField] public Transform ceuNoturno;
    [SerializeField] public Transform ceuDiurno;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            if(Noite) amanhecer();
            else anoitecer();
        }
    }

    private void anoitecer(int faseDaLua = 0)
    {
        Noite = true;

        ceuDiurno.GetComponent<SpriteRenderer>().enabled = false;
        ceuNoturno.GetComponent<SpriteRenderer>().enabled = true;
    }

    private void amanhecer()
    {
        Noite = false;

        ceuDiurno.GetComponent<SpriteRenderer>().enabled = true;
        ceuNoturno.GetComponent<SpriteRenderer>().enabled = false;
    }

}
