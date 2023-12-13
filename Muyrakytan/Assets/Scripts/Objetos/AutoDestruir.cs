using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestruir : MonoBehaviour
{
    [SerializeField] public float tempoProItemDesaparecer = 4f;
    public bool autoDestruir = false;
    private float tempo = 0;

    void Update()
    {
        tempo += Time.deltaTime;
        if(autoDestruir && tempo > tempoProItemDesaparecer) Destroy(gameObject);
    }
}
