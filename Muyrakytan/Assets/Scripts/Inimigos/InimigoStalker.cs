using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InimigoStalker : Enemy
{
    [Header("Ataque")]
    [SerializeField] private int dano = 2;
    [SerializeField] private GameObject particulas;


    public override void Action()
    {    
        tempoDecorrido = tempoDaUltimaAcao = 0;

        GameObject obj = Instantiate(particulas);
        obj.transform.position = transform.position;
        Destroy(obj, 5f);

        jogador.GetComponent<VidaPlayer>().Dano(dano);
    }   
}
