using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoMovingBackground : MonoBehaviour
{
    [SerializeField] private float velocidade = -3f;

    private float minDist, maxDist, tempo;

    void Start()
    {
        for(int i=0; i < transform.childCount; i++)
        {
            Transform chd = transform.GetChild(i).transform;
            minDist = Mathf.Min(chd.position.x - transform.position.x, minDist);
            maxDist = Mathf.Max(chd.position.x - transform.position.x, maxDist);
        }
        tempo = Time.fixedDeltaTime;
        Debug.Log(minDist + "m  M" + maxDist);
    }

    void FixedUpdate()
    {
        for(int i=0; i < transform.childCount; i++)
        {
            Transform chd = transform.GetChild(i).transform;
            
            if(velocidade < 0 && chd.position.x - transform.position.x < minDist) 
                chd.position = new Vector3(transform.position.x + maxDist, chd.position.y, chd.position.z);
            else
            if(velocidade > 0 && chd.position.x - transform.position.x > maxDist)
                chd.position = new Vector3(transform.position.x + minDist, chd.position.y, chd.position.z);
            
            chd.position = new Vector3(chd.position.x + (velocidade*tempo), chd.position.y, chd.position.z);
        }
        
    }
}
