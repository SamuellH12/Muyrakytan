using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class CicloDaNoite : MonoBehaviour
{

    // receber como parâmetro os grupos dos ceus/estrelas
    // criar um script separado para elementos que trocam de cor, baseado no static Noite

    public static bool noite = false;
    public static float tempoDeTransicaoDoCicloDiaNoite = 2f;
    public static int quantidadeDePassosDoCicloDiaNoite = 50;
    [Header("Background dos Céus")]
    [SerializeField] public Transform grupoCeuNoturno;
    [SerializeField] public Transform grupoCeuDiurno;
    [SerializeField] public Transform lua;
    [SerializeField] public float alturaDaLua = 6f;
    private float alturaLuaDia = -15f;
    private float intensidadeDaLua = 2f;
    private float transicaoDaLuz = .3f; // vai fazer a transição de iluminação no 1/partes de tempo da transição completa

    [Header("Grupos que mudam de Cor")]
    [SerializeField] public List<Transform> grupoMudaCor; 
    [SerializeField] public Color corDuranteANoite = new Color(.09f, .11f, .24f);
    [SerializeField] public Color corDutanteODia = Color.white;

    [Header("Iluminação")]
    [SerializeField] public Transform luzGlobal;
    [SerializeField] public Color corDaLuzDia   = Color.white;
    [SerializeField] public Color corDaLuzNoite = new Color(.09f, .11f, .24f);
    // [SerializeField] public List<Transform> grupoApagaAcende; 
    // private List<float> grupoApagaAcendeValor;

    [Header("Transição")]
    [SerializeField] public float tempoDeTransicao = 2f;
    [SerializeField] public int quantidadeDePassos = 100;

    void Awake(){ 
        tempoDeTransicaoDoCicloDiaNoite = tempoDeTransicao; 
        quantidadeDePassosDoCicloDiaNoite = quantidadeDePassos;
    }


    void Update()
    {
        if ( Input.GetKeyDown(KeyCode.Tab))
        {
            if(noite) Amanhecer();
            else Anoitecer();
        }
    }

    public void Anoitecer(int faseDaLua = 0)
    {
        if(noite) return;
        noite = true;
        StartCoroutine("TransicaoDiaNoite");
        StartCoroutine("TransicaoLua");
    }

    public void Amanhecer()
    {
        if(!noite) return;
        noite = false;
        StartCoroutine("TransicaoDiaNoite");
        StartCoroutine("TransicaoLua");
    }

    IEnumerator TransicaoDiaNoite()
    {
        // #DEFINIR AS VARIÁVEIS
        float qtd = quantidadeDePassos;
        float tempoDormindo = tempoDeTransicao / qtd;
        
        // Alpha do Céu
        float CeuDiff = (noite ? -1f : 1f) / qtd;
        float total = (noite ? 0f : 1f);
        
        // Cores dos Objtos
        float cDiffr = (corDutanteODia.r - corDuranteANoite.r) * (noite ? -1f : 1f) / qtd;
        float cDiffg = (corDutanteODia.g - corDuranteANoite.g) * (noite ? -1f : 1f) / qtd;
        float cDiffb = (corDutanteODia.b - corDuranteANoite.b) * (noite ? -1f : 1f) / qtd;

        // Cores da Luz
        float clr = (corDaLuzDia.r - corDaLuzNoite.r) * (noite ? -1f : 1f) / qtd;
        float clg = (corDaLuzDia.g - corDaLuzNoite.g) * (noite ? -1f : 1f) / qtd;
        float clb = (corDaLuzDia.b - corDaLuzNoite.b) * (noite ? -1f : 1f) / qtd;


        // #PEGAR OS COMPONENTES
        List<SpriteRenderer> rendersCeu = new List<SpriteRenderer>();
        List<SpriteRenderer> rendersObj = new List<SpriteRenderer>();

        // Ceu
        SpriteRenderer parent = grupoCeuDiurno.gameObject.GetComponent<SpriteRenderer>();
        if(parent) rendersCeu.Add(parent);

        for(int i=0; i<grupoCeuDiurno.childCount; i++){
            SpriteRenderer chd = grupoCeuDiurno.transform.GetChild(i).GetComponent<SpriteRenderer>();
            if(chd) rendersCeu.Add(chd);
        }

        // Objetos
        foreach(Transform obj in grupoMudaCor){
            SpriteRenderer p = obj.gameObject.GetComponent<SpriteRenderer>();
            if(p) rendersObj.Add(p);
            for(int i=0; i<obj.childCount; i++)
            {
                SpriteRenderer chd = obj.transform.GetChild(i).GetComponent<SpriteRenderer>();
                if(chd) rendersObj.Add(chd);
            }
        }
        
        //Luz global
        Light2D luz = luzGlobal.GetComponent<Light2D>();

        // #TROCAR AS CORES
        for(int p=0; p<quantidadeDePassos; p++){
            // definindo as novas cores
            Color temp = Color.white, nw = Color.white, lz = Color.white;        
            if(rendersCeu.Count > 0){ temp = rendersCeu[0].color; temp.a = temp.a + CeuDiff;}
            if(rendersObj.Count > 0) nw = new Color(rendersObj[0].color.r + cDiffr, rendersObj[0].color.g + cDiffg, rendersObj[0].color.b + cDiffb);
            if(luz) lz = new Color(luz.color.r + clr, luz.color.g + clg, luz.color.b + clb);


            // aplicando as cores
            foreach(SpriteRenderer spr in rendersCeu){ spr.color = temp; }
            foreach(SpriteRenderer spr in rendersObj){ spr.color = nw; }
            if(luz) luz.color = lz;

            yield return new WaitForSeconds(tempoDormindo);
        }

        // Colocando as cores absolutas
        Color nw_ = (noite ? corDuranteANoite : corDutanteODia), lz_ = (noite ? corDaLuzNoite : corDaLuzDia);
        foreach(SpriteRenderer spr in rendersCeu){ Color temp = spr.color; temp.a = total; spr.color = temp; }
        foreach(SpriteRenderer spr in rendersObj){ spr.color = nw_; }
        if(luz) luz.color = lz_;
    }

    IEnumerator TransicaoLua()
    {
        Parallax luaParallax = lua.GetComponent<Parallax>();
        Light2D luz = lua.GetComponent<Light2D>();
        
        // setar altura inicial
        float h0 = (noite ? alturaLuaDia : alturaDaLua);
        float n = transicaoDaLuz;
        luaParallax.SetHeight( h0 );
        if(noite) lua.GetComponent<SpriteRenderer>().enabled = true;
        luz.intensity = (!noite ? intensidadeDaLua : 0f);

        float qtd = quantidadeDePassos;
        float tempoDormindo = tempoDeTransicao / qtd;
        float diff = (alturaDaLua - alturaLuaDia) / qtd * (noite ? 1f : -1f);
        float dfl = (intensidadeDaLua / qtd * (noite ? 1f : -1f)) / n;

        for(int p=0, cnt = (int)(qtd*n) ; p<quantidadeDePassos; p++)
        { 
            h0 += diff;
            luaParallax.SetHeight(h0);
            
            if((!noite && cnt > 0) || (noite && quantidadeDePassos - p == cnt))
            {
                luz.intensity = luz.intensity + dfl;
                cnt--;
            }

            yield return new WaitForSeconds(tempoDormindo);
        }

        //setar altura final
        luaParallax.SetHeight( (noite ? alturaDaLua : alturaLuaDia) );
        lua.GetComponent<SpriteRenderer>().enabled = noite;
        luz.intensity = (noite ? intensidadeDaLua : 0f);
    }
}
