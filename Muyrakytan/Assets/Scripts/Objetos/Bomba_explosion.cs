using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomba_explosion : MonoBehaviour
{
    [SerializeField] private float raioDeExplosao = 5;
    [SerializeField] private float forcaDaExplosao = 5;
    [SerializeField] private int danoCausado = 3;
    [SerializeField] private float raioDeDeteccao = 7;
    [SerializeField] private LayerMask layerAfeta;
    [SerializeField] private GameObject efeito;
    private Animator anim;
    private Transform jogador;
    bool fire = false;

    void Start(){
        anim = GetComponent<Animator>();
        if(jogador == null) jogador = GameObject.FindWithTag("Player").transform;    
    }

    void Update()
    {
        float distJogador = (jogador.transform.position - transform.position).magnitude;

        if(distJogador <= raioDeDeteccao && !fire )
        {   
            anim.Play("Bomba");
        }
    }

    public void Explode(){

        Collider2D[] objExp = Physics2D.OverlapCircleAll(transform.position, raioDeExplosao, layerAfeta);

        foreach(Collider2D obj in objExp )
        {
            Vector2 dir = (obj.transform.position - transform.position);
            dir.Normalize();
            dir.y *= 1.25f;

            Rigidbody2D rig = obj.GetComponent<Rigidbody2D>();
            if(rig) rig.AddForce(dir*forcaDaExplosao, ForceMode2D.Impulse);

            if(obj.transform.tag == "Player") obj.transform.GetComponent<VidaPlayer>().Dano(danoCausado);
        }

        GameObject eff = Instantiate(efeito);
        eff.transform.position = transform.position;
        Destroy(eff.gameObject, 5f);
        Destroy(gameObject, 0.2f);
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "flecha") Explode();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "flecha") Explode();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, raioDeDeteccao);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, raioDeExplosao);
    }

}
