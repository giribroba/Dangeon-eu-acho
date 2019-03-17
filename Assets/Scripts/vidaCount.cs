using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vidaCount : MonoBehaviour
{
    public static float Vida;
    public GameObject Player;

    void Start()
    {
        Vida = 100;  
    }

    void Update()
    {

        if (transform.localScale.x < Vida / 20 && Vida >= 0)
        {
            transform.localScale = new Vector3(transform.localScale.x + 0.2f, transform.localScale.y, transform.localScale.z);
        }
        if (transform.localScale.x > Vida / 20 && Vida >= 0)
        {
            transform.localScale = new Vector3(transform.localScale.x - 0.2f, transform.localScale.y, transform.localScale.z);
        }
        if (Vida <= 0)
        {
            Destroy(Player);
        }
        Debug.Log(Vida);
    }
}
