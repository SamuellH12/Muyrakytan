using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FlechaPlayer : MonoBehaviour
{
    //[SerializeField] 
    public int dano = 1;
    [SerializeField] private bool autoDestruirAposDano = true;
    [SerializeField] private bool autoDestruirComImpacto = true;
    [SerializeField] public Sprite spriteDia;
    [SerializeField] public Sprite spriteNoite;
    [SerializeField] public bool luzDuranteDia = false;
    //[SerializeField] 
    private Transform pontoDeLuz;

    void Start(){
        GetComponent<SpriteRenderer>().sprite = (CicloDaNoite.noite ? spriteNoite : spriteDia);
        // luz de efeito
        pontoDeLuz = transform.GetChild(0);
        if(!luzDuranteDia) pontoDeLuz.GetComponent<Light2D>().enabled = CicloDaNoite.noite;
        //luz de iluminação  
        pontoDeLuz = transform.GetChild(1);
        if(!luzDuranteDia) pontoDeLuz.GetComponent<Light2D>().enabled = CicloDaNoite.noite;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "Enemy")
        {
            collision.transform.GetComponent<Enemy>().Dano(dano);

            if(autoDestruirAposDano)
            {
                // se ouver animação, dar play
                Destroy(gameObject);
            }
        }
        else
        if(autoDestruirComImpacto && collision.transform.tag != "Player" && collision.transform.tag != "Tecnico")
            Destroy(gameObject, 0.01f);
    }
}
