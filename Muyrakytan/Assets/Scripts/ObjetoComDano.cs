using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjetoComDano : MonoBehaviour
{   
    [SerializeField] private int danoCausado = 1;
    [SerializeField] private bool autoDestruirAposDano = false;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag != "Player") return;

        collision.transform.GetComponent<VidaPlayer>().Dano(danoCausado);

        if(autoDestruirAposDano)
        {
            // se ouver animação, dar play
            Destroy(gameObject);
        }
    }
}
