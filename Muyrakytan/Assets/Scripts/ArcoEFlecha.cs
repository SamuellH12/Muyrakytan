using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcoEFlecha : MonoBehaviour
{
    
    [SerializeField] private Transform Arco;
    [SerializeField] private GameObject Flecha;
    [SerializeField] private float forcaDaFlecha = 10f;

    void Update()
    {
        if(Input.GetButtonDown("Fire1") && !PauseControl.paused) AtirarFlecha();
    }

    private void AtirarFlecha()
    {
        GameObject flecha = Instantiate(Flecha);
        flecha.transform.position = Arco.position;

        float normal = 1;
        
        if(transform.localScale.x < 0)
        {
            normal = -1;
            Vector3 scale = flecha.transform.localScale;
            scale.x *= -1;
            flecha.transform.localScale = scale;
        }
        
        flecha.GetComponent<Rigidbody2D>().velocity = new Vector2(forcaDaFlecha * normal, 0);

        Destroy(flecha.gameObject, 5f);
    }

}
