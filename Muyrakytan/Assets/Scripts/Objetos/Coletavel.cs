using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coletavel : MonoBehaviour
{
    [SerializeField] private int vidaRecuperada = 0;
    [SerializeField] private int energiaRecuperada = 0;
    

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag != "Player") return;

        VidaPlayer vp = collision.transform.GetComponent<VidaPlayer>();
        
        vp.RecuperarVida(vidaRecuperada);
        vp.somaEnergia(energiaRecuperada);

        Destroy(gameObject);        
    }
}
