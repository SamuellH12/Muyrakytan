using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlechaPlayer : MonoBehaviour
{
    [SerializeField] private int danoCausado = 1;
    [SerializeField] private bool autoDestruirAposDano = true;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag != "Enemy") return;

        collision.transform.GetComponent<VidaNPC>().Dano(danoCausado);

        if(autoDestruirAposDano)
        {
            // se ouver animação, dar play
            Destroy(gameObject);
        }
    }
}
