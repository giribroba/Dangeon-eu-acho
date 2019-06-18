using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class algoBeavior : MonoBehaviour
{
    float defPercent;
    float armTotal = 400;
    private float distance;
    private bool alerta;
    private int X, Y;

    [SerializeField] float vida, speed, danoBase, forcaFisica, detectaPlayer, visão;
    [SerializeField] int resistMagica; //-1 = fraquesa / 0 = normal / 1 = resistencia 
    [SerializeField] GameObject player, arma, mao, detectaCosta, detectaFrente;

    public float danoTotal;

    private void Start()
    {
        danoTotal = danoBase + 0.8f * forcaFisica;
        InvokeRepeating("Random", 0, 0.5f);
        alerta = false;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        //Detecta colisão com um projetil / Recebe dano
        if (other.tag == "Projétil")
        {
            alerta = true;
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
        MoveToPlayer();
    }
    void MoveToPlayer()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
        alerta = (distance <= visão) ? true : alerta;
        if (alerta)
        {
            arma.GetComponent<SpriteRenderer>().enabled = true;
            GetComponent<SpriteRenderer>().flipX = player.transform.position.x < transform.position.x ;
            mao.transform.localPosition = new Vector2(((player.transform.position.x < transform.position.x)? -0.07f : 0.07f), -0.045f);
            GetComponent<Animator>().SetBool("Andando", true);
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        }
        else
        {
            if (X == 0 && Y == 0)
            {
                GetComponent<Animator>().SetBool("Andando", false);
            }
            else
            {
                GetComponent<Animator>().SetBool("Andando", true);
            }
            if (X < 0)
            {
                GetComponent<SpriteRenderer>().flipX = true;
            }
            else
            {
                GetComponent<SpriteRenderer>().flipX = false;
            }
            transform.Translate(Vector2.right * X * speed * Time.deltaTime);
            transform.Translate(Vector2.up * Y * speed * Time.deltaTime);
        }
        if(distance <= detectaPlayer)
        {
            if (player.transform.position.y - transform.position.y > 0.4f)
            {
                GetComponent<Animator>().SetFloat("Ataque", 0.66f);
            }
            else if(player.transform.position.y - transform.position.y < -1.15f)
            {
                GetComponent<Animator>().SetFloat("Ataque", 1);
            }
            else if(player.transform.position.x < transform.position.x)
            {
                GetComponent<Animator>().SetFloat("Ataque", 0.33f);
            }
            else
            {
                GetComponent<Animator>().SetFloat("Ataque", 0);
            }
            GetComponent<Animator>().SetTrigger("Atacando");
        }
    }
    void defCalc()
    {
        defPercent = (4 * Convert.ToSingle(Math.Sqrt(armTotal))) / 100;
    }
    void Random()
    {
        X = UnityEngine.Random.Range(-1, 2);
        Y = UnityEngine.Random.Range(-1, 2);
    }
    void Dano()
    {
        player.GetComponent<playerBehavior>().Dano(danoTotal);  
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,  detectaPlayer);
        Gizmos.DrawWireSphere(transform.position, visão);
    }

}
