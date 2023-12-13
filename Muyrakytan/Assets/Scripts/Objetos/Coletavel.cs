using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coletavel : MonoBehaviour
{
    [SerializeField] private int vidaRecuperada = 0;
    [SerializeField] private int energiaRecuperada = 0;
    
    void Aplicar(VidaPlayer vp)
    {
        vp.RecuperarVida(vidaRecuperada);
        vp.somaEnergia(energiaRecuperada);

        Destroy(gameObject);        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "Player") 
            Aplicar(collision.transform.GetComponent<VidaPlayer>());  
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "flecha")
            Aplicar(GameObject.FindWithTag("Player").transform.GetComponent<VidaPlayer>());
    }
}
