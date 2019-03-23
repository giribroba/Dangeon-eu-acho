using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projetil : MonoBehaviour
{
    public float speed;
    public float xmax;
    public float ymax;
    public GameObject bolaEnergetica;
    public float danoBase;
    public bool Magic; 
    public float danoTotal;

    void Update()
    {
        movimentação();
    }
    /// <summary>
    /// movimenta o projétil e destroi ele ao passar dos limites do cenario.
    /// </summary>
    void movimentação()
    {

        transform.Translate(Vector3.up * speed * Time.deltaTime);
        // O projétil irá se destruir ao passar dos limites do cenario
        if (transform.position.x > xmax || transform.position.y > ymax)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag != "Player")
        {
            Dano();
            Destroy(gameObject);
        }
    }

    void Dano()
    {
        danoTotal = danoBase + 0.8f * playerBehavior.poderMagico;
    }
}
