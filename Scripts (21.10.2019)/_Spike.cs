using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _Spike : MonoBehaviour
{
    public Player player;
    int x = 0;
    int limite;
    private void Start()
    {
        if (_SavePoints.Jogo_Dificuldade == 1) { limite = 10; }
        if (_SavePoints.Jogo_Dificuldade == 2) { limite = 3; }
        if (_SavePoints.Jogo_Dificuldade == 3) { limite = 1; }
    }
    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.name == "Player") {
            if (x == 0 || x > limite || gameObject.name == "SpikesGround") {
                StartCoroutine(player.LevarDano(transform.position.x));
            } else {
                if (transform.position.x > player.transform.position.x) {
                    StartCoroutine(player.LevarDano(10000));
                } else {
                    StartCoroutine(player.LevarDano(-10000));
                }
            }
            x++;
        }
    }
}
