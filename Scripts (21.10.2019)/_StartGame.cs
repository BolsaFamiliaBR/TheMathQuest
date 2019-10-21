using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class _StartGame : MonoBehaviour
{
    public Toggle jFacil;
    public Toggle jMedio;
    public Toggle jDificil;
    public Toggle mFacil;
    public Toggle mMedio;
    public Toggle mDificil;
    public Toggle lowq;
    public TextMeshProUGUI tjFacil;
    public TextMeshProUGUI tjMedio;
    public TextMeshProUGUI tjDificil;
    public TextMeshProUGUI tmFacil;
    public TextMeshProUGUI tmMedio;
    public TextMeshProUGUI tmDificil;
    public GameObject one;
    public GameObject two;
    public GameObject numbers;
    float x = 0;
    private void Start()
    {
        two.SetActive(false);
    }
    private void Update()
    {
        x += Time.deltaTime * 50;
        numbers.transform.rotation = Quaternion.Euler(0, 0, x);
        if (jFacil.isOn == true) { tjFacil.color = Color.green; } else { tjFacil.color = Color.white; }
        if (jMedio.isOn == true) { tjMedio.color = Color.yellow; } else { tjMedio.color = Color.white; }
        if (jDificil.isOn == true) { tjDificil.color = Color.red; } else { tjDificil.color = Color.white; }
        if (mFacil.isOn == true) { tmFacil.color = Color.green; } else { tmFacil.color = Color.white; }
        if (mMedio.isOn == true) { tmMedio.color = Color.yellow; } else { tmMedio.color = Color.white; }
        if (mDificil.isOn == true) { tmDificil.color = Color.red; } else { tmDificil.color = Color.white; }
    }
    public void startGame()
    {
        one.SetActive(false);
        two.SetActive(true);
    }
    public void startGame2()
    {
        if (lowq.isOn == true) { _SavePoints.low = true; } else { _SavePoints.low = false; }
        if (jFacil.isOn == true) { _SavePoints.Jogo_Dificuldade = 1; }
        if (jMedio.isOn == true) { _SavePoints.Jogo_Dificuldade = 2; }
        if (jDificil.isOn == true) { _SavePoints.Jogo_Dificuldade = 3; }
        if (mFacil.isOn == true) { _SavePoints.Matematica_Dificuldade = 1; }
        if (mMedio.isOn == true) { _SavePoints.Matematica_Dificuldade = 2; }
        if (mDificil.isOn == true) { _SavePoints.Matematica_Dificuldade = 3; }
        SceneManager.LoadScene("CenaTutorial");
    }
}
