using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentoJogador : MovimentoPersonagem
{
    public GameObject posicaoMira;

    public void RotacaoJogador(LayerMask MascaraChao)
    {

        //mira com cursor do mouse
        //Ray raio = Camera.main.ScreenPointToRay(Input.mousePosition);
        //Debug.DrawRay(raio.origin, raio.direction * 100, Color.red);

        //RaycastHit impacto;

        //if (Physics.Raycast(raio, out impacto, 100, MascaraChao))
        //{
        //    Vector3 posicaoMiraJogador = impacto.point - transform.position;

        //    posicaoMiraJogador.y = transform.position.y;

        //    Rotacionar(posicaoMiraJogador);

        //}

        
        //mira com icone para ficar correto o centro
        Ray raio = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(raio.origin, raio.direction * 100, Color.red);

        Plane plano = new Plane(Vector3.up, posicaoMira.transform.position);

        float distanciaColisao;

        if (plano.Raycast(raio, out distanciaColisao))
        {
            Vector3 localColisao = raio.GetPoint(distanciaColisao);
            localColisao.y = 0; //ou transform.position.y

            //direcao para onde vamos olhar baseado onde estamos
            Vector3 posicaoParaOlhar = localColisao - transform.position;

            Quaternion novaRotacao = Quaternion.LookRotation(posicaoParaOlhar);
            GetComponent<Rigidbody>().MoveRotation(novaRotacao);
        }
    }

}
