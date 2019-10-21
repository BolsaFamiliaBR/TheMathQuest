using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _Plataform : MonoBehaviour
{
    public BoxCollider2D col;
    public Transform check;
    public float checkRange;
    public LayerMask ground;
    float timer;
    bool isOn = false;
    bool back = false;
    int way = 2;

    void Update()
    {
        if (!col)
        {
            if (Physics2D.OverlapCircle(check.position, checkRange, ground) == true)
            {
                if (way == -5) { way = -2; } else if (way == 5) { way = 2; }
                way *= -1;
                check.localPosition = new Vector2(check.localPosition.x * -1, check.localPosition.y);
            }
            transform.position = new Vector2(transform.position.x + Time.deltaTime * way, transform.position.y);
        }
        if (back)
        {
            transform.position = new Vector2(transform.position.x + Time.deltaTime * way, transform.position.y);
            if (Physics2D.OverlapCircle(check.position, checkRange, ground) == true)
            {
                back = false;
                isOn = false;
                check.localPosition = new Vector2(check.localPosition.x * -1, check.localPosition.y);
            }
        }
        else
        {
            if (timer > 5 && col && isOn == true)
            {
                check.localPosition = new Vector2(check.localPosition.x * -1, check.localPosition.y);
                way = 4;
                way *= -1;
                back = true;
            }
            else
            {
                timer += Time.deltaTime;
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
        if(collision.gameObject.name == "Player" && col)
        {
            way = 4;
            isOn = true;
            if (Physics2D.OverlapCircle(check.position, checkRange, ground) == true)
            {
                way = 0;
            }
            transform.position = new Vector2(transform.position.x + Time.deltaTime * way, transform.position.y);
            timer = 0;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.name == "Player")
        {
            if (way == 0)
            {
                check.localPosition = new Vector2(check.localPosition.x * -1, check.localPosition.y);
                way = 4;
                way *= -1;
                back = true;
            }
        }
    }

    public void isWaiting()
    {
        if (way > 0) { way = -5; check.localPosition = new Vector2(check.localPosition.x * -1, check.localPosition.y); }
    }
}
