using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlechaPlayer : MonoBehaviour
{
    //[SerializeField] 
    public int dano = 1;
    [SerializeField] private bool autoDestruirAposDano = true;
    [SerializeField] private bool autoDestruirComImpacto = true;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "Enemy")
        {
            collision.transform.GetComponent<Enemy>().Dano(dano);

            if(autoDestruirAposDano)
            {
                // se ouver animação, dar play
                Destroy(gameObject);
            }
        }
        else
        if(autoDestruirComImpacto && collision.transform.tag != "Player")
            Destroy(gameObject, 0.1f);
    }
}
