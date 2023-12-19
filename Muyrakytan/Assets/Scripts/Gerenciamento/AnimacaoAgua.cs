using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimacaoAgua : MonoBehaviour
{
    [SerializeField] private float maxDist = 1f;
    [SerializeField] private float diff = 0.1f;
    private float diffAtual;
    private float posOriginal;

    void Start(){ 
        diffAtual = diff; 
        posOriginal = transform.position.x;
    }

    void FixedUpdate()
    {
        if(transform.position.x - posOriginal >=  maxDist) diffAtual = -diff;
        if(transform.position.x - posOriginal <= -maxDist) diffAtual = +diff;

        Vector3 pos = transform.position;
        pos.x += diffAtual;

        transform.position = pos;
    }
}
