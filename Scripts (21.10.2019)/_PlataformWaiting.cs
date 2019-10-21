using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _PlataformWaiting : MonoBehaviour
{
    public _Plataform plataform;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        plataform.isWaiting();
    }
}
