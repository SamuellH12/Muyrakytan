using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaDeDano : MonoBehaviour
{
    [SerializeField] private int danoCausado = 2;
    public Transform safeArea;
    //Area segura deve ser setado pelos colliders de SafeArea, children de AreaDano

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag != "Player") return;
        
        collision.transform.GetComponent<VidaPlayer>().Dano(danoCausado);

        collision.transform.position = safeArea.position;
    }
}
