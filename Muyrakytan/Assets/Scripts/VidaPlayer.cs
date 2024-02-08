using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VidaPlayer : MonoBehaviour
{
    [Header("Vida")]
    [SerializeField] public int vidaMaxima = 10;
    [SerializeField] public int vidaAtual = 10;
    [SerializeField] private float tempoInvulneravel = 1f;
    [SerializeField] private StatsUI barraDeStats;
    [SerializeField] public Transform checkPoint;

    [Header("Energia")]
    [SerializeField] public int energiaMaxima = 100;
    [SerializeField] public int energiaAtual = 50;

    [Header("Chave")]
    [SerializeField] public bool taComAChave = false;

    [Header("Cores de interação")]
    [SerializeField] private float tempoCorDano = 1f;
    [SerializeField] private Color corDeDano = new Color(200, 10, 10);
    // [SerializeField] private float tempoCorEnergia = 1f;
    // [SerializeField] private Color corDaEnergia = new Color(0, 0, 1);
    private Color originalColor;
    private Color nwColor;
    private float tempoColorido = 0f;
    private SpriteRenderer sprite;
    int qtdDeEventosDeCor = 0;

    private float tempoDecorrido = 0f;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        originalColor = sprite.color;
        if(barraDeStats == null) barraDeStats = GameObject.FindWithTag("BarraDeStats").GetComponent<StatsUI>();
        barraDeStats.SetMaxValues(vidaMaxima, energiaMaxima);
        AtualizaUI();
        //cria um "checkpoint" temporário no início do nível
        GameObject temp = new GameObject("InicioDoNivel");
        temp.transform.position = transform.position;
        checkPoint = temp.transform;
    }

    void Update(){ tempoDecorrido += Time.deltaTime; }


    public void Dano(int dano=1)
    {
        if(tempoDecorrido < tempoInvulneravel) return;
        tempoDecorrido = 0;

        vidaAtual -= dano;

        AtualizaUI();
        nwColor = corDeDano;
        tempoColorido = tempoCorDano;
        StartCoroutine("TrocarCor");

        if(vidaAtual <= 0) Morte();
    }

    public void RecuperarVida(int valor)
    {
        vidaAtual += valor;
        AtualizaUI();
    }

    public void somaEnergia(int valor)
    {
        energiaAtual += valor;

        energiaAtual = Mathf.Max(energiaAtual, 0);
        energiaAtual = Mathf.Min(energiaAtual, energiaMaxima);

        AtualizaUI();

        // if(valor > 0){
        //     nwColor = corDaEnergia;
        //     tempoColorido = tempoCorEnergia;
        //     StartCoroutine("TrocarCor");
        // }

        if(energiaAtual == energiaMaxima)
        {
            //chama o evento da noite
            if(CicloDaNoite.noite == false) 
                GameObject.FindWithTag("GlobalConfig").GetComponent<CicloDaNoite>().Anoitecer();
        }
    }


    private void Morte()
    {  
        GetComponent<ControlePlayer>().controlavel = false;
        UIControl.GameOver();
    }

    public void ResetarPlayer(){
        GetComponent<ControlePlayer>().controlavel = true;
        transform.position = checkPoint.transform.position;
        vidaAtual = vidaMaxima / 2;
        if(energiaAtual < energiaMaxima / 2) energiaAtual = energiaMaxima / 2;
        AtualizaUI();
    }

    private void AtualizaUI()
    {
        // Debug.Log("Vidas: " + vidaAtual);
        // Debug.Log("Energia: " + energiaAtual);
        // atualizar quantidade de corações na interface de usuário
        barraDeStats.AtualizarVidas(vidaAtual);
        barraDeStats.AtualizarEnergia(energiaAtual);
    }

    IEnumerator TrocarCor()
    {
        sprite.color = nwColor;
        qtdDeEventosDeCor++;

        yield return new WaitForSeconds(tempoColorido);

        qtdDeEventosDeCor--;

        if(qtdDeEventosDeCor == 0) sprite.color = originalColor;
    }
}
