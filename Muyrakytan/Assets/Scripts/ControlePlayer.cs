using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Controle2D))]
public class ControlePlayer : MonoBehaviour
{
    [SerializeField] private float velocidade = 50f;
    
    private Rigidbody2D Rig;
    private Controle2D controle;

    private float movimentoHorizontal = 0f;
    private bool pulando = false;

    private void Awake()
    {
        controle = GetComponent<Controle2D>();
        Rig = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        movimentoHorizontal = Input.GetAxisRaw("Horizontal") * velocidade;
       
        if(Input.GetButtonDown("Jump")) pulando = true;
    }

    void FixedUpdate()
    {
        controle.aplicarMovimento(movimentoHorizontal, pulando);
        pulando = false;
    }
}
