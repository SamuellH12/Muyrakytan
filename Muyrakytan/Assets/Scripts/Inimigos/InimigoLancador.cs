using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InimigoLancador : Enemy
{
    [Header("Ataque")]
    [SerializeField] private Transform OrigemDoAtaque;
    [SerializeField] private GameObject ObjetoJogado;
    [SerializeField] private float velocidadeDoLancamento = 10f;
    [SerializeField] private float tempoProItemDesaparecer = 4f;



    public override void Action()
    {
        pulando = true;
        tempoDecorrido = 0;
        tempoDaUltimaAcao = 0;

        anim.Play("arremessar");

        GameObject obj = Instantiate(ObjetoJogado);
        obj.transform.position = OrigemDoAtaque.position;
        Rigidbody2D rbObj = obj.GetComponent<Rigidbody2D>();

        //Calcula o ângulo e a força necessários para acertar o jogador
        float t = distJogador / velocidadeDoLancamento, g = 9.81f * rbObj.gravityScale;
        Vector2 dir = (jogador.transform.position - OrigemDoAtaque.position) / t;
        dir.y += (g*t) / 2 + .6f / t;

        rbObj.AddForce(dir, ForceMode2D.Impulse);
        
        Destroy(obj.gameObject, tempoProItemDesaparecer);

        if(jogador.transform.position.x - transform.position.x > 0 && !controle.facingDir) controle.flip();
        if(jogador.transform.position.x - transform.position.x < 0 &&  controle.facingDir) controle.flip();
    }
}
