using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projetil : MonoBehaviour
{
    [SerializeField] float speed, xMax, yMax, danoBase;
    [SerializeField] GameObject bolaEnergetica;
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
        if (transform.position.x > xMax + Camera.main.transform.position.x || transform.position.y > yMax + Camera.main.transform.position.y || transform.position.x < -xMax + Camera.main.transform.position.x|| transform.position.y < -yMax + Camera.main.transform.position.y)
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
