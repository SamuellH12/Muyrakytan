using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformaMovel : MonoBehaviour
{
    [SerializeField] private bool horizontal = true;
    [Tooltip("Movimento Horizontal ou Vertical")]
    [SerializeField] private float distanciaPercorrida = 5f;
    [SerializeField] private float velocidade_ = 5f;
    [Tooltip("Direcao inicial")]
    [SerializeField] private bool dirCima = true;
    private float vel = 0f, oldDist;
    private float maxL, maxR;
    Vector3 initialPos;

    void Start()
    {
        initialPos = transform.position;
        updateDist();
    }

    void FixedUpdate()
    {
        if(oldDist != distanciaPercorrida) updateDist();
        if( (horizontal && transform.position.x <= maxL) || (!horizontal && transform.position.y <= maxL) ) dirCima = true;
        if( (horizontal && transform.position.x >= maxR) || (!horizontal && transform.position.y >= maxR) ) dirCima = false;
        vel = velocidade_ / (10000f * Time.fixedDeltaTime);
        
        if(horizontal) 
            transform.position  = new Vector3( transform.position.x + vel * (dirCima ? 1f : -1f), transform.position.y, transform.position.z);
        else transform.position = new Vector3( transform.position.x, transform.position.y + vel * (dirCima ? 1f : -1f), transform.position.z);
    }


    private void updateDist(){
        maxL = (horizontal ? initialPos.x : initialPos.y) - distanciaPercorrida/2f;
        maxR = (horizontal ? initialPos.x : initialPos.y) + distanciaPercorrida/2f;
        oldDist = distanciaPercorrida;
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "Player")
            collision.transform.parent = transform;
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.transform.tag == "Player") 
            collision.transform.parent = null;
    }





    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        if(vel != 0f)
        {
            if(horizontal)
            {
                Gizmos.DrawSphere(new Vector3((horizontal ? initialPos.x : initialPos.y) - distanciaPercorrida/2f, transform.position.y, transform.position.z), 0.2f);
                Gizmos.DrawSphere(new Vector3((horizontal ? initialPos.x : initialPos.y) + distanciaPercorrida/2f, transform.position.y, transform.position.z), 0.2f);
            } 
            else
            {
                Gizmos.DrawSphere(new Vector3(transform.position.x, (horizontal ? initialPos.x : initialPos.y) - distanciaPercorrida/2f, transform.position.z), 0.2f);
                Gizmos.DrawSphere(new Vector3(transform.position.x, (horizontal ? initialPos.x : initialPos.y) + distanciaPercorrida/2f, transform.position.z), 0.2f);
            }
        }
        else
        {
            if(horizontal)
            {
                Gizmos.DrawSphere(new Vector3((horizontal ? transform.position.x : transform.position.y) - distanciaPercorrida/2f, transform.position.y, transform.position.z), 0.2f);
                Gizmos.DrawSphere(new Vector3((horizontal ? transform.position.x : transform.position.y) + distanciaPercorrida/2f, transform.position.y, transform.position.z), 0.2f);
            } 
            else
            {
                Gizmos.DrawSphere(new Vector3(transform.position.x, (horizontal ? transform.position.x : transform.position.y) - distanciaPercorrida/2f, transform.position.z), 0.2f);
                Gizmos.DrawSphere(new Vector3(transform.position.x, (horizontal ? transform.position.x : transform.position.y) + distanciaPercorrida/2f, transform.position.z), 0.2f);
            }
        }
    }
}
