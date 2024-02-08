using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkPoint : MonoBehaviour
{
    [SerializeField] private GameObject particulas;
    [SerializeField] private Sprite desativado;
    [SerializeField] private Sprite ativado;

    void Start(){
        if(desativado == null) desativado = GetComponent<SpriteRenderer>().sprite;
        else GetComponent<SpriteRenderer>().sprite = desativado;
        
        if(ativado == null) ativado = GetComponent<SpriteRenderer>().sprite;
    }

    public void chamarParticulas()
    {
        if(particulas != null){
            GameObject eff = Instantiate(particulas);
            eff.transform.position = transform.position;
            Destroy(eff, 5f);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag != "Player") return;
        if(collision.transform.GetComponent<VidaPlayer>().checkPoint == transform) return;
        
        collision.transform.GetComponent<VidaPlayer>().checkPoint = transform;

        GetComponent<SpriteRenderer>().sprite = ativado;

        chamarParticulas();
    }

}
