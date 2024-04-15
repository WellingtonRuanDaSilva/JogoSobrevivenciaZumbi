using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class GeradorZumbis : MonoBehaviour
{
    public GameObject Zumbi;
    private float contadorTempo = 0;
    public float TempoGerarZumbi = 1;
    public LayerMask LayerZumbi;
    private float distanciaDeGeracao = 3;
    private float DistanciaDoJogadorPraGeracao = 20;
    private GameObject jogador;
    private int quantidadeMaximaZumbisVivos = 2;
    private int quantidadeZumbisVivos;
    private float tempoProximoAumentoDificuldade = 30;
    private float contadorAumentarDificuldade;

    

    private void Start()
    {
        jogador = GameObject.FindWithTag("Jogador");
        contadorAumentarDificuldade = tempoProximoAumentoDificuldade;
        for (int i = 0; i < quantidadeMaximaZumbisVivos;  i++)
        {
            StartCoroutine(GerarNovoZumbi());
        }
    }

    void Update()
    {
        bool possoGerarZumbisPelaDistancia = Vector3.Distance(transform.position, jogador.transform.position) > DistanciaDoJogadorPraGeracao;

        if (possoGerarZumbisPelaDistancia == true && quantidadeZumbisVivos < quantidadeMaximaZumbisVivos)
        {
            contadorTempo += Time.deltaTime;

            if (contadorTempo >= TempoGerarZumbi)
            {
                StartCoroutine(GerarNovoZumbi());
                contadorTempo = 0;
            }
        }

        if(Time.timeSinceLevelLoad > contadorAumentarDificuldade)
        {
            quantidadeMaximaZumbisVivos++;
            contadorAumentarDificuldade = Time.timeSinceLevelLoad + tempoProximoAumentoDificuldade;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, distanciaDeGeracao);
    }

    IEnumerator GerarNovoZumbi()
    {
        Vector3 posicaoDeCriacao = AleatorizarPosicao();
        Collider[] colisores = Physics.OverlapSphere(posicaoDeCriacao, 1, LayerZumbi);

        while(colisores.Length > 0)
        {
            posicaoDeCriacao = AleatorizarPosicao();
            colisores = Physics.OverlapSphere(posicaoDeCriacao, 1, LayerZumbi);
            yield return null;
        }

        ControlaInimigo zumbi = Instantiate(Zumbi, posicaoDeCriacao, transform.rotation).GetComponent<ControlaInimigo>();
        zumbi.meuGerador = this;
        quantidadeZumbisVivos++;
    }

     Vector3 AleatorizarPosicao()
    {
        Vector3 posicao = Random.insideUnitSphere * distanciaDeGeracao;
        posicao += transform.position;
        posicao.y = 0;

        return posicao;
    }
    public void DiminuirQuantidadeZumbisVivos()
    {
        quantidadeZumbisVivos--;
    }
}
