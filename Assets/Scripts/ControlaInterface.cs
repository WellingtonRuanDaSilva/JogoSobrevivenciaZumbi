using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlaInterface : MonoBehaviour
{

    private ControlaJogador scriptControlaJogador;
    public Slider SliderVidaJogador;

    // Start is called before the first frame update
    void Start()
    {
        scriptControlaJogador = GameObject.FindWithTag("Jogador").GetComponent<ControlaJogador>();

        SliderVidaJogador.maxValue = scriptControlaJogador.Vida;
        AtualizarSlideVidaJogador();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AtualizarSlideVidaJogador()
    {
        SliderVidaJogador.value = scriptControlaJogador.Vida;
    }
}