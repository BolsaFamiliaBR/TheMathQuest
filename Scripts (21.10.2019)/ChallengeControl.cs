using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChallengeControl : MonoBehaviour
{
    Animator anim;
    public CameraScript cam;
    public Player player;
    public GameMaster gm;
    public GameObject DestruirParede;
    public GameObject ApareceAe;
    bool PerguntaFeita = false;
    bool stop = false;
    bool end = false;
    int resposta;
    int points;
    int AlternativaCerta;
    public ParticleSystem fire1;
    public ParticleSystem fire2;
    public ParticleSystem fire3;
    public ParticleSystem fire4;
    public ParticleSystem particle1;
    public ParticleSystem particle2;
    public ParticleSystem particle3;
    public ParticleSystem particle4;
    public ParticleSystem particle5;
    public SpriteRenderer point1;
    public SpriteRenderer point2;
    public SpriteRenderer point3;
    public SpriteRenderer point4;
    public SpriteRenderer point5;
    public GameObject options;
    public TextMeshProUGUI opA;
    public TextMeshProUGUI opB;
    public TextMeshProUGUI opC;
    public TextMeshPro Timer;
    public TextMeshPro StartTXT;
    float timer;
    void Start()
    {
        anim = GetComponent<Animator>();
        options.SetActive(false);
        ApareceAe.SetActive(false);
    }
    private void Update()
    {
        if (stop == true && end == false)
        {
            timer -= Time.deltaTime;
            if (PerguntaFeita == true)
            {
                if (timer > 5) { Timer.color = Color.green; }
                if (timer < 5 && timer > 3) { Timer.color = Color.yellow; }
                if (timer < 3) { Timer.color = Color.red; }
                if (timer < 0)
                {
                    StartTXT.text = "O Tempo acabou :(";
                    Acertou(false);
                }
                else
                {
                    Timer.text = timer.ToString();
                }
            }
            else
            {
                Timer.color = Color.white;
                Timer.text = "0";
            }
            if (_SavePoints.morto == true) { options.SetActive(false); }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if ((Input.GetKeyDown("e") || Input.GetKeyDown(KeyCode.Return)) && stop == false)
        {
            StartCoroutine("starting");
            stop = true;
        }
    }

    IEnumerator starting()
    {
        StartTXT.text = "Prepare-se, o desafio ja vai começar";
        yield return new WaitForSeconds(3);
        StartTXT.color = Color.red;
        StartTXT.text = "3";
        yield return new WaitForSeconds(1);
        StartTXT.text = "2";
        yield return new WaitForSeconds(1);
        StartTXT.text = "1";
        yield return new WaitForSeconds(1);
        StartTXT.color = Color.white;
        newQuestion();
    }

    void newQuestion()
    {
        int num1 = 0, num2 = 0;
        switch(Random.Range(1, 5)){
            case 1:
                // Divisão
                int i = 0;
                while (i == 0)
                {
                    num1 = Random.Range(10, 30);
                    num2 = Random.Range(2, 5);
                    if(num1 % num2 == 0) { i++; }
                }
                StartTXT.text = num1 + " Dividido por " + num2;
                resposta = num1 / num2;
                break;
            case 2:
                // Multiplicação
                num1 = Random.Range(2, 10);
                num2 = Random.Range(2, 10);
                StartTXT.text = num1 + " Vezes " + num2;
                resposta = num1 * num2;
                break;
            case 3:
                // Adição
                num1 = Random.Range(10, 100);
                num2 = Random.Range(10, 100);
                StartTXT.text = num1 + " Mais " + num2;
                resposta = num1 + num2;
                break;
            case 4:
                //Subtração
                int i2 = 0;
                while (i2 == 0)
                {
                    num1 = Random.Range(10, 100);
                    num2 = Random.Range(10, 100);
                    if (num1 > num2) { i2++; }
                }
                StartTXT.text = num1 + " Menos " + num2;
                resposta = num1 - num2;
                break;
        }
        AlternativaCerta = Random.Range(1, 4);
        int respostaErrada1 = resposta + Random.Range(-10, 10);
        int respostaErrada2 = resposta + Random.Range(-10, 10);
        if (respostaErrada1 == respostaErrada2) { respostaErrada2++; }
        if (resposta == respostaErrada1) { respostaErrada1++; }
        if (resposta == respostaErrada2) { respostaErrada2++; }
        switch (AlternativaCerta)
        {
            case 1:
                opA.text = resposta.ToString();
                opB.text = respostaErrada1.ToString();
                opC.text = respostaErrada2.ToString();
                break;
            case 2:
                opA.text = respostaErrada1.ToString();
                opB.text = resposta.ToString();
                opC.text = respostaErrada2.ToString();
                break;
            case 3:
                opA.text = respostaErrada2.ToString();
                opB.text = respostaErrada1.ToString();
                opC.text = resposta.ToString();
                break;
        }
        PerguntaFeita = true;
        timer = 10;
        options.SetActive(true); 
    }

    public void buttonA()
    {
        if (AlternativaCerta == 1 && _SavePoints.morto == false) { Acertou(true); _SavePoints.acertos++; } else { Acertou(false); _SavePoints.erros++; }
    }
    public void buttonB()
    {
        if (AlternativaCerta == 2 && _SavePoints.morto == false) { Acertou(true); _SavePoints.acertos++; } else { Acertou(false); _SavePoints.erros++; }
    }
    public void buttonC()
    {
        if (AlternativaCerta == 3 && _SavePoints.morto == false) { Acertou(true); _SavePoints.acertos++; } else { Acertou(false); _SavePoints.erros++; }
    }
    void Acertou(bool x)
    {
        PerguntaFeita = false;
        options.SetActive(false);
        if(x == true)
        {
            StartTXT.text = "Você acertou!";
            points++;
            switch (points)
            {
                case 0:
                    point1.color = Color.white;
                    point2.color = Color.white;
                    point3.color = Color.white;
                    point4.color = Color.white;
                    point5.color = Color.white;
                    break;
                case 1:
                    point1.color = Color.green;
                    point2.color = Color.white;
                    point3.color = Color.white;
                    point4.color = Color.white;
                    point5.color = Color.white;
                    particle1.Play();
                    break;
                case 2:
                    point1.color = Color.green;
                    point2.color = Color.green;
                    point3.color = Color.white;
                    point4.color = Color.white;
                    point5.color = Color.white;
                    particle2.Play();
                    break;
                case 3:
                    point1.color = Color.green;
                    point2.color = Color.green;
                    point3.color = Color.green;
                    point4.color = Color.white;
                    point5.color = Color.white;
                    particle3.Play();
                    break;
                case 4:
                    point1.color = Color.green;
                    point2.color = Color.green;
                    point3.color = Color.green;
                    point4.color = Color.green;
                    point5.color = Color.white;
                    particle4.Play();
                    break;
                case 5:
                    point1.color = Color.green;
                    point2.color = Color.green;
                    point3.color = Color.green;
                    point4.color = Color.green;
                    point5.color = Color.green;
                    particle5.Play();
                    StartCoroutine("Ganhou");
                    break;
            }
            StartCoroutine("score");
            StartCoroutine(cam.Shake(.50f, .15f));

        } else
        {
            Debug.Log("A");
            StartCoroutine(player.LevarDano(-1));
            if(timer < 0) { StartTXT.text = "O tempo acabou :("; } else { StartTXT.text = "Você errou :(";  }
        }
        if (points != 5) { newQuestion(); }
    }

    IEnumerator score()
    {
        gm.getTotalScore(200);
        for (int i = 0; i < 100; i++)
        {
            gm.getScore();
            gm.getScore();
            yield return new WaitForSeconds(0.001f);
        }
    }

    IEnumerator Ganhou()
    {
        end = true;
        yield return new WaitForSeconds(1);
        StartTXT.color = Color.green;
        StartTXT.text = "Desafio concluido!";
        fire1.Play(); fire2.Play(); fire3.Play(); fire4.Play();
        yield return new WaitForSeconds(0.7f);
        StartCoroutine(cam.Shake(.50f, .15f));
        yield return new WaitForSeconds(1);
        StartTXT.text = ""; Timer.text = "";
        anim.SetTrigger("Exit");
        yield return new WaitForSeconds(2);
        Destroy(DestruirParede);
        ApareceAe.SetActive(true);
    }
}
