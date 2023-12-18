using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjetoComDano : MonoBehaviour
{   
    [SerializeField] private int danoCausado = 1;
    [SerializeField] private bool autoDestruirAposDano = false;
    [SerializeField] private bool danoConstante = false;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(danoConstante) return;
        if(collision.transform.tag != "Player") return;

        collision.transform.GetComponent<VidaPlayer>().Dano(danoCausado);

        if(autoDestruirAposDano) Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(danoConstante) return;
        if(autoDestruirAposDano && collision.transform.tag == "flecha" ) Destroy(gameObject);
        if(collision.transform.tag != "Player") return;

        collision.transform.GetComponent<VidaPlayer>().Dano(danoCausado);

        if(autoDestruirAposDano) Destroy(gameObject);
    }


    void OnTriggerStay2D(Collider2D collision)
    {
        if(!danoConstante) return;
        if(collision.transform.tag != "Player") return;
        
        collision.transform.GetComponent<VidaPlayer>().Dano(danoCausado);
    }
}
