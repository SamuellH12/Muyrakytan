using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class redimido : MonoBehaviour
{
    [Header("Movimentação")]
    [SerializeField] protected float velocidadeNormal = 4f;
    [SerializeField] protected float raioDePercepcao = 16f;
    [SerializeField] protected Transform jogador;
    [SerializeField] protected float tempoAndando = 2.5f;
    [SerializeField] protected float tempoTransicao = .5f;    
    [SerializeField] protected bool anda = true;
    protected float distJogador;
    protected float movimentoHorizontal = 0f;

    [Header("Ação")]
    [SerializeField] protected float raioDeAcao = 8f;
    [SerializeField] protected float tempoEntreAcao = 1f;
    protected float tempoDecorrido = 0f;
    protected float tempoDaUltimaAcao = 0f;
    protected int estado = 0;

    protected Rigidbody2D Rig;
    protected Controle2D controle;
    protected SpriteRenderer sprite;
    protected Animator anim;
    
    // encontrar o player sem instanciar -> GameObject.Find("Player")

    private void Awake()
    {
        controle = GetComponent<Controle2D>();
        Rig = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        if(jogador == null) jogador = GameObject.FindWithTag("Player").transform;
    }

    
    void Update()
    {
        Vector2 distJogadorV = jogador.transform.position - transform.position;
        distJogador = distJogadorV.magnitude;
        float maxDist = Mathf.Max(Mathf.Abs(distJogadorV.x), Mathf.Abs(distJogadorV.y));

        if(estado == 0 && maxDist <= raioDeAcao && tempoDecorrido >= tempoEntreAcao){ estado = 1; tempoDecorrido = 0; }
        else
        if(estado == 2 && tempoDaUltimaAcao >= tempoTransicao){ estado = 0; tempoDecorrido = 0; controle.flip(); }
        
        if(estado == 2 || estado == 0) tempoDecorrido += Time.deltaTime;
        tempoDaUltimaAcao += Time.deltaTime;
    }

    void FixedUpdate()
    {
        if(estado == 0 && anda) movimentoHorizontal = velocidadeNormal * (controle.facingDir ? 1 : -1);
        else 
        if(estado == 1 && tempoDaUltimaAcao >= tempoEntreAcao) Action();
        
        if(estado == 0 ) anim.SetBool("andando", true);
        else anim.SetBool("andando", false);

        controle.aplicarMovimento(movimentoHorizontal, false);
        movimentoHorizontal = 0;
    }

    public virtual void Action(){
        anim.Play("acao");
        tempoDaUltimaAcao = 0;
        estado = 2;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, new Vector3(2*raioDePercepcao, 2*raioDePercepcao, 1));
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(2*raioDeAcao, 2*raioDeAcao, 1));
    }

}


/*
    LEGENDA DE ESTADOS

    0 - Patrulha [O inimigo anda de um lado para o outro, se andar, a espera do player]
    1 - Ação [O Player está na área de ação. O inimigo executa sua ação em particular]
    2 - Transição [O inimigo fica parado por x segundos]

    MUDANÇA DE ESTADOS

    [ 
    0 -> 1: Se o player entrar no range, executa a ação
    1 -> 2: Após a ação
    ] == 0 -> 2: a ação é uma animação
    2 -> 0: X segundos após entrar no estado 3
    0 -> 2: Após Y segundos andando
*/