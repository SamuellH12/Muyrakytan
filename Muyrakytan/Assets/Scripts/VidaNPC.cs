using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VidaNPC : MonoBehaviour
{
    [SerializeField] public int vidaAtual = 10;
    [SerializeField] private float tempoCorDano = 1f;
    [SerializeField] private Color corDeDano = new Color(200, 10, 10);
    private Color originalColor;
    private SpriteRenderer sprite;
    
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        originalColor = sprite.color;
    }

    public void Dano(int dano=1)
    {
        vidaAtual -= dano;

        StartCoroutine("TrocarCor");        

        if(vidaAtual <= 0) Morte();
    }

    private void Morte(){
        //animação de morte

        Destroy(gameObject);
    }

    IEnumerator TrocarCor(){
        sprite.color = corDeDano;

        yield return new WaitForSeconds(tempoCorDano);

        sprite.color = originalColor;
    }
}
