using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class empresario_pre : redimido
{
    [SerializeField] public Transform trator;

    public override void Action(){
        anim.SetBool("andando", true);

        if(transform.position.x < trator.position.x && controle.facingDir == false) controle.flip();
        if(transform.position.x > trator.position.x && controle.facingDir == true ) controle.flip();

        movimentoHorizontal = velocidadeNormal * (controle.facingDir ? 1 : -1);

        if(Mathf.Abs(transform.position.x - trator.position.x) <= 2.0)
        {
            trator.transform.GetComponent<InimigoDash>().enabled = true;
            Destroy(gameObject);
        }
    }
}
