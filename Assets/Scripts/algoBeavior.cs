using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class algoBeavior : MonoBehaviour
{
    float defPercent;
    float armTotal = 400;
    private float distance;
    private bool movimento, empurrar;

    [SerializeField] float vida, speed;
    [SerializeField] int resistMagica; //-1 = fraquesa / 0 = normal / 1 = resistencia 
    [SerializeField] GameObject Player, arma, mao;

    private Vector2 pMao;

    private void Start()
    {
        pMao = mao.transform.position - transform.position;
        movimento = false;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        //Detecta colisão com o player / Causa dano
        if (other.tag == "Player") 
        {   
	    StartCoroutine(Ataque());
        }
        //Detecta colisão com um projetil / Recebe dano
        if (other.tag == "Projétil")
        {
            movimento = true;
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
	    playerBehavior.move = true;
            Destroy(gameObject);
        }
	if (empurrar)
	{
	    Player.transform.Translate(Vector2.right * ((Player.transform.position.x < transform.position.x)?-1.5f:1.5f)* speed * Time.deltaTime);
	    Player.transform.Translate(Vector2.up * ((Player.transform.position.y < transform.position.y)?-1.5f:1.5f)* speed * Time.deltaTime);
	}
        MoveToPlayer();
    }

    void MoveToPlayer()
    {
        distance = Vector2.Distance(transform.position, Player.transform.position);
        movimento = (distance <= 5) ? true : movimento;
        if (movimento)
        {
            arma.GetComponent<SpriteRenderer>().enabled = true;
            if ((Player.transform.position.x - transform.position.x) < 0)
            {
                GetComponent<Animator>().SetFloat("Ataque", 0);
                mao.transform.position = new Vector2((transform.position.x + pMao.x) - 0.1f, (transform.position.y + pMao.y) - 0.1f);
                GetComponent<SpriteRenderer>().flipX = true;
            }
            else
            {
                GetComponent<Animator>().SetFloat("Ataque", 1);
                mao.transform.position = new Vector2(transform.position.x + pMao.x, transform.position.y + pMao.y);
                GetComponent<SpriteRenderer>().flipX = false;
            }
            GetComponent<Animator>().SetBool("Andando", true);
            transform.position = Vector2.MoveTowards(transform.position, Player.transform.position, speed * Time.deltaTime);
        }
    }
    void defCalc()
    {
        defPercent = (4 * Convert.ToSingle(Math.Sqrt(armTotal))) / 100;
    }

    IEnumerator Ataque()
    {    
	GetComponent<Animator>().SetBool("Atacando", true);	
	empurrar = true;
	playerBehavior.move = false;
	yield return new WaitForSeconds(0.3f);
	GetComponent<Animator>().SetBool("Atacando", false);
	playerBehavior.move = true;
	empurrar = false;
    }
}
