using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VidaPlayer : MonoBehaviour
{
    [SerializeField] private int vidaMaxima = 10;
    [SerializeField] public int vidaAtual = 10;
    [SerializeField] private float tempoInvulneravel = 1f;

    [Header("Cor de Dano")]
    [SerializeField] private float tempoCorDano = 1f;
    [SerializeField] private Color corDeDano = new Color(200, 10, 10);
    private Color originalColor;
    private SpriteRenderer sprite;
    int qtdDeEventosDeDano = 0;

    private float tempoDecorrido = 0f;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        originalColor = sprite.color;
    }

    void Update() { 
        tempoDecorrido += Time.deltaTime;
    }


    public void Dano(int dano=1)
    {
        if(tempoDecorrido < tempoInvulneravel) return;
        tempoDecorrido = 0;

        vidaAtual -= dano;

        AtualizaUI();
        StartCoroutine("TrocarCor");

        if(vidaAtual <= 0) Morte();
    }

    private void Morte(){

        GetComponent<ControlePlayer>().controlavel = false;

        //animação de morte
        //tela de GameOver

        PauseControl.PauseGame(false);
    }

    private void AtualizaUI(){

        Debug.Log("Vidas: " + vidaAtual);

        // atualizar quantidade de corações na interface de usuário

    }

    IEnumerator TrocarCor(){
        sprite.color = corDeDano;
        qtdDeEventosDeDano++;

        yield return new WaitForSeconds(tempoCorDano);

        qtdDeEventosDeDano--;

        if(qtdDeEventosDeDano == 0) sprite.color = originalColor;
    }
}
