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
        Quaternion rotacaOpostaBala = Quaternion.LookRotation(-transform.forward);
        switch(objetoDeColisao.tag)
        {
            case "Inimigo":
                ControlaInimigo inimigo = objetoDeColisao.GetComponent<ControlaInimigo>();
                inimigo.TomarDano(danoTiro);
                inimigo.ParticulaSangue(transform.position, rotacaOpostaBala);
                break;
            case "ChefeFase":
                ControlaChefe chefe = objetoDeColisao.GetComponent<ControlaChefe>();
                chefe.TomarDano(danoTiro);
                chefe.ParticulaSangue(transform.position, rotacaOpostaBala);
                break;

        }

        Destroy(gameObject);
    }
}
