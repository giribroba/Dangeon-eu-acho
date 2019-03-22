using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerBehavior : MonoBehaviour
{
    float ultimoDisparo;

    public float speed;
    public float xMax;
    public float yMax;
    public float coolDown;

    public static float xMoviment;
    public static float yMoviment;
    public static bool move;

    public GameObject ataqueDoMago;

    private void Start()
    {
        move = true;
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
        if (transform.position.x > xMax)
        {
            transform.position = new Vector3(xMax, transform.position.y);
        }
        else if (transform.position.x < -xMax)
        {
            transform.position = new Vector3(-xMax, transform.position.y);
        }

        //Eixo Y
        if (transform.position.y > yMax)
        {
            transform.position = new Vector3(transform.position.x, yMax);
        }
        else if (transform.position.y < -yMax)
        {
            transform.position = new Vector3(transform.position.x, -yMax);
        }
    }

    void Ataque()
    {
        if (Input.GetKeyDown(KeyCode.Space) && ultimoDisparo >= coolDown)
        {
            Instantiate(ataqueDoMago, new Vector3(transform.position.x, transform.position.y), Quaternion.identity);
            ultimoDisparo = 0;
        }

        ultimoDisparo += Time.deltaTime;
   }
}
