using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    int vidas = 1;
    int move = 1;
    int defaultMove;
    public float speed;
    bool haveSomethingAhead;
    bool knockback;
    bool isOnFloor;
    bool sendScore = false;
    bool isAlive = true;
    bool isOnRange_Right;
    bool isOnRange_Left;
    bool isAttacking = false;
    public Transform CheckTerrain;
    public Transform AttackLeft;
    public Transform AttackRight;
    public Transform player;
    public Transform CheckObj;
    public LayerMask LayerCantos;
    public LayerMask LayerSpikes;
    public LayerMask LayerTerrain;
    public LayerMask LayerPlayer;
    public LayerMask LayerEnemy;
    public float checkObj_Radius;
    public float CheckPlayer_Radius;
    public float CheckTerrain_Radius;
	public GameObject BloodParticle;
    float cooldownPadrao_Atk;
    float timerAttack = 0;
    float timerKnock = 0;
    Transform tr;
    Rigidbody2D body;
    Animator anim;
    SpriteRenderer sprite;
    public GameObject snake;
    public GameMaster gm;

    void Start()
    {
        tr = GetComponent<Transform>();
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        if (_SavePoints.Jogo_Dificuldade == 1) { cooldownPadrao_Atk = 5; defaultMove = 1; }
        if (_SavePoints.Jogo_Dificuldade == 2) { cooldownPadrao_Atk = 2; defaultMove = 1; }
        if (_SavePoints.Jogo_Dificuldade == 3) { cooldownPadrao_Atk = 0.3f; vidas = 5; defaultMove = 4; }
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("velX", Mathf.Abs(body.velocity.x));
        isOnRange_Right = Physics2D.OverlapCircle(AttackRight.position, CheckPlayer_Radius, LayerPlayer);
        isOnRange_Left = Physics2D.OverlapCircle(AttackLeft.position, CheckPlayer_Radius, LayerPlayer);
        isOnFloor = Physics2D.OverlapCircle(CheckTerrain.position, CheckTerrain_Radius, LayerTerrain);
        haveSomethingAhead = Physics2D.OverlapCircle(CheckObj.position, checkObj_Radius, LayerCantos);
        if (haveSomethingAhead == false) { haveSomethingAhead = Physics2D.OverlapCircle(CheckObj.position, checkObj_Radius, LayerSpikes);}
        if (haveSomethingAhead == false) { haveSomethingAhead = Physics2D.OverlapCircle(CheckObj.position, checkObj_Radius, LayerEnemy); }
        if (timerAttack > 0) { timerAttack -= Time.deltaTime; }
        if ((isOnRange_Left || isOnRange_Right) && timerAttack <= 0 && isAttacking == false)
        {
            if((isOnRange_Left && sprite.flipX == false) || (isOnRange_Right && sprite.flipX == true))
            {
                Flip();
            }
            timerAttack = cooldownPadrao_Atk;
            isAttacking = true;
            anim.SetTrigger("Attack");
        }
        if(vidas <= 0)
        {
            anim.SetTrigger("Die");
            if (sendScore == false)
            {
                _SavePoints.inimigosMortos++;
                StartCoroutine("sendScoretoGM");
                sendScore = true;
            }
            isAlive = false;
        }
        if (knockback)
        {
            timerKnock += Time.deltaTime;
            if (tr.position.x < player.position.x)
            {
                if (timerKnock > 0.2f) { body.velocity = new Vector2(-3, -3); }
                if (timerKnock < 0.1f) { body.velocity = new Vector2(-3, 3); }
            }
            if (tr.position.x > player.position.x)
            {
                if (timerKnock < 0.1f) { body.velocity = new Vector2(3, 3); }
                if (timerKnock > 0.2f) { body.velocity = new Vector2(3, -3); }
            }
            if (timerKnock > 0.3f) { timerKnock = 0; knockback = false; }
        }
    }
    private void FixedUpdate()
    {
        if(isOnFloor == false || haveSomethingAhead)
        {
            Flip();
        }
        if (knockback == false && isAttacking == false && isAlive == true)
        {
            body.velocity = new Vector2(move * speed, body.velocity.y);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(CheckTerrain.position, CheckTerrain_Radius);
        Gizmos.DrawWireSphere(AttackLeft.position, CheckPlayer_Radius);
        Gizmos.DrawWireSphere(AttackRight.position, CheckPlayer_Radius);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(CheckObj.position, checkObj_Radius);
    }

    void Flip()
    {
        sprite.flipX = !sprite.flipX;
        CheckObj.localPosition = new Vector2(CheckObj.localPosition.x * -1, CheckObj.localPosition.y);
        CheckTerrain.localPosition = new Vector2(CheckTerrain.localPosition.x * -1, CheckTerrain.localPosition.y);
        if(sprite.flipX == true) { move = -defaultMove; } else { move = defaultMove; }
    }

    public IEnumerator LevarDano()
    {
        vidas -= 1;
        knockback = true;
        Instantiate(BloodParticle, tr.position, Quaternion.identity);
        sprite.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        sprite.color = Color.white;
        yield return new WaitForSeconds(0.2f);
        sprite.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        sprite.color = Color.white;
    }

    public void Sumir()
    {
        Destroy(snake, 3f);
    }

    public void Attack()
    {
        Collider2D[] player = Physics2D.OverlapCircleAll(AttackLeft.position, CheckPlayer_Radius, LayerPlayer);
        if(player.Length == 0)
        {
            player = Physics2D.OverlapCircleAll(AttackRight.position, CheckPlayer_Radius, LayerPlayer);
        }
        for (int i = 0; i < player.Length; i++)
        {
            player[i].SendMessage("LevarDano", tr.position.x);
        }
        isAttacking = false;
    }


    public IEnumerator sendScoretoGM()
    {
        if (_SavePoints.Jogo_Dificuldade == 3) { gm.getTotalScore(200); } else { gm.getTotalScore(100); }
        for (int i = 0; i < 50; i++)
        {
            gm.getScore();
            gm.getScore();
            if(_SavePoints.Jogo_Dificuldade == 3)
            {
                gm.getScore();
                gm.getScore();
            }
            yield return new WaitForSeconds(0.001f);
        }
    }
}