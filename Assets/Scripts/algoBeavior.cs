using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class algoBeavior : MonoBehaviour
{
    float xPush;
    float yPush;
    float defPercent;
    float armTotal = 400;

    public float vida;
    public float speed;
    public int resistMagica; //-1 = fraquesa / 0 = normal / 1 = resistencia 
    public GameObject Player;

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Detecta colisão com o player / Causa dano
        if (other.tag == "Player") 
        {   
            vidaCount.Vida -= 10;
        }
        //Detecta colisão com um projetil / Recebe dano
        if (other.tag == "Projétil")
        {
            //Resistencia magica
            if (other.GetComponent<projetil>().Magic)
            {
                switch (resistMagica)
                {
                    case -1:
                        vida -= (other.GetComponent<projetil>().danoTotal * defPercent) + (playerBehavior.poderMagico / 3); 
                        break;
                    case 0:
                        vida -= (other.GetComponent<projetil>().danoTotal * defPercent);
                        break;
                    case 1:
                        vida -= (other.GetComponent<projetil>().danoTotal * defPercent) - (playerBehavior.poderMagico / 3);
                        break;
                }
            }
            else
            {
                vida -= (other.GetComponent<projetil>().danoTotal * defPercent);
            }

        }
    }

    private void Update()
    {
        defCalc();
        Debug.Log("Vida: " + vida);
        Debug.Log(defPercent);
        if (vida <= 0)
        {
            Destroy(gameObject);
        }
    }

    void defCalc()
    {
        defPercent = (4 * Convert.ToSingle(Math.Sqrt(armTotal))) / 100;
    }
}
