using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fogoChamine : MonoBehaviour
{
    [SerializeField] public int danoCausado = 1;
    [SerializeField] public float tempoEntreFogo = 2f;
    [SerializeField] public bool randomStart = false;
    private Animator anim;
    private float tempoDecorrido = 0;
    private bool ativo = false;

    void Start()
    {
        anim = GetComponent<Animator>();
        if(randomStart) tempoDecorrido = Random.Range(0, tempoEntreFogo);
    }

    void Update()
    {
        if(tempoDecorrido >= tempoEntreFogo && !ativo) Ativar();
        if(!ativo) tempoDecorrido += Time.deltaTime;
    }

    private void Ativar(){
        ativo = true;
        anim.Play("fogoChamine");
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(!ativo || collision.transform.tag != "Player") return;

        collision.transform.GetComponent<VidaPlayer>().Dano(danoCausado);
    }

    public void Desativar(){
        ativo = false;
        tempoDecorrido = 0;
    }
}
