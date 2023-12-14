using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafePoint : MonoBehaviour
{
    private AreaDeDano areaDeDano;

    void Start()
    {
        areaDeDano = transform.parent.GetComponent<AreaDeDano>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "Player") 
            areaDeDano.safeArea = transform;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position, 1);
    }
}
