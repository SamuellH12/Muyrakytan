using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CicloDaNoite : MonoBehaviour
{

    // receber como parâmetro os grupos dos ceus/estrelas
    // criar um script separado para elementos que trocam de cor, baseado no static Noite

    public static bool Noite = false;
    [SerializeField] public Transform grupoCeuNoturno;
    [SerializeField] public Transform grupoCeuDiurno;
    [SerializeField] public List<Transform> grupoMudaCor; 
    [SerializeField] public Color corDosObjetosDuranteANoite;
    [Header("Transição")]
    [SerializeField] public float tempoDeTransicao = 2f;
    [SerializeField] public int quantidadeDePassos = 100;

    void Update()
    {
        if ( Input.GetKeyDown(KeyCode.Tab))
        {
            if(Noite) Amanhecer();
            else Anoitecer();
        }
      //  Debug.Log("Working");
    }

    public void Anoitecer(int faseDaLua = 0)
    {
        Noite = true;
        Debug.Log("NoiteDia");

        // grupoCeuDiurno.GetComponent<SpriteRenderer>().enabled = false;
        // grupoCeuNoturno.GetComponent<SpriteRenderer>().enabled = true;

        StartCoroutine("makeTransparent");
    }

    public void Amanhecer()
    {
        Noite = false;
        Debug.Log("NoiteDia");

        // grupoCeuDiurno.GetComponent<SpriteRenderer>().enabled = true;
        // grupoCeuNoturno.GetComponent<SpriteRenderer>().enabled = false;

        StartCoroutine("makeTransparent");
    }

    IEnumerator makeTransparent()
    {
        float qtd = quantidadeDePassos;
        float tempoDormindo = tempoDeTransicao / qtd;
        float diff = (Noite ? -1f : 1f) / qtd;
        int total = (Noite ? 0 : 1);


        // PEGAR OS COMPONENTES
        List<SpriteRenderer> renders = new List<SpriteRenderer>();

        SpriteRenderer parent = grupoCeuDiurno.gameObject.GetComponent<SpriteRenderer>();
        if(parent) renders.Add(parent);

        for(int i=0; i<grupoCeuDiurno.childCount; i++)
        {
            SpriteRenderer chd = grupoCeuDiurno.transform.GetChild(i).GetComponent<SpriteRenderer>();
            if(chd) renders.Add(chd);
        }


        // TROCAR AS CORES
        for(int p=0; p<quantidadeDePassos; p++)
        {
            // ALPHA DO CÉU
            foreach(SpriteRenderer spr in renders)
            {
                Color temp = spr.color;
                temp.a = temp.a + diff;
                spr.color = temp;
            }

            yield return new WaitForSeconds(tempoDormindo);
        }
    }
}


/*

Descrição das funções:

Anoitecer:
    - Fazer o CéuDia ficar transparente
    - Colorir as nuvens para a cor noite


*/