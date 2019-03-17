using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class algoBeavior : MonoBehaviour
{
    float empurrando;

    public float speed;

    public float tempoEmpurrar;
    public GameObject Player;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player") 
        {   
            vidaCount.Vida -= 10;
            empurrando = tempoEmpurrar;
        }
    }

    private void Update()
    {
        if (empurrando > 0 && playerBehavior.xMoviment != 0)
        {
            Player.transform.Translate(Vector3.right * -playerBehavior.xMoviment * speed * Time.deltaTime);
            playerBehavior.move = false;
        }
        else if (empurrando > 0 && playerBehavior.yMoviment != 0)
        {
            Player.transform.Translate(Vector3.right * -playerBehavior.yMoviment * speed * Time.deltaTime);
            playerBehavior.move = false;
        }
        else
        {
            playerBehavior.move = true;
        }
        empurrando -= Time.deltaTime;
    }
}
