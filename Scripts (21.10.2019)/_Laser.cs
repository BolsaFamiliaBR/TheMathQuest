using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class _Laser : MonoBehaviour
{
    Animator anim;
    public TextMeshPro txt;
    public GameObject particle;
    public Player player;
    float timer;
    float cooldownPadrao;

    void Start()
    {
        anim = GetComponent<Animator>();
        particle.SetActive(false);
        txt.text = "";
        if (_SavePoints.Jogo_Dificuldade == 1) { timer = Random.Range(15, 30); cooldownPadrao = 15; }
        if (_SavePoints.Jogo_Dificuldade == 2) { timer = Random.Range(10, 20); cooldownPadrao = 10; }
        if (_SavePoints.Jogo_Dificuldade == 3) { timer = Random.Range(1, 5); cooldownPadrao = 3; }
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            anim.SetTrigger("Fire");
            timer = cooldownPadrao;
            txt.text = "";
            particle.SetActive(false);
        }
        if(timer < 3) { txt.text = "3..."; particle.SetActive(true); }
        if(timer < 2) { txt.text = "2..."; }
        if(timer < 1) { txt.text = "1..."; }
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "Player")
        {
            StartCoroutine(player.LevarDano(transform.position.x));
        }
    }
}
