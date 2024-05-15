using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class ControlaInterface : MonoBehaviour
{

    private ControlaJogador scriptControlaJogador;
    public Slider SliderVidaJogador;
    public GameObject PainelGameOver;
    public TMP_Text TextoTempoSobrevivencia;
    public TMP_Text TextoPontuacaoMaxima;
    private float tempoPontuacaoSalvo;
    private int quantidadeZumbisMortos;
    public TMP_Text TextoQuantidadeZumbisMortos;
    public TMP_Text TextoChefeAparece;

    // Use this for initialization
    void Start()
    {
        scriptControlaJogador = GameObject.FindWithTag("Jogador").GetComponent<ControlaJogador>();

        SliderVidaJogador.maxValue = scriptControlaJogador.statusJogador.Vida;
        AtualizarSlideVidaJogador();

        Time.timeScale = 1;
        tempoPontuacaoSalvo = PlayerPrefs.GetFloat("PontuacaoMaxima");
    }

    public void AtualizarSlideVidaJogador()
    {
        SliderVidaJogador.value = scriptControlaJogador.statusJogador.Vida;
    }

    public void AtualiarQuatidadeZumbisMortos()
    {
        quantidadeZumbisMortos++;
        TextoQuantidadeZumbisMortos.text = string.Format("x {0}", quantidadeZumbisMortos);
    }

    public void GameOver()
    {
        PainelGameOver.SetActive(true);
        Time.timeScale = 0;

        int minutos = (int)(Time.timeSinceLevelLoad / 60);
        int segundos = (int)(Time.timeSinceLevelLoad % 60);
        TextoTempoSobrevivencia.text = "Você sobreviveu por " + minutos + "min e " + segundos + "s";

        AjustarPontuacaoMaximo(minutos, segundos);
    }
    
    void AjustarPontuacaoMaximo(int min, int seg)
    {
        if(Time.timeSinceLevelLoad > tempoPontuacaoSalvo)
        {
            tempoPontuacaoSalvo = Time.timeSinceLevelLoad;
            TextoPontuacaoMaxima.text = string.Format("Seu melhor tempo: {0}min e {1}s", min, seg);
            PlayerPrefs.SetFloat("PontuacaoMaxima", tempoPontuacaoSalvo);
        }
        if(TextoPontuacaoMaxima.text == "")
        {
            min = (int)tempoPontuacaoSalvo / 60;
            seg = (int)tempoPontuacaoSalvo % 60;
            tempoPontuacaoSalvo = Time.timeSinceLevelLoad;
            TextoPontuacaoMaxima.text = string.Format("Seu melhor tempo: {0}min e {1}s", min, seg);
        }
    }
    public void Reiniciar()
    {
        SceneManager.LoadScene("game");
    }

    public void AparecerTextoChefeCriado()
    {
        StartCoroutine(DesaparecerTexto(2, TextoChefeAparece));
    }

    IEnumerator DesaparecerTexto(float tempoDeSumico, TMP_Text textoParaSumir)
    {
        textoParaSumir.gameObject.SetActive(true);
        Color corTexto = textoParaSumir.color;
        corTexto.a = 1;
        textoParaSumir.color = corTexto;
        yield return new WaitForSeconds(1);
        float contador = 0;
        while (textoParaSumir.color.a > 0)
        {
            contador += Time.deltaTime / tempoDeSumico;
            corTexto.a = Mathf.Lerp(1, 0, contador);
            textoParaSumir.color = corTexto;
            if(textoParaSumir.color.a <= 0)
            {
                textoParaSumir.gameObject.SetActive(false);
            }
            yield return null;
        }
    }
}
