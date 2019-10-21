using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class _FinalScreen : MonoBehaviour
{
    public TextMeshPro pontuação;
    public TextMeshPro tentativas;
    public TextMeshPro danoSofrido;
    public TextMeshPro inimigosAbatidos;
    public TextMeshPro tempoTotal;
    public TextMeshPro precisão;
    int pontuação_;
    int tentativas_;
    int danoSofrido_;
    int inimigosAbatidos_;
    float tempoTotal_;
    float precisão_;
    int acertos;
    int erros;
    float precisao;

    void Start()
    {
        acertos = _SavePoints.acertos;
        erros = _SavePoints.erros;
        precisao = (100 * acertos) / (acertos + erros);
        precisão.text = precisao.ToString() + "%";
        StartCoroutine("AttPontuação");
        StartCoroutine("AttTentativas");
        StartCoroutine("AttDanoSofrido");
        StartCoroutine("AttInimigosAbatidos");
        StartCoroutine("AttTempoTotal");
    }

    IEnumerator AttPontuação()
    {
        for (int i = 0; pontuação_ < _SavePoints.pontosTotal; i++)
        {
            for (int x = 0; x < 30; x++)
            {
                pontuação_++;
                pontuação.text = pontuação_.ToString();
                if (pontuação_ == _SavePoints.pontosTotal) { break; }
            }
            yield return new WaitForSeconds(0.00001f);
            if (pontuação_ >= _SavePoints.pontosTotal) { break; }
        }
    }
    IEnumerator AttTentativas()
    {
        for (int i = 0; tentativas_ < _SavePoints.mortes; i++)
        {
            tentativas_++;
            tentativas.text = tentativas_.ToString();
            yield return new WaitForSeconds(0.01f);
            if (tentativas_ == _SavePoints.mortes) { break; }
        }
    }
    IEnumerator AttDanoSofrido()
    {
        for (int i = 0; danoSofrido_ < _SavePoints.danosofrido; i++)
        {
            danoSofrido_++;
            danoSofrido.text = danoSofrido_.ToString();
            yield return new WaitForSeconds(0.01f);
            if (danoSofrido_ == _SavePoints.danosofrido) { break; }
        }
    }
    IEnumerator AttInimigosAbatidos()
    {
        for (int i = 0; inimigosAbatidos_ < _SavePoints.inimigosMortos; i++)
        {
            inimigosAbatidos_++;
            inimigosAbatidos.text = inimigosAbatidos_.ToString();
            yield return new WaitForSeconds(0.01f);
            if (inimigosAbatidos_ == _SavePoints.inimigosMortos) { break; }
        }
    }
    IEnumerator AttTempoTotal()
    {
        for (int i = 0; tempoTotal_ < _SavePoints.tempo; i++)
        {
            for (int x = 0; x < 10; x++)
            {
                tempoTotal_++;
                if (tempoTotal_ == _SavePoints.tempo) { break; }
                UpdateTimer();
            }
            yield return new WaitForSeconds(0.00001f);
            if (tempoTotal_ >= _SavePoints.tempo) { break; }
        }
    }
    void UpdateTimer()
    {
        float minutos = 0;
        float x = tempoTotal_;
        string txt;
        minutos = Mathf.Floor(tempoTotal_ / 60);
        x = Mathf.Floor(x % 60);
        if (x < 10)
        {
            if (minutos < 10)
            {
                txt = "0" + minutos + ":0" + x;
            }
            else
            {
                txt = minutos + ":0" + x;
            }
        }
        else
        {
            if (minutos < 10)
            {
                txt = "0" + minutos + ":" + x;
            }
            else
            {
                txt = minutos + ":" + x;
            }
        }
        tempoTotal.text = txt;
    }
}
