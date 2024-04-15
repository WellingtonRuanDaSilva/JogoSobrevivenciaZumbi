using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitMedico : MonoBehaviour
{
    private int quantidadeCura = 15;
    private int tempoDestruicao = 5;

    private void Start()
    {
        Destroy(gameObject, tempoDestruicao);
    }

    private void OnTriggerEnter(Collider objetoDeColisao)
    {
        if (objetoDeColisao.tag == "Jogador")
        {
            objetoDeColisao.GetComponent<ControlaJogador>().CurarVida(quantidadeCura);
            Destroy(gameObject);
        }
    }
}
