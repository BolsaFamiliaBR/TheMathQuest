using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _Scene5Plataform : MonoBehaviour
{
    public Transform check;
    public float checkRange;
    public LayerMask ground;
    float timer;
    bool start = false;
    bool back = false;
    int way = 0;

    void Update()
    {
        if (Physics2D.OverlapCircle(check.position, checkRange, ground) == true)
        {
            way *= -1;
            check.localPosition = new Vector2(check.localPosition.x * -1, check.localPosition.y);
        }
        transform.position = new Vector2(transform.position.x + Time.deltaTime * way, transform.position.y);

        if (timer > 5)
        {
            back = true;
            if (check.localPosition.x > 0) { check.localPosition = new Vector2(check.localPosition.x * -1, check.localPosition.y); }
            timer = 0;
        }
        timer += Time.deltaTime;
        
        if (back)
        {
            way = -4;
            transform.position = new Vector2(transform.position.x + Time.deltaTime * way, transform.position.y);
            if (Physics2D.OverlapCircle(check.position, checkRange, ground) == true)
            {
                back = false;
                check.localPosition = new Vector2(check.localPosition.x * -1, check.localPosition.y);
                way = 0;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(check.position, checkRange);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            if(start == false) { way = 4; start = true; }
            timer = 0;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            start = false;
            timer = 0;
        }
    }
}