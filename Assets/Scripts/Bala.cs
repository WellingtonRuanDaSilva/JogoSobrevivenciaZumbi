using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala : MonoBehaviour
{
    public float Velocidade = 20;
    private Rigidbody rigidbodyBala;
    public AudioClip SomMorte;
    private int danoTiro = 1;

    private void Start()
    {
        rigidbodyBala = GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
        rigidbodyBala.MovePosition(rigidbodyBala.position + transform.forward * Velocidade * Time.deltaTime);   
    }

    void OnTriggerEnter(Collider objetoDeColisao)
    {
        switch(objetoDeColisao.tag)
        {
            case "Inimigo":
                objetoDeColisao.GetComponent<ControlaInimigo>().TomarDano(danoTiro);
                break;
            case "ChefeFase":
                objetoDeColisao.GetComponent<ControlaChefe>().TomarDano(danoTiro);
                break;

        }

        Destroy(gameObject);
    }
}
