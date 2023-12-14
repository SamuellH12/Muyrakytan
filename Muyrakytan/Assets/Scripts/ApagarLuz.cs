using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;


public class ApagarLuz : MonoBehaviour
{
    bool ligado = true;
    private float tempoDeTransicao;
    private int quantidadeDePassos;
    private Light2D luz;
    private float intensidadeOriginal;


    void Start()
    {
        tempoDeTransicao = CicloDaNoite.tempoDeTransicaoDoCicloDiaNoite; 
        quantidadeDePassos = CicloDaNoite.quantidadeDePassosDoCicloDiaNoite;

        luz = GetComponent<Light2D>();

        intensidadeOriginal = luz.intensity;
        ligado = luz.enabled = CicloDaNoite.noite;
    }


    void FixedUpdate(){
        if(CicloDaNoite.noite != ligado){
            ligado = !ligado;
            StartCoroutine("Transicao");
        }
    }

    
    IEnumerator Transicao()
    {
        luz.intensity = (ligado ? 0f : intensidadeOriginal);
        luz.enabled = true;
     
        float tempoDormindo = tempoDeTransicao / (float)(quantidadeDePassos);
        float diff = intensidadeOriginal / (float)(quantidadeDePassos) * (ligado ? 1f : -1f);
        
        for(int p=0; p<quantidadeDePassos; p++)
        {
            luz.intensity += diff;
            yield return new WaitForSeconds(tempoDormindo);
        }

        luz.intensity = (ligado ? intensidadeOriginal : 0f);
        luz.enabled = ligado;
    }

}
