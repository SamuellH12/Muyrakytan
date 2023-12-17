using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlterarVelocidade : MonoBehaviour
{
    [SerializeField] private float novaVelocidade = 3f;
    [SerializeField] private float tempoComAVelocidade = 5f;
    [SerializeField] public bool autoDestruir = true;
    private bool inAction = false;
    private ControlePlayer ctrl;

    
    void Aplicar(ControlePlayer cont)
    {
        if(inAction) return;
        ctrl = cont;
        if(autoDestruir) GetComponent<SpriteRenderer>().enabled = false;
        StartCoroutine("Alterar");
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "Player") Aplicar(collision.transform.GetComponent<ControlePlayer>());  
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "Player") Aplicar(collision.transform.GetComponent<ControlePlayer>());
    }

    IEnumerator Alterar(){
        inAction = true;
        float oldvel = ctrl.velocidade;
        ctrl.velocidade = novaVelocidade;

        StatsUI ui = GameObject.FindWithTag("BarraDeStats").GetComponent<StatsUI>();
        ui.ativarMaracuja();


        yield return new WaitForSeconds(tempoComAVelocidade);

        ctrl.velocidade = oldvel;

        ui.desativarMaracuja();
        if(autoDestruir) Destroy(gameObject);
    }
}
