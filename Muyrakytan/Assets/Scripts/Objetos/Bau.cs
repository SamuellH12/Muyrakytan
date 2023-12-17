using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;


public class Bau : MonoBehaviour
{
    [SerializeField] private GameObject particulas;
    [SerializeField] private Sprite bauFechado;
    [SerializeField] private Sprite bauAberto;
    [SerializeField] private Transform muyrakytan;
    [SerializeField] public float tempoAntesDeChamarMenu = 2f;

    void Start(){
        if(bauFechado == null) bauFechado = GetComponent<SpriteRenderer>().sprite;
        else GetComponent<SpriteRenderer>().sprite = bauFechado;

        muyrakytan.transform.GetComponent<SpriteRenderer>().enabled = false;
        muyrakytan.transform.GetComponent<Light2D>().enabled = false;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag != "Player") return;
        if(collision.transform.GetComponent<VidaPlayer>().taComAChave == false) return;
        
        collision.transform.GetComponent<ControlePlayer>().DesabilitaControles();

        GetComponent<SpriteRenderer>().sprite = bauAberto;
        muyrakytan.transform.GetComponent<SpriteRenderer>().enabled = true;
        muyrakytan.transform.GetComponent<Light2D>().enabled = true;

        if(particulas != null){
            GameObject eff = Instantiate(particulas);
            eff.transform.position = transform.position;
            Destroy(eff, 5f);
        }
    
        StartCoroutine("ChamarWin");
    }

    IEnumerator ChamarWin(){
        yield return new WaitForSeconds(tempoAntesDeChamarMenu);
        UIControl.WinGame();
    }
}
