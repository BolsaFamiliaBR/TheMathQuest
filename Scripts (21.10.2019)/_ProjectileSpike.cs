using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _ProjectileSpike : MonoBehaviour
{
    public GameObject espinho;
    public GameObject particle;
    Player player;
    private void Start()
    {
        transform.rotation = Quaternion.Euler(0, 0, 90);
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        Destroy(espinho, 10);
    }
    void Update()
    {
        transform.position = new Vector2(transform.position.x + Time.deltaTime * -6, transform.position.y);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == "Player")
        {
            player.ProjectileCollision(transform.position.x);
            Instantiate(particle, transform.position, Quaternion.identity);
            Destroy(espinho);
        } else
        {
            Instantiate(particle, transform.position, Quaternion.identity);
            Destroy(espinho);
        }
    }
}
