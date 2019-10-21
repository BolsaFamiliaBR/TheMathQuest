using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameMaster : MonoBehaviour
{
    GameObject target;
    ParticleSystem p1of3;
    ParticleSystem p2of3;
    ParticleSystem p3of3;
    ParticleSystem b1of3;
    ParticleSystem b2of3;
    ParticleSystem b3of3;
    ParticleSystem e1of1;
    ParticleSystem particle1000;
    public Image ExtraHearth;
    public Image Hearth1of3;
    public Image Hearth2of3;
    public Image Hearth3of3;
    public GameObject blood_1of3;
    public GameObject blood_2of3;
    public GameObject blood_3of3;
    public GameObject plus_1of3;
    public GameObject plus_2of3;
    public GameObject plus_3of3;
    public GameObject HUD_Death;
    public GameObject pontosParticulas1k;
    public GameObject TelaConfirmarResposta;
    public GameObject extra_1of1;
    public Button HUD_Continue;
    public Text FPS;
    public Text ExtraLifes;
    public Text HUD_TxtMorte;   
    public Text pontos;
    public Text pontosGanhos;
    public Text pontosGanhos2;
    public Text Txt_Fase;
    public Text Txt_Tempo;
    public TextMeshPro question;
    public TextMeshPro RespostaA;
    public TextMeshPro RespostaB;
    public TextMeshPro RespostaC;
    public TextMeshPro ConfirmarResposta;
    public TextMeshProUGUI ConfirmarRespostaHUD;
    public Animator ConfirmationScreen;
    public Animator FinalScreen;
    public Animator pontosGanhosAnim2;
    public Animator pontosGanhosAnim;
    public ParticleSystem pontosParticulas;
    public ParticleSystem Fireworks1;
    public ParticleSystem Fireworks2;
    public ParticleSystem Fireworks3;
    public ParticleSystem Fireworks4;
    public Player player;
    public CameraScript cam;
    public EdgeCollider2D Capsule;
    public float limiteMaximoNuvem;
    public float inicioDaNuvem;
    float timer_death;
    bool isInside = false;
    bool isDead;
    bool exploded;
    bool respostaRecebida;
    bool particlesRemoved = false;
    bool win = false;
    bool errou = false;
    string x;
    public int pontuação = 0;
    float tempo;
    float fps;
    int RespostaRecebidaInt;
    int respostaFinal;
    int num1;
    int respostaCerta;
    int num2;
    int opção;

    void Start()
    {
        e1of1 = extra_1of1.GetComponent<ParticleSystem>();
        b1of3 = blood_1of3.GetComponent<ParticleSystem>();
        b2of3 = blood_2of3.GetComponent<ParticleSystem>();
        b3of3 = blood_3of3.GetComponent<ParticleSystem>();
        p1of3 = plus_1of3.GetComponent<ParticleSystem>();
        p2of3 = plus_2of3.GetComponent<ParticleSystem>();
        p3of3 = plus_3of3.GetComponent<ParticleSystem>();
        particle1000 = pontosParticulas1k.GetComponent<ParticleSystem>();
        TelaConfirmarResposta.SetActive(false);
        pontosParticulas1k.SetActive(false);
        blood_1of3.SetActive(false);
        blood_2of3.SetActive(false);
        blood_3of3.SetActive(false);
        plus_1of3.SetActive(false);
        plus_2of3.SetActive(false);
        plus_3of3.SetActive(false);
        extra_1of1.SetActive(false);
        HUD_Death.SetActive(false);
        Fireworks1.Pause();
        Fireworks2.Pause();
        Fireworks3.Pause();
        Fireworks4.Pause();
        HUD_Continue.interactable = false;
        ExtraLifes.text = _SavePoints.vidasExtras.ToString();
        if (_SavePoints.vidasExtras > 0) { ExtraHearth.color = Color.white; } else { ExtraHearth.color = Color.black; }
        tempo = _SavePoints.tempo;
        pontuação = _SavePoints.pontosTotal;
        if (_SavePoints.nivel >= 10) { Txt_Fase.text = _SavePoints.nivel.ToString(); }
        else { Txt_Fase.text = "0" + _SavePoints.nivel.ToString(); }
        updateScore();
    }

    // Update is called once per frame
    void Update()
    {
        if (particlesRemoved == false && _SavePoints.low == true)
        {
            if (GameObject.FindGameObjectWithTag("Particles_Leafs"))
            {
                Destroy(GameObject.FindGameObjectWithTag("Particles_Leafs"));
            }
            else
            {
                particlesRemoved = true;
            }
        }
        fps = Time.frameCount / Time.time;
        FPS.text = fps.ToString();
        tempo += Time.deltaTime;
        UpdateTimer();
        blood_1of3.transform.position = new Vector3(Hearth1of3.transform.position.x + 5.5f, Hearth1of3.transform.position.y - 5f, Hearth1of3.transform.position.z);
        blood_2of3.transform.position = new Vector3(Hearth2of3.transform.position.x + 5.5f, Hearth2of3.transform.position.y - 5f, Hearth2of3.transform.position.z);
        blood_3of3.transform.position = new Vector3(Hearth3of3.transform.position.x + 5.5f, Hearth3of3.transform.position.y - 5f, Hearth3of3.transform.position.z);
        plus_1of3.transform.position = new Vector3(Hearth1of3.transform.position.x + 5.5f, Hearth1of3.transform.position.y - 5f, Hearth1of3.transform.position.z);
        plus_2of3.transform.position = new Vector3(Hearth2of3.transform.position.x + 5.5f, Hearth2of3.transform.position.y - 5f, Hearth2of3.transform.position.z);
        plus_3of3.transform.position = new Vector3(Hearth3of3.transform.position.x + 5.5f, Hearth3of3.transform.position.y - 5f, Hearth3of3.transform.position.z);
        extra_1of1.transform.position = new Vector3(ExtraHearth.transform.position.x + 5.5f, ExtraHearth.transform.position.y - 5f, ExtraHearth.transform.position.z);
        if(timer_death > 0) { timer_death -= Time.deltaTime; }
        if(timer_death <= 0 && isDead == true)
        {
            HUD_Death.SetActive(true);
            HUD_Continue.interactable = true;
        }
    }

    public void UpdateHearths(int x, bool y)
    {
        ExtraLifes.text = _SavePoints.vidasExtras.ToString();
        if (_SavePoints.vidasExtras > 0) { ExtraHearth.color = Color.white; } else { ExtraHearth.color = Color.black; }
        switch (x)
        {
            case 0:
                Hearth1of3.color = Color.black;
                Hearth2of3.color = Color.black;
                Hearth3of3.color = Color.black;
                if(y == true)
                {
                    blood_1of3.SetActive(true);
                    b1of3.Play();
                }
                break;
            case 1:
                Hearth1of3.color = Color.white;
                Hearth2of3.color = Color.black;
                Hearth3of3.color = Color.black;
                if (y == true)
                {
                    blood_2of3.SetActive(true);
                    b2of3.Play();
                } else {
                    plus_1of3.SetActive(true);
                    p1of3.Play();
                }
                break;
            case 2:
                Hearth1of3.color = Color.white;
                Hearth2of3.color = Color.white;
                Hearth3of3.color = Color.black;
                if (y == true)
                {
                    blood_3of3.SetActive(true);
                    b3of3.Play();
                } else {
                    plus_2of3.SetActive(true);
                    p2of3.Play();
                }
                break;
            case 3:
                Hearth1of3.color = Color.white;
                Hearth2of3.color = Color.white;
                Hearth3of3.color = Color.white;
                if (y == false)
                {
                    plus_3of3.SetActive(true);
                    p3of3.Play();
                }
                break;
        }
        Invoke("DesactiveParticles", 0.5f);

    }
    public void RestartGame()
    {
        _SavePoints.tempo = tempo;
        _SavePoints.pontosTotal = pontuação;
        if(win == true) 
        {
            _SavePoints.nivel++;
            switch (_SavePoints.nivel)
            {
                case 0: SceneManager.LoadScene("CenaTutorial"); break;
                case 1: SceneManager.LoadScene("Cena1"); break;
                case 2: SceneManager.LoadScene("Cena2"); break;
                case 3: SceneManager.LoadScene("Cena3"); break;
                case 4: SceneManager.LoadScene("Cena4"); break;
                case 5: SceneManager.LoadScene("Cena5"); break;
                case 6: SceneManager.LoadScene("End_Scene"); break;
            }
        }
        else { SceneManager.LoadScene(SceneManager.GetActiveScene().name); }
    }
    public void playerDeath()
    {
        if(errou == true)
        {
            HUD_TxtMorte.text = "Você errou a pergunta, \nAperte o botão continuar para tentar de novo, você consegue!";
            _SavePoints.mortes++;
        }
        else if(win == true) {
            if(SceneManager.GetActiveScene().name == "CenaTutorial") {
                HUD_TxtMorte.text = "Você passou do tutorial! \nAperte o botão continuar para iniciar o jogo";
            } else {
                HUD_TxtMorte.text = "Boa! Você passou de fase, \nAperte o botão continuar para avançar";
            }
        }
        else {
            HUD_TxtMorte.text = "Você ficou sem vidas! \nAperte o botão continuar para tentar de novo";
            _SavePoints.mortes++;
        }
        timer_death = 3;
        isDead = true;
    }
    public void DesactiveParticles()
    {
        blood_1of3.SetActive(false);
        blood_2of3.SetActive(false);
        blood_3of3.SetActive(false);
        plus_1of3.SetActive(false);
        plus_2of3.SetActive(false);
        plus_3of3.SetActive(false);
        extra_1of1.SetActive(false);
        HUD_Death.SetActive(false);
    }
    public float getLimit() { return limiteMaximoNuvem; }
    public float getLimit2() { return inicioDaNuvem; }
    public void getTotalScore(int x) 
    {
        if (x > 0)
        {
            pontosGanhos.color = Color.green;
            pontosGanhos.text = "+" + x.ToString();
            pontosGanhosAnim.SetTrigger("Ganhei");
        } else
        {
            int y = x * -1;
            pontosGanhos2.color = Color.red;
            pontosGanhos2.text = "-" + y.ToString();
            pontosGanhosAnim2.SetTrigger("Perdi");
        }
    }
    public void getScore()
    {
        pontuação += 1;
        updateScore();
    }
    public void getScore2()
    {
        pontuação -= 1;
        updateScore();
    }
    public void updateScore()
    {
        if(pontuação >= 1000 * _SavePoints.ContagemVidasExtras)
        {
            _SavePoints.ContagemVidasExtras++;
            _SavePoints.vidasExtras++;
            extra_1of1.SetActive(true);
            e1of1.Play();
            Invoke("DesactiveParticles", 0.5f);
            ExtraLifes.text = _SavePoints.vidasExtras.ToString();
            if(_SavePoints.vidasExtras > 0) { ExtraHearth.color = Color.white; }
        }
        if (pontuação >= 0)
        {
            if (pontuação < 10) { x = "00000" + pontuação.ToString(); }
            else if (pontuação >= 10 && pontuação < 100) { x = "0000" + pontuação.ToString(); }
            else if (pontuação >= 100 && pontuação < 1000) { x = "000" + pontuação.ToString(); }
            else if (pontuação >= 1000 && pontuação < 10000) { x = "00" + pontuação.ToString(); }
            else if (pontuação >= 10000 && pontuação < 100000) { x = "0" + pontuação.ToString(); }
            else { x = pontuação.ToString(); }
        } else
        {
            int y = pontuação * -1;
            if (y < 10) { x = "-00000" + y.ToString(); }
            else if (y >= 10 && y < 100) { x = "-0000" + y.ToString(); }
            else if (y >= 100 && y < 1000) { x = "-000" + y.ToString(); }
            else if (y >= 1000 && y < 10000) { x = "-00" + y.ToString(); }
            else if (y >= 10000 && y < 100000) { x = "-0" + y.ToString(); }
            else { x = pontuação.ToString(); }
        }
        pontos.text = x;
        var main = pontosParticulas.main;
        if (pontuação < 0) { main.startColor = new Color(255, 0, 0, 1); }
        else if (pontuação >= 0 && pontuação < 1000) { main.startColor = new Color(0, 0, 0, 0); exploded = false; }
        else if (pontuação >= 1000)
        {
            main.startColor = Color.green;
            if(exploded == false)
            {
                pontosParticulas1k.SetActive(true);
                particle1000.Play();
                exploded = true;
            }
        }
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (isInside == false && col.gameObject.name == "Player")
        {
            isInside = true;
            FinalScreen.SetTrigger("Ativar");
            Capsule.isTrigger = false;
            cam.endgame();
            opção = Random.Range(1, 5);
            switch (opção)
            {
                case 1: // Multiplicação
                    if (_SavePoints.Matematica_Dificuldade == 1) // Facil
                    {
                        num1 = Random.Range(1, 15);
                        num2 = Random.Range(1, 10);
                    }
                    if (_SavePoints.Matematica_Dificuldade == 2) // Medio
                    {
                        num1 = Random.Range(20, 100);
                        num2 = Random.Range(2, 10);
                    }
                    if (_SavePoints.Matematica_Dificuldade == 3) // Dificil
                    {
                        num1 = Random.Range(30, 200);
                        num2 = Random.Range(5, 20);
                    }
                    break;

                case 2: // Divisão
                    if (_SavePoints.Matematica_Dificuldade == 1) // Facil
                    {
                        int i2 = 0;
                        while (i2 == 0)
                        {
                            num1 = Random.Range(20, 50);
                            num2 = Random.Range(1, 5);
                            if (num1 % num2 == 0) { i2++; }
                        }
                    }
                    if (_SavePoints.Matematica_Dificuldade == 2) // Medio
                    {
                        int i2 = 0;
                        while (i2 == 0)
                        {
                            num1 = Random.Range(50, 200);
                            num2 = Random.Range(2, 10);
                            if (num1 % num2 == 0) { i2++; }
                        }
                    }
                    if (_SavePoints.Matematica_Dificuldade == 3) // Dificil
                    {
                        int i2 = 0;
                        while (i2 == 0)
                        {
                            num1 = Random.Range(50, 2000);
                            num2 = Random.Range(2, 200);
                            if (num1 % num2 == 0 && num1 > num2) { i2++; }
                        }
                    }
                    break;

                case 3: // Adição
                    if (_SavePoints.Matematica_Dificuldade == 1) // Facil
                    {
                        num1 = Random.Range(20, 500);
                        num2 = Random.Range(20, 500);
                    }
                    if (_SavePoints.Matematica_Dificuldade == 2) // Medio
                    {
                        num1 = Random.Range(100, 8000);
                        num2 = Random.Range(100, 8000);
                    }
                    if (_SavePoints.Matematica_Dificuldade == 3) // Dificil
                    {
                        num1 = Random.Range(100, 10000);
                        num2 = Random.Range(100, 10000);
                    }
                    break;

                case 4: // Subtração
                    if (_SavePoints.Matematica_Dificuldade == 1) // Facil
                    {
                        int i4 = 0;
                        while (i4 == 0)
                        {
                            num1 = Random.Range(20, 500);
                            num2 = Random.Range(20, 500);
                            if (num1 > num2) { i4++; }
                        }
                    }
                    if (_SavePoints.Matematica_Dificuldade == 2) // Medio
                    {
                        int i4 = 0;
                        while (i4 == 0)
                        {
                            num1 = Random.Range(100, 3000);
                            num2 = Random.Range(100, 3000);
                            if (num1 > num2) { i4++; }
                        }
                    }
                    if (_SavePoints.Matematica_Dificuldade == 3) // Dificil
                    {
                        int i4 = 0;
                        while (i4 == 0)
                        {
                            num1 = Random.Range(100, 10000);
                            num2 = Random.Range(100, 10000);
                            if (num1 > num2) { i4++; }
                        }
                    }
                    break;
            }
            quest();
        }
    }
    void quest()
    {
        if(SceneManager.GetActiveScene().name == "CenaTutorial") { opção = 0; num1 = 1; num2 = 1; }
        switch (opção)
        {
            // Tutorial
            case 0: question.text = "Quanto é 1 + 1?"; break;

            // Multiplicação
            case 1: int quantidade1 = 5;
                opção = Random.Range(1, quantidade1);
                switch (opção)
                {
                    case 1: question.text = "Em uma pista de corrida, uma volta completa tem " + num1 + " metros. Qual é a distância percorrida por um ciclista que deu " + num2 +" voltas na pista?"; break;
                    case 2: question.text = "Uma escola comprou " + num2 + " livros por " + num1 + " reais cada um, quantos reais a escola gastou em todos os livros?"; break;
                    case 3: question.text = "Para a apresentação de um teatro foram vendidos " + num1 + " ingressos a " + num2 + " reais cada um. Qual foi o total arrecadado?"; break;
                    case 4: question.text = "Os alunos matriculados no 4º ano de uma escola foram divididos em " + num2 + " turmas, cada turma ficou com " + num1 + " alunos, qual é o total de alunos do 4º ano matriculados nessa escola?"; break;
                }
                respostaCerta = num1 * num2;
                break;    

            // Divisão
            case 2: int quantidade2 = 6;
                opção = Random.Range(1, quantidade2);
                switch (opção)
                {
                    case 1: question.text = "Joana quer dividir " + num1 + " livros em " + num2 + " prateleiras, quantos livros cada prateleira ira conter?"; break;
                    case 2: question.text = "Para realizar uma gincana, a organizadora irá dividir " + num1 + " pessoas em " + num2 + " grupos. Quantas pessoas ficarão em cada grupo?"; break;
                    case 3: question.text = "Pedro quer repartir " + num1 + " figurinhas entre " + num2 + " amigos. Cada amigo receberá o mesmo número de figurinhas. Quantas figurinhas ele dará a cada amigo?"; break;
                    case 4: question.text = "Maria andou " + num1 + "Km em " + num2 + " horas, quantos Km por hora a Maria andou?"; break;
                    case 5: question.text = "Lúcia comprou " + num2 + " calças do mesmo preço por \nR$ " + num1 + ",00 reais. Qual é o preço de cada calça?"; break;    
                }
                respostaCerta = num1 / num2;
                break;

            // Adição
            case 3: int quantidade3 = 6;
                opção = Random.Range(1, quantidade3);
                if(opção == 5) { num1 = Random.Range(20, 40); num2 = Random.Range(1, 20); }
                if(opção == 2) { num1 = Random.Range(50, 300); num2 = Random.Range(50, 300); }
                switch (opção)
                {
                    case 1: question.text = "O Zoológico de uma cidade foi visitado por " + num1 + " pessoas no sábado e por " + num2 + " pessoas no domingo. Quantas pessoas visitaram o zoológico nesse fim de semana?"; break;
                    case 2: question.text = "Num trem viajam " + num1 + " passageiros em pé e " + num2 + " sentados. Quantos passageiros há no trem?"; break;
                    case 3: question.text = "Em uma empresa, trabalham " + num1 + " pessoas durante o dia, e " + num2 + " trabalham durante a noite. Quantas pessoas ao todo trabalham nessa empresa?"; break;
                    case 4: question.text = "Uma farmácia vendeu " + num1 + " Caixas de remédio na segunda-feira, e mais " + num2 + " na terça-feira, quantas caixas foram vendidas nos dois dias?"; break;
                    case 5: question.text = "Quando Amanda nasceu sua mãe tinha " + num1 + " anos. Hoje Amanda está completando " + num2 + " anos. Quantos anos a mãe de Amanda tem?"; break;
                }
                respostaCerta = num1 + num2;
                break;

            // Subtração
            case 4: int quantidade4 = 5;
                opção = Random.Range(1, quantidade4);
                switch (opção)
                {
                    case 1: question.text = "Estavam assistindo a uma partida de futebol " + num1 + " pessoas. Se " + num2 + " dessas pessoas eram crianças, quantos adultos assistiam a essa partida de futebol?"; break;
                    case 2: question.text = "Um livro tem " + num1 + " paginas, Fernando já leu " + num2 + ". Quantas páginas ele ainda precisa ler para terminar seu livro?"; break;
                    case 3: question.text = "Uma escola tem " + num1 + " Alunos, sabemos que desse total, " + num2 + " são meninas. Quantos meninos estudam nessa escola?"; break;
                    case 4: question.text = "A soma de dois numeros é igual a " + num1 + ", sabendo que um desses numeros é " + num2 + ", qual é o outro numero? \n(Dica: Use a subtração)"; break;
                    
                }
                respostaCerta = num1 - num2;
                break; 
        }
        opção = Random.Range(1, 4);
        respostaFinal = opção;
        if(SceneManager.GetActiveScene().name == "CenaTutorial") { respostaCerta = num1 + num2; }
        int respostaErrada1 = respostaCerta + Random.Range(-5, 5);
        int respostaErrada2 = respostaCerta + Random.Range(-5, 5);
        if(respostaErrada1 == respostaErrada2) { respostaErrada2++; }
        if(respostaCerta == respostaErrada1) { respostaErrada1++; }
        if(respostaCerta == respostaErrada2) { respostaErrada2++; }
        switch (opção)
        {
            case 1:
                RespostaA.text = respostaCerta.ToString();
                RespostaB.text = respostaErrada1.ToString();
                RespostaC.text = respostaErrada2.ToString();
                break;
            case 2:
                RespostaA.text = respostaErrada1.ToString();
                RespostaB.text = respostaCerta.ToString();
                RespostaC.text = respostaErrada2.ToString();
                break;
            case 3:
                RespostaA.text = respostaErrada2.ToString();
                RespostaB.text = respostaErrada1.ToString();
                RespostaC.text = respostaCerta.ToString();
                break;
        }
    }
    public void receberResposta(string x)
    {
        if(respostaRecebida == false)
        {
            ConfirmarRespostaHUD.text = "Você escolheu a opção " + x.ToUpper() + " Você tem certeza?";
            TelaConfirmarResposta.SetActive(true);
            respostaRecebida = true;
            if (x == "A") { RespostaRecebidaInt = 1; }
            if (x == "B") { RespostaRecebidaInt = 2; }
            if (x == "C") { RespostaRecebidaInt = 3; }
        }
    }
    public void sim()
    {
        TelaConfirmarResposta.SetActive(false);
        FinalScreen.SetTrigger("Desativar");
        FinalScreen.ResetTrigger("Ativar");
        StartCoroutine("activateConfirmation");
        

    }
    public void nao()
    {
        respostaRecebida = false;
        TelaConfirmarResposta.SetActive(false);
    }
    IEnumerator activateConfirmation()
    {
        ConfirmarResposta.text = "Sua resposta esta...";
        yield return new WaitForSeconds(0.6f);
        ConfirmationScreen.SetTrigger("Ativar");
        yield return new WaitForSeconds(3);
        ConfirmarResposta.color = Color.white;
        ConfirmarResposta.text = "3";
        yield return new WaitForSeconds(1);
        ConfirmarResposta.color = Color.yellow;
        ConfirmarResposta.text = "2";
        yield return new WaitForSeconds(1);
        ConfirmarResposta.color = Color.red;
        ConfirmarResposta.text = "1";
        yield return new WaitForSeconds(1);
        if (RespostaRecebidaInt == respostaFinal)
        {
            _SavePoints.acertos++;
            win = true;
            ConfirmarResposta.color = Color.green;
            ConfirmarResposta.text = "Correta! =)";
            Fireworks1.Play();
            Fireworks2.Play();
            Fireworks3.Play();
            Fireworks4.Play();
            StartCoroutine("FireworkShake");
            int totalPoints = 0, repeat = 0;
            if (SceneManager.GetActiveScene().name == "CenaTutorial" || _SavePoints.Matematica_Dificuldade == 1) { totalPoints = 1000; repeat = 333; }
            else if (_SavePoints.Matematica_Dificuldade == 2) { totalPoints = 1250; repeat = 416; getScore(); }
            else if (_SavePoints.Matematica_Dificuldade == 3) { totalPoints = 1500; repeat = 500; getScore2(); }
            getTotalScore(totalPoints);
            for(int i = 0; i < repeat; i++)
            {
                getScore();
                getScore();
                getScore();
                yield return new WaitForSeconds(0.001f);
            }
            getScore();
                
        } else
        {
            _SavePoints.erros++;
            errou = true;
            ConfirmarResposta.color = Color.red;
            ConfirmarResposta.text = "Incorreta! -> Tente novamente!";
            getTotalScore(-500);
            for (int i = 0; i < 250; i++)
            {
                getScore2();
                getScore2();
                yield return new WaitForSeconds(0.001f);
            }
            player.Errou();
            playerDeath();
        }
        yield return new WaitForSeconds(3);
        ConfirmationScreen.SetTrigger("Desativar");
        ConfirmationScreen.ResetTrigger("Ativar");
    }
    IEnumerator FireworkShake()
    {
        yield return new WaitForSeconds(0.7f);
        StartCoroutine(cam.Shake(.3f, .20f));
        yield return new WaitForSeconds(2f);
        playerDeath();
    }
    void UpdateTimer()
    {
        float minutos = 0;
        float x = tempo;
        string txt;
        minutos = Mathf.Floor(tempo / 60);
        x = Mathf.Floor(x % 60);
        if(x < 10) {
            if (minutos < 10) {
                txt = "0" + minutos + ":0" + x;
            } else {
                txt = minutos + ":0" + x;
            }
        } else {
            if (minutos < 10) {
                txt = "0" + minutos + ":" + x;
            } else {
                txt = minutos + ":" + x;
            }
        }
        Txt_Tempo.text = txt;
    }
}
