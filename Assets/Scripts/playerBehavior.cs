using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerBehavior : MonoBehaviour
{
    private SpriteRenderer sr;
    private Animator anim;

    float ultimoDisparo, defPercent;
    Vector2 pMouse;

    [SerializeField] float speed, yMax, xMax, coolDown, armTotal;
    [SerializeField] GameObject ataqueDoMago, arma, ponta, mao, controlador, fade;

    public static float xMoviment, yMoviment;
    public static bool move;
    public static float poderMagico = 50;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        move = true;
    }

    void Update()
    {
        pMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        defPercent = (4 * Convert.ToSingle(Math.Sqrt(armTotal))) / 100;
        Moviment();
        Ataque();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Room")
        {
            Camera.main.transform.position = new Vector3(collision.transform.position.x, collision.transform.position.y, Camera.main.transform.position.z);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Room")
        {
            fade.GetComponent<Animator>().SetTrigger("Fade");
        }
    }

    /// <summary>
    /// Essa função controla a movimentação do personagem
    /// </summary>
    void Moviment()
    {
        //impulso
        controlador.transform.localPosition = new Vector2(Input.GetAxis("Horizontal") / 10, Input.GetAxis("Vertical") / 10);

        //movimento
        transform.position = Vector2.MoveTowards(transform.position, controlador.transform.position, speed * Time.deltaTime);      
        anim.SetBool("Andando", controlador.transform.localPosition != Vector3.zero);        
    }
    /// <summary>
    /// Lança um ataque básico quando o jogador pressiona a barra de espaço
    /// </summary>
    void Ataque()
    {
        arma.transform.up  = new Vector2(pMouse.x - arma.transform.position.x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y - arma.transform.position.y);
        if(arma.transform.eulerAngles.z > 20 && arma.transform.eulerAngles.z < 160 && Mathf.Abs(pMouse.x - transform.position.x) > 0.5f)
        {
            mao.transform.localPosition = new Vector2(0.05f, -0.055f);
            sr.flipX = true;
        }
        else if (arma.transform.eulerAngles.z > 200  && arma.transform.eulerAngles.z < 340 && Mathf.Abs(pMouse.x - transform.position.x) > 0.5f)
        {
            mao.transform.localPosition = new Vector2(0.07f, -0.045f);
            sr.flipX = false;
        }

        if (Input.GetKey(KeyCode.Mouse0) && ultimoDisparo >= coolDown)
        {
            Instantiate(ataqueDoMago, new Vector3(ponta.transform.position.x, ponta.transform.position.y), arma.transform.rotation);
            ultimoDisparo = 0;
        }

        ultimoDisparo += Time.deltaTime;
   }
    
    public void Dano(float danoTotal)
    {
        vidaCount.Vida -= (danoTotal * (1 - defPercent));
    }
}
