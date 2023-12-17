using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chave : MonoBehaviour
{
    [SerializeField] private GameObject particulas;
    [SerializeField] public bool deixarDeNoite = false;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag != "flecha" && collision.transform.tag != "Player") return;

        GameObject.FindWithTag("BarraDeStats").GetComponent<StatsUI>().MostrarChave();
        collision.transform.GetComponent<VidaPlayer>().taComAChave = true;

        if(deixarDeNoite) GameObject.FindWithTag("GlobalConfig").GetComponent<CicloDaNoite>().Anoitecer();

        if(particulas != null){
            GameObject eff = Instantiate(particulas);
            eff.transform.position = transform.position;
            Destroy(eff, 5f);
        }
        
        Destroy(gameObject);
    }
}
