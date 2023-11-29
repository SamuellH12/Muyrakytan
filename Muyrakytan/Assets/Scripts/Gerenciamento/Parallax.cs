using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{

    [Header("Parallax in X axis")]
    [SerializeField] private bool parallaxX = true;
    [Range(-1f, 1f)] [SerializeField] private float effectX = 0.5f;

    [Header("Parallax in Y axis")]
    [SerializeField] private bool parallaxY = true;
    [Range(-1f, 1f)] [SerializeField] private float effectY = 0.5f;
    
    [Header("Infinity Repetition Parallax X")]
    [SerializeField] private bool infinity = true;
    [SerializeField] private bool autoWidth = true;
    [SerializeField] private float width;

    private Vector3 startPos;
    private Transform cam;


    void Start()
    {
        startPos = transform.position;
        if(autoWidth) width = GetComponent<SpriteRenderer>().bounds.size.x;
        //cam = Camera.main.transform;
        cam = GameObject.FindWithTag("MainCamera").transform;
    }

    void Update()
    {
        if( cam != null ){
            float distx = parallaxX ? (cam.transform.position.x * effectX) : 0f;
            float disty = parallaxY ? (cam.transform.position.y * effectY) : 0f;

            transform.position = new Vector3(startPos.x + distx, startPos.y + disty, startPos.z);

            if(infinity)
            {
                float repos = cam.transform.position.x * (1 - effectX);

                if(repos > startPos.x + width) startPos.x += width;
                else
                if(repos < startPos.x - width) startPos.x -= width;
            }
        }
        else Debug.Log(transform.position.x + " " + transform.position.y);
    }
}


/*

# Sobre os valores #
Quão próximos ou distantes da câmera estão os
objetos de acordo com os valores:

    1.0   -> Infinitamente distante / Estático
    0.9   -> Muito Longe

    0.75  -> Longe
    0.5   -> Distante
    0.25  -> Atrás do player

    0.0   -> Na mesma distância do player

    -0.25 -> Na frente do player
    -0.5  -> Perto
    -0.75 -> Muito perto da câmera
*/