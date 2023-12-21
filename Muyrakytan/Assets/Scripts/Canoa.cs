using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canoa : MonoBehaviour
{
    
    void Start()
    {
        
    }

    void Update()
    {
        
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
}
