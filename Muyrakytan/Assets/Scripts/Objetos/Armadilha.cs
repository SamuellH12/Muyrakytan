using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armadilha : MonoBehaviour
{
    [SerializeField] private int danoCausado = 1;
    [SerializeField] private float tempoFechado = 1.5f;
    [SerializeField] private float cooldown = 2f;
    [SerializeField] public bool autoDestruir = false;
    private float tempoDecorrido;
    private bool fechado = false;
    private ControlePlayer player;
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        tempoDecorrido = cooldown;
    }

    void Update()
    {
        if(!fechado) tempoDecorrido += Time.deltaTime;    
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(fechado || collision.transform.tag != "Player") return;
        if(tempoDecorrido < cooldown) return;

        if(autoDestruir){ GetComponent<AutoDestruir>().autoDestruir = false; }

        tempoDecorrido = 0;
        collision.transform.GetComponent<VidaPlayer>().Dano(danoCausado);
        player = collision.transform.GetComponent<ControlePlayer>();

        StartCoroutine("Prende");
    }

    IEnumerator Prende(){
        player.controlavel = false;
        fechado = true;
        anim.SetBool("closed", true);
        

        yield return new WaitForSeconds(tempoFechado);

        player.controlavel = true; 
        fechado = false;
        anim.SetBool("closed", false);

        if(autoDestruir) Destroy(gameObject);
    }
}
