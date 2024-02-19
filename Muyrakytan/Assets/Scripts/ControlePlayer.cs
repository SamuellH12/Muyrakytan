using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Controle2D))]
public class ControlePlayer : MonoBehaviour
{
    [Header("Movimentação")]
    [SerializeField] public float velocidade = 50f;
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
    [SerializeField] private float cooldown = 0.5f;
    private float tempoDaFlecha = 0;
    public bool podeAtirarFlechas = true;

    private Animator anim;
    private Rigidbody2D Rig;
    private Controle2D controle;
    private VidaPlayer vidaEnergia;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        Rig = GetComponent<Rigidbody2D>();
        controle = GetComponent<Controle2D>();
        vidaEnergia = GetComponent<VidaPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(controlavel) movimentoHorizontal = Input.GetAxisRaw("Horizontal") * velocidade;
        else movimentoHorizontal = 0;
       
        if(controlavel && (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetButtonDown("Jump"))) pulando = true;

        tempoDaFlecha += Time.deltaTime;
        bool atirarBotao = Input.GetButtonDown("Fire1") || Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.RightShift) || Input.GetKeyDown(KeyCode.LeftShift);
        if(atirarBotao && tempoDaFlecha >= cooldown && vidaEnergia.energiaAtual > 0 && !UIControl.paused) AtirarFlecha();
    }

    void FixedUpdate()
    {
        controle.aplicarMovimento(movimentoHorizontal, pulando);
        pulando = false;

        anim.SetBool("noChao", controle.noChao);
        anim.SetBool("andando", movimentoHorizontal != 0);
        anim.SetBool("noite", CicloDaNoite.noite);
    }


    private void AtirarFlecha()
    {
        tempoDaFlecha = 0;
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

        anim.Play((CicloDaNoite.noite ? "naruna_ataque_noite" : "naruna_ataque_dia"));
    }


    public void DesabilitaControles(){
        controlavel = false;
        podeAtirarFlechas = false;
    }

    public void HabilitaControles(){
        controlavel = true;
        podeAtirarFlechas = true;
    }

    public void Pular(){ pulando = true; }
}

