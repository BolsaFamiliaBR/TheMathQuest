using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _SpawnProjectiles : MonoBehaviour
{
    public GameObject spike;
    public float x;
    public float yMax;
    public float yMin;
    float timer = 0;
    float cooldownPadrao;
    private void Start()
    {
        if(_SavePoints.Jogo_Dificuldade == 1) { cooldownPadrao = 10; }
        if(_SavePoints.Jogo_Dificuldade == 2) { cooldownPadrao = 5f; }
        if(_SavePoints.Jogo_Dificuldade == 3) { cooldownPadrao = 1f; }
    }
    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            float y = Random.Range(yMin, yMax);
            Vector2 spawn = new Vector2(x, y);
            Instantiate(spike, spawn, Quaternion.identity);
            timer = cooldownPadrao;
        }
    }
}
