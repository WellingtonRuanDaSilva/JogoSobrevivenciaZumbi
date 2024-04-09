using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlaInimigo : MonoBehaviour, IMatavel
{

    public GameObject Jogador;
    private MovimentoPersonagem movimentoInimigo;
    private AnimacaoPersonagem animacaoInimigo;
    private Status statusInimigo;
    public AudioClip SomMorte;
    private Vector3 posicaoAleatoria;
    private Vector3 direcao;
    private float contadorVagar;
    private float tempoEntrePosicaoAleatorias = 4;


    void Start()
    {
        Jogador = GameObject.FindWithTag(Tags.Jogador);
        animacaoInimigo = GetComponent<AnimacaoPersonagem>();
        movimentoInimigo = GetComponent<MovimentoPersonagem>();
        statusInimigo = GetComponent<Status>(); 
        AleatorizarZumbis();
    }

    void FixedUpdate()
    {
        float distancia = Vector3.Distance(transform.position, Jogador.transform.position);

        movimentoInimigo.Rotacionar(direcao);
        animacaoInimigo.Movimentar(direcao.magnitude);

        if(distancia > 15)
        {
            Vagar();

        }
        else if (distancia > 2.5)
        {
            direcao = Jogador.transform.position - transform.position;
            movimentoInimigo.Movimentar(direcao, statusInimigo.Velocidade);
            animacaoInimigo.Atacar(false);
        }
        else
        {
            direcao = Jogador.transform.position - transform.position;
            animacaoInimigo.Atacar(true);
        }
    }

    void Vagar()
    {
        contadorVagar -= Time.deltaTime;
        if(contadorVagar < 0)
        {
            posicaoAleatoria = AleatorizarPosicao();
            contadorVagar += tempoEntrePosicaoAleatorias;
        }

        bool ficouPertoOSuficiente = Vector3.Distance(transform.position, posicaoAleatoria) <= 0.05;

        if (ficouPertoOSuficiente == false)
        {
            direcao = posicaoAleatoria - transform.position;
            movimentoInimigo.Movimentar(direcao, statusInimigo.Velocidade);
        }
    }
    
    Vector3 AleatorizarPosicao()
    {
        Vector3 posicao = Random.insideUnitSphere * 10;
        posicao += transform.position;
        posicao.y = transform.position.y;

        return posicao;
    }

    void AtacaJogador()
    {
        int dano = Random.Range(20, 30);
        Jogador.GetComponent<ControlaJogador>().TomarDano(dano);
    }

    void AleatorizarZumbis()
    {
        int geraTipoZumbi = Random.Range(1, 28);
        transform.GetChild(geraTipoZumbi).gameObject.SetActive(true);
    }

    public void TomarDano(int dano)
    {
        statusInimigo.Vida -= dano;
        if(statusInimigo.Vida <= 0)
        {
            Morrer();
        }
    }

    public void Morrer()
    {
        Destroy(gameObject);
        ControlaAudio.instancia.PlayOneShot(SomMorte);
    }
}
