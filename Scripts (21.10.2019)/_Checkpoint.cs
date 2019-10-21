using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _Checkpoint : MonoBehaviour
{
    public Player player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            player.checkpoint();
        }
    }
}
