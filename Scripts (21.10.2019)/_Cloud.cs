using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _Cloud : MonoBehaviour
{
    public GameMaster gm;
    float limite, spawn;
    void Start()
    {
        limite = gm.getLimit();
        spawn = gm.getLimit2();
    }
    void Update()
    {
        transform.position = new Vector2(transform.position.x + Time.deltaTime, transform.position.y);
        if(transform.position.x > limite)
        {
            transform.position = new Vector2(spawn, transform.position.y);
        }
    }
}
