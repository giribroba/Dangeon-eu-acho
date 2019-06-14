using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class algoBeavior : MonoBehaviour
{
    float defPercent;
    float armTotal = 400;

    [SerializeField] float vida, speed;
    [SerializeField] int resistMagica; //-1 = fraquesa / 0 = normal / 1 = resistencia 
    [SerializeField] GameObject Player;

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
                        vida -= (other.GetComponent<projetil>().danoTotal * (1 - defPercent)) + (playerBehavior.poderMagico / 3); 
                        break;
                    case 0:
                        vida -= (other.GetComponent<projetil>().danoTotal * (1 - defPercent));
                        break;
                    case 1:
                        vida -= (other.GetComponent<projetil>().danoTotal * (1 - defPercent)) - (playerBehavior.poderMagico / 3);
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
