using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Movimentação")]
    [SerializeField] protected float velocidadeNormal = 4f;
    [SerializeField] protected float velocidadeCorrendo = 5f;
    [SerializeField] protected float raioDePercepcao = 16f;
    [SerializeField] protected Transform jogador;
    [SerializeField] protected float tempoAndando = 2.5f;
    [SerializeField] protected float tempoTransicao = .5f;    
    [SerializeField] protected bool anda = true;
    protected float distJogador;
    protected float movimentoHorizontal = 0f;
    protected bool pulando = false;

    [Header("Ação")]
    [SerializeField] protected float raioDeAcao = 8f;
    [SerializeField] protected float margemDoRaio = 2f;
    [SerializeField] protected float tempoEntreAcao = 1f;
    protected float tempoDecorrido = 0f;
    protected float tempoDaUltimaAcao = 0f;
    protected int estado = 0;

    [Header("Vida")]
    [SerializeField] public int vidaAtual = 10;
    [SerializeField] protected float tempoCorDano = 1f;
    [SerializeField] protected Color corDeDano = new Color(1f, 1f, 1f);
    protected Color originalColor = new Color(1f, 1f, 1f);
    protected SpriteRenderer sprite;
    protected int qtdDeEventosDeDano = 0;
    public bool redimido = false;
    [SerializeField] protected Sprite spriteRedimido;


    [Header("Drops")]
    [SerializeField] protected int qtdDeDrops = 0;
    [SerializeField] protected GameObject drop;


    protected Rigidbody2D Rig;
    protected Controle2D controle;
    
    // encontrar o player sem instanciar -> GameObject.Find("Player")

    private void Awake()
    {
        controle = GetComponent<Controle2D>();
        Rig = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        originalColor = sprite.color;
        if(jogador == null) jogador = GameObject.FindWithTag("Player").transform;
        if(spriteRedimido == null) spriteRedimido = sprite.sprite;
    }

    
    void Update()
    {
        Vector2 distJogadorV = jogador.transform.position - transform.position;
        distJogador = distJogadorV.magnitude;
        float maxDist = Mathf.Max(Mathf.Abs(distJogadorV.x), Mathf.Abs(distJogadorV.y));
        if(redimido && estado < 3) estado = 4;

        if(estado == 0 && maxDist <= raioDePercepcao) estado = 1;
        else
        if(estado == 1 && maxDist <= raioDeAcao - margemDoRaio){ estado = 2; tempoDecorrido = 0f; }
        else 
        if(estado == 2 && maxDist > raioDeAcao){ estado = 1; ResetAction(); }
        else
        if(estado == 1 && maxDist > raioDePercepcao){  estado = 3; tempoDecorrido = 0; }
        else
        if(estado == 3 && tempoDecorrido >= tempoTransicao){ estado = 0; tempoDecorrido = 0; controle.flip(); }
        else
        if(estado == 0 && tempoDecorrido >= tempoAndando){ estado = 3; tempoDecorrido = 0; }
        else
        if(estado == 4 && tempoDecorrido >= tempoAndando){ estado = 5; tempoDecorrido = 0; }
        

        if(estado == 0 || estado == 3 || estado == 2 || estado == 4) tempoDecorrido += Time.deltaTime;
        else tempoDecorrido = 0;
        tempoDaUltimaAcao += Time.deltaTime;
        // if ( Input.GetKeyDown(KeyCode.Space)) Action();
    }

    void FixedUpdate()
    {
        if(estado%4 == 0 && anda) movimentoHorizontal = velocidadeNormal * (controle.facingDir ? 1 : -1);
        else
        if(estado == 1 && anda) movimentoHorizontal = velocidadeCorrendo * (Mathf.Abs(jogador.transform.position.x - transform.position.x) <= 0.1 ? 0 : (jogador.transform.position.x > transform.position.x ? 1 : -1));
        else 
        if(estado == 2 && tempoDaUltimaAcao >= tempoEntreAcao) Action();
        else
        if(estado == 5) RedemptionAction();


        controle.aplicarMovimento(movimentoHorizontal, pulando);
        movimentoHorizontal = 0;
        pulando = false;
    }

    public virtual void Action(){}
    public virtual void ResetAction(){}
    public virtual void RedemptionAction(){}

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, new Vector3(2*raioDePercepcao, 2*raioDePercepcao, 1));
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(2*raioDeAcao, 2*raioDeAcao, 1));
        Gizmos.color = new Color(0.8f, 0.3f, 0f);
        Gizmos.DrawWireCube(transform.position, new Vector3(2*(raioDeAcao - margemDoRaio), 2*(raioDeAcao - margemDoRaio), 1));
    }

    public void Dano(int dano=1)
    {
        vidaAtual -= dano;

        StartCoroutine("TrocarCor");        

        if(vidaAtual <= 0) Morte();
    }

    private void Morte(){
        if(redimido) return;
        //animação de morte

        if(qtdDeDrops > 0 && drop != null)
        {
            for(int i=0; i<qtdDeDrops; i++)
            {
                GameObject obj = Instantiate(drop);
                obj.transform.position = transform.position;

                Vector2 dir = (jogador.transform.position - transform.position);
                dir.x += Random.Range(-2, 2);
                dir.y += Random.Range(-2, 2);
                
                Rigidbody2D rg = obj.GetComponent<Rigidbody2D>();
                if(rg != null) rg.AddForce(dir, ForceMode2D.Impulse);
            }
        }

        //GetComponent<Animator>().Play("redencao");
        
        redimido = true;

        //Destroy(gameObject);
    }

    IEnumerator TrocarCor(){
        sprite.color = corDeDano;
        qtdDeEventosDeDano++;

        yield return new WaitForSeconds(tempoCorDano);

        qtdDeEventosDeDano--;

        if(qtdDeEventosDeDano == 0) sprite.color = originalColor;
    }


}


/*
    LEGENDA DE ESTADOS

    0 - Patrulha [O inimigo anda de um lado para o outro, se andar, a espera do player]
    1 - Perseguição [O Player está na área de percebepção do inimigo. O inimigo corre em direção ao player]
    2 - Ação [O Player está na área de ação. O inimigo executa sua ação em particular]
    3 - Transição [O inimigo fica parado por x segundos]

    4 - Patrulha (Redimido)
    5 - Ação (Redimido)

    MUDANÇA DE ESTADOS

    0 -> 1: Se o player entrar no range 1
    1 -> 2: Se o player ultrapassar o range 2 menos a margem de raio
    2 -> 1: Se o player sair do range 2
    1 -> 3: Se o player sair do range 1
    3 -> 0: X segundos após entrar no estado 3
    0 -> 3: Após Y segundos andando
*/