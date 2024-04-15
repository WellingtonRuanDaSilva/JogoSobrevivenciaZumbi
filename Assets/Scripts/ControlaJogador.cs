using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ControlaJogador : MonoBehaviour, IMatavel, ICuravel
{

    private Vector3 direcao;
    public LayerMask MascaraChao;
    public GameObject TextoGameOver;
    public ControlaInterface scriptControlaInterface;
    public AudioClip SomDano;
    private MovimentoJogador meuMovimentoJogador;
    public AnimacaoPersonagem animacaoJogador;
    public Status statusJogador;

    private void Start()
    {
        meuMovimentoJogador = GetComponent<MovimentoJogador>();
        animacaoJogador = GetComponent<AnimacaoPersonagem>();
        statusJogador = GetComponent<Status>();
    }

    private void Update()
    {
        float eixoX = Input.GetAxis("Horizontal");
        float eixoZ = Input.GetAxis("Vertical");

        direcao = new Vector3(eixoX, 0, eixoZ);

        animacaoJogador.Movimentar(direcao.magnitude);

    }

    void FixedUpdate()
    {
        meuMovimentoJogador.Movimentar(direcao, statusJogador.Velocidade);
        
        meuMovimentoJogador.RotacaoJogador(MascaraChao);

    }

    public void TomarDano(int dano)
    {
        statusJogador.Vida -= dano;
        scriptControlaInterface.AtualizarSlideVidaJogador();
        ControlaAudio.instancia.PlayOneShot(SomDano);

        if (statusJogador.Vida <= 0) 
        {
            Morrer();
        }
    }

    public void Morrer()
    {
        scriptControlaInterface.GameOver();
    }

    public void CurarVida (int quantidadeDeCura)
    {
        statusJogador.Vida += quantidadeDeCura;
        if(statusJogador.Vida > statusJogador.VidaInicial)
        {
            statusJogador.Vida = statusJogador.VidaInicial;
        }
        scriptControlaInterface.AtualizarSlideVidaJogador();
    }
}
