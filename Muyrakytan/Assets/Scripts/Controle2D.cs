using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Controle2D : MonoBehaviour
{
    [SerializeField] private float jumpForce = 70f;  // força do pulo

    [Range(0, .3f)] [SerializeField] private float suavizacao = .05f;   //suavização de movimento
	[SerializeField] private LayerMask layerChao;      //layers que representam o chao
	[SerializeField] private Transform groundCheck; //objeto para checar se o personagem está no chao

    private Rigidbody2D Rig;         // rigidbody do personagem

    private bool noChao = false; //está no chão
    public bool facingDir = true;   //direção que o personagem está olhando
    [Range(0, 1f)] [SerializeField] private float raioChaoCheck = .2f; //raio de checagem para o chao
	private Vector3 velocity = Vector3.zero;


    private void Awake()
    {
        Rig = GetComponent<Rigidbody2D>();
    }


    private void FixedUpdate()
    {
        //verifica se o player está no chão
        noChao = Physics2D.OverlapCircle(groundCheck.position, raioChaoCheck, layerChao);
    }

    public void aplicarMovimento(float movimento, bool jump)
    {
        Vector2 velocidade = new Vector2(movimento, Rig.velocity.y);

        Rig.velocity = Vector3.SmoothDamp(Rig.velocity, velocidade, ref velocity, suavizacao);
    
        if(jump && noChao) Rig.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);

        if(movimento > 0 && !facingDir) flip();
        if(movimento < 0 &&  facingDir) flip();
    }

    public void flip()
    {
        facingDir = !facingDir;

        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
