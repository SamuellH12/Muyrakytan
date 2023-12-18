using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InimigoDash : Enemy
{
    [Header("Ataque")]
    [SerializeField] private float velocidadeDash = 200f;
    [SerializeField] private float delay = 2f;
    // [SerializeField] private float dashTime = 2f;
    [SerializeField] private float delayAfter = 3f;
    [SerializeField] private GameObject particulas;
    [SerializeField] private GameObject particulas2;
    
    private bool inAction = false;

    public override void Action(){

        if(!inAction)
        {
            if(jogador.transform.position.x - transform.position.x > 0 && !controle.facingDir) controle.flip();
            if(jogador.transform.position.x - transform.position.x < 0 &&  controle.facingDir) controle.flip();

            if(particulas != null)
            {
                GameObject eff = Instantiate(particulas);
                eff.transform.position = transform.position;
                Destroy(eff.gameObject, 5f);
            }

            inAction = true;
            StartCoroutine("Dash");
            tempoDecorrido = tempoDaUltimaAcao = 0;
        }
    }

    public override void ResetAction()
    {
        tempoDecorrido = tempoDaUltimaAcao = 0;
        inAction = false;
        anda = true;
    }

    IEnumerator Dash(){
        inAction = true;
        anda = false;

        yield return new WaitForSeconds(delay);

        if(particulas2 != null)
        {
            GameObject eff = Instantiate(particulas2);
            eff.transform.position = transform.position;
            Destroy(eff.gameObject, 3f);
        }

        if(inAction) Rig.velocity = new Vector2(velocidadeDash * (controle.facingDir ? 1 : -1), 0f);

        // yield return new WaitForSeconds(dashTime);
        // Rig.velocity = Vector2.zero; 
        yield return new WaitForSeconds(delayAfter);

        if(inAction) ResetAction();
    }

}
