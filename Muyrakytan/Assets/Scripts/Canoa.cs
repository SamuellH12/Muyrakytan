using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canoa : MonoBehaviour
{
    [SerializeField] public float velocidade = 6f;
    [SerializeField] public Transform centro;

    private Controle2D controle;
    private Transform player;
    private float movimentoHorizontal = 0f;
    private bool controlavel = false;
    private bool oldFacingDir;
    

    void Start()
    {
        controle = transform.parent.GetComponent<Controle2D>();
        oldFacingDir = controle.facingDir;
    }


    void Update()
    {    
        if(controlavel) movimentoHorizontal = Input.GetAxisRaw("Horizontal") * velocidade;
        else movimentoHorizontal = 0;

        if(controlavel && Input.GetButtonDown("Jump")) LiberarPlayer();
    }

    void FixedUpdate()
    {
        controle.aplicarMovimento(movimentoHorizontal, false);
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag != "Player") return;

        oldFacingDir = collision.transform.GetComponent<Controle2D>().facingDir;
        if(oldFacingDir != controle.facingDir) controle.flip();

        player = collision.transform;
        player.parent = transform.parent;

        player.transform.GetComponent<ControlePlayer>().controlavel = false;
        controlavel = true;
    }

    void LiberarPlayer()
    {
        player.parent = null;

        player.transform.GetComponent<Controle2D>().facingDir = controle.facingDir;
        player.transform.GetComponent<ControlePlayer>().controlavel = true;
        player.transform.GetComponent<ControlePlayer>().Pular();
        
        controlavel = false;
    }
}
