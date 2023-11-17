using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InimigoLancador : MonoBehaviour
{
    [Header("Movimentação")]
    [SerializeField] private float velocidade = 50f;
    [SerializeField] private Transform jogador;
    [SerializeField] private float raioDePercepcao = 10f;
    [SerializeField] private bool andar = false;
    
    private float movimentoHorizontal = 0f;
    private bool pulando = false;
    private float distJogador;

    [Header("Ataque")]
    [SerializeField] private Transform Origem;
    [SerializeField] private GameObject ObjetoJogado;
    [SerializeField] private float velocidadeDoLancamento = 10f;
    [SerializeField] private float intervaloEntreAtaques = 1f;

    private float tempoDecorrido = 0f;

    private Rigidbody2D Rig;
    private Controle2D controle;
    private void Awake()
    {
        controle = GetComponent<Controle2D>();
        Rig = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        distJogador = (jogador.transform.position - transform.position).magnitude;
        
        tempoDecorrido += Time.deltaTime;
    }

    void FixedUpdate()
    {
        if(distJogador < raioDePercepcao && tempoDecorrido >= intervaloEntreAtaques) Atacar();
    }

    void Atacar()
    {
        tempoDecorrido = 0;

        GameObject obj = Instantiate(ObjetoJogado);
        obj.transform.position = Origem.position;
        Rigidbody2D rbObj = obj.GetComponent<Rigidbody2D>();

        //Calcula o ângulo e a força necessários para acertar o jogador
        float t = distJogador / velocidadeDoLancamento, g = 9.81f * rbObj.gravityScale;
        Vector2 dir = (jogador.transform.position - Origem.position) / t;
        dir.y += (g*t) / 2 + .6f / t;

        rbObj.AddForce(dir, ForceMode2D.Impulse);
        
        Destroy(obj.gameObject, 2f);

        if(jogador.transform.position.x - transform.position.x > 0 && !controle.facingDir) controle.flip();
        if(jogador.transform.position.x - transform.position.x < 0 &&  controle.facingDir) controle.flip();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, raioDePercepcao);
    }
}
