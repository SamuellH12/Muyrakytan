using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsUI : MonoBehaviour
{
    [Header("Vida")]
    [SerializeField] public Transform barraDeVida;
    [SerializeField] public Sprite vidaCompleta;
    [SerializeField] public Sprite vidaVazia;
    [SerializeField] public GameObject vida;
    [SerializeField] public float distanciaEntreVidas = 10f;
    private float tamanhoVida = 20f;
    private int vidaMaxima;
    private List<GameObject> listaVidas = new List<GameObject>();

    [Header("Energia")]
    [SerializeField] public Transform barraDeEnergia;
    private int energiaMaxima;
    private Slider fill;

    [Header("Chave")]
    [SerializeField] public Image chave;
    static int qtdChaves = 0;

    [Header("Maracuja")]
    [SerializeField] public Image maracuja;
    static int maracujasAtivos = 0;


    void Start(){   
        //tamanhoVida = ;
        fill = barraDeEnergia.GetComponent<Slider>();
        chave.enabled = false;
        maracuja.enabled = false;
    }

    public void AtualizarVidas(int vidaAtual)
    {
        ResetarVidas();        
        for(int i=0; i<vidaAtual; i++)
        {
            GameObject vd = Instantiate(vida, new Vector3(barraDeVida.position.x + tamanhoVida*i + distanciaEntreVidas*(i+1), barraDeVida.transform.position.y + tamanhoVida/2f, 0), Quaternion.identity, barraDeVida.transform);
            listaVidas.Add(vd);
        }
    }

    public void AtualizarEnergia(int energiaAtual)
    {
        if(fill != null) fill.value = (float)energiaAtual / (float)energiaMaxima;
    }

    private void ResetarVidas(){
        foreach(GameObject vd in listaVidas) Destroy(vd);
        listaVidas.Clear();
    }

    public void SetMaxValues(int _vidaMaxima, int _energiaMaxima)
    {
        vidaMaxima = _vidaMaxima;
        energiaMaxima = _energiaMaxima;
    }

    public void MostrarChave(bool value = true){
        chave.enabled = value;  
        qtdChaves = (value ? 1 : 0);
    }

    public void ativarMaracuja(){
        maracuja.enabled = true;
        maracujasAtivos++;
    }

    public void desativarMaracuja(){
        maracujasAtivos--;
        if(maracujasAtivos == 0) maracuja.enabled = false;
    }
}
