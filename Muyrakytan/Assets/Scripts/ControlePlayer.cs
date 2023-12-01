using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Controle2D))]
public class ControlePlayer : MonoBehaviour
{
    [Header("Movimentação")]
    [SerializeField] private float velocidade = 50f;
    public bool controlavel = true;
    private float movimentoHorizontal = 0f;
    private bool pulando = false;

    [Header("Arco e Flecha")]
    [SerializeField] private Transform Arco;
    [SerializeField] private GameObject Flecha;
    [SerializeField] private float forcaDaFlecha = 10f;
    [SerializeField] private int gastoDeEnergiaFlecha = 1;
    [SerializeField] private int danoDaFlecha = 1;
    [SerializeField] private int danoDaFlechaANoite = 3;

    private Rigidbody2D Rig;
    private Controle2D controle;
    private VidaPlayer vidaEnergia;

    private void Awake()
    {
        controle = GetComponent<Controle2D>();
        Rig = GetComponent<Rigidbody2D>();
        vidaEnergia = GetComponent<VidaPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(controlavel) movimentoHorizontal = Input.GetAxisRaw("Horizontal") * velocidade;
        else movimentoHorizontal = 0;
       
        if(controlavel && Input.GetButtonDown("Jump")) pulando = true;

        if((Input.GetButtonDown("Fire1") || Input.GetKeyDown(KeyCode.UpArrow)) && vidaEnergia.energiaAtual > 0 && !PauseControl.paused) AtirarFlecha();
    }

    void FixedUpdate()
    {
        controle.aplicarMovimento(movimentoHorizontal, pulando);
        pulando = false;
    }


    private void AtirarFlecha()
    {
        GameObject flecha = Instantiate(Flecha);
        flecha.transform.position = Arco.position;
        flecha.GetComponent<FlechaPlayer>().dano = (CicloDaNoite.noite ? danoDaFlechaANoite : danoDaFlecha);

        vidaEnergia.somaEnergia(-gastoDeEnergiaFlecha);

        float normal = 1;
        
        if(transform.localScale.x < 0)
        {
            normal = -1;
            Vector3 scale = flecha.transform.localScale;
            scale.x *= -1;
            flecha.transform.localScale = scale;
        }
        
        flecha.GetComponent<Rigidbody2D>().velocity = new Vector2(forcaDaFlecha * normal, 0);

        Destroy(flecha.gameObject, 5f);
    }
}
