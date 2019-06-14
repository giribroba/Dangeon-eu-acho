using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerBehavior : MonoBehaviour
{
    float ultimoDisparo;

    [SerializeField] float speed,yMax,xMax,coolDown;
    [SerializeField] GameObject ataqueDoMago, arma, ponta, mao;

    public static float xMoviment, yMoviment;
    public static bool move;
    public static float poderMagico = 50;

    private Vector2 pMao;

    private void Start()
    {
        move = true;
        pMao = mao.transform.position - transform.position;
    }

    void Update()
    {
        Moviment();
        Limit();
        Ataque();
    }

    /// <summary>
    /// Essa função controla a movimentação do personagem
    /// </summary>
    void Moviment()
    {
        //Impulso
        xMoviment = Input.GetAxis("Horizontal");
        yMoviment = Input.GetAxis("Vertical");

        if(xMoviment != 0 || yMoviment != 0)
        {
            GetComponent<Animator>().SetBool("Andando", true);
        }
        else
        {
            GetComponent<Animator>().SetBool("Andando", false);
        }
        //Direção
        if (move)
        {
            transform.Translate(Vector3.right * xMoviment * speed * Time.deltaTime);
            transform.Translate(Vector3.up * yMoviment * speed * Time.deltaTime);
        }

    }

    /// <summary>
    /// Essa função limita o movimento do player
    /// </summary>
    void Limit()
    {
        //Eixo X
        if (transform.position.x - Camera.main.transform.position.x > xMax)
        {
            transform.position = new Vector3(xMax + Camera.main.transform.position.x, transform.position.y);
        }
        else if (transform.position.x - Camera.main.transform.position.x < -xMax)
        {
            transform.position = new Vector3(-xMax + Camera.main.transform.position.x, transform.position.y);
        }

        //Eixo Y
        if (transform.position.y - Camera.main.transform.position.y > yMax)
        {
            transform.position = new Vector3(transform.position.x, yMax + Camera.main.transform.position.y);
        }
        else if (transform.position.y - Camera.main.transform.position.y < -yMax)
        {
            transform.position = new Vector3(transform.position.x, -yMax + Camera.main.transform.position.y);
        }
    }
    /// <summary>
    /// Lança um ataque básico quando o jogador pressiona a barra de espaço
    /// </summary>
    void Ataque()
    {
        arma.transform.up  = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x - arma.transform.position.x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y - arma.transform.position.y);
        if(arma.transform.eulerAngles.z > 20 && arma.transform.eulerAngles.z < 160)
        {
            mao.transform.position = new Vector2((transform.position.x + pMao.x) - 0.1f, (transform.position.y + pMao.y) - 0.1f);
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (arma.transform.eulerAngles.z > 200  && arma.transform.eulerAngles.z < 340)
        {
            mao.transform.position = new Vector2(transform.position.x + pMao.x, transform.position.y + pMao.y);
            GetComponent<SpriteRenderer>().flipX = false;
        }

        if (Input.GetKey(KeyCode.Mouse0) && ultimoDisparo >= coolDown)
        {
            Instantiate(ataqueDoMago, new Vector3(ponta.transform.position.x, ponta.transform.position.y), arma.transform.rotation);
            ultimoDisparo = 0;
        }

        ultimoDisparo += Time.deltaTime;
   }
}
