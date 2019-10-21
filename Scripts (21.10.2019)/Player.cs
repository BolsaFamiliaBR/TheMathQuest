using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region - Variaveis para controle das particulas - 
    bool particle_left = true;
    bool particle_Fall = false;
    float EmissionValue = 0;
    public GameObject particlesSpikes;
    public Transform particles_transform;
    public ParticleSystem TerrainParticles_Settings;
    public ParticleSystem FallParticles_Left;
    public ParticleSystem FallParticles_Right;
    #endregion
    #region - Variaveis para controlar hitbox -
    float ChecarSoloRaio = 0.3f;
    public float ChecarInimigoRaio;
    public float ChecarInimigoRaio2;
    public float ChecarCantoRaio;
    public float ChecarCoraçãoRaio;
    public float ChecarNoFreezeRaio;
    public Transform ChecarInimigo;
    public Transform ChecarInimigo2;
    public Transform ChecarCoração;
    public Transform ChecarSolo;
    public Transform ChecarCanto;
    public Transform ChecarSpike1;
    public Transform ChecarSpike2;
    public Transform CheckDontFreeze;
    public LayerMask DontFreezeOnFall;
    public LayerMask CamadaSpike;
    public LayerMask CamadaQueda;
    public LayerMask CamadaCanto;
    public LayerMask CamadaSolo;
    public LayerMask CamadaInimigo;
    public LayerMask CamadaCoração;
    #endregion
    #region - Variaveis para controle de mecanica de jogo (movimentação, ataque, knockback, etc)
    Vector3 _SpawnLocation_;
    int vidas = 3;
    int PuloExtra = 0;
    bool isDead = false;
    bool knockback;
    bool EstaNoChao;
    bool EstaAtacando;
    bool EstaPulando;
    bool Caiu;
    float xZombie;
    float velocidade = 15f;
    float ForçaPulo = 450f;
    float coolDown_Attack = 1f;
    float timerKnock;
    float timer_JumpAfterConner;
    float timer_AttackTwice = 0.1f;
    #endregion
    #region - Outros tipos de variaveis -

    public GameObject player_;
    public GameMaster gm;
    public CameraScript cam;
    CapsuleCollider2D col;
    Transform tr;
    Rigidbody2D body;
    Animator anim;
    SpriteRenderer sprite;
    #endregion

    void Start()
    {
        _SpawnLocation_ = transform.position;
        col = GetComponent<CapsuleCollider2D>();
        tr = GetComponent<Transform>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
        particlesSpikes.SetActive(false);
        if(_SavePoints.low == true)
        {
            Destroy(TerrainParticles_Settings.gameObject);
            Destroy(FallParticles_Left.gameObject);
            Destroy(FallParticles_Right.gameObject);
        }
        _SavePoints.morto = false;
    }
    void Update()
    {
        anim.SetFloat("velX", Mathf.Abs(body.velocity.x));
        anim.SetFloat("velY", body.velocity.y);
        Caiu = Physics2D.OverlapCircle(ChecarSolo.position, ChecarSoloRaio, CamadaQueda);
        EstaNoChao = Physics2D.OverlapCircle(ChecarSolo.position, ChecarSoloRaio, CamadaSolo);
        #region -- If's aleatorios --
        if (CheckDontFreeze)
        {
            if(Physics2D.OverlapCircle(CheckDontFreeze.position, ChecarNoFreezeRaio, DontFreezeOnFall) && EstaNoChao == false) { DoNotFreeze(); }
        }
        if(vidas < 3)
        {
            Collider2D[] Hearths = Physics2D.OverlapCircleAll(ChecarCoração.position, ChecarCoraçãoRaio, CamadaCoração);
            for (int i = 0; i < Hearths.Length; i++)
            {
                Hearths[i].SendMessage("Pegar");
                vidas++;
                StartCoroutine("PiscarVerde");
                bool levouDano = false;
                gm.UpdateHearths(vidas, levouDano);
            }
            if (_SavePoints.vidasExtras > 0)
            {
                vidas++;
                _SavePoints.vidasExtras--;
                bool levouDano = false;
                gm.UpdateHearths(vidas, levouDano);
            }
        }
        if (Physics2D.OverlapCircle(ChecarCanto.position, ChecarCantoRaio, CamadaCanto) == true) 
        { 
            GrabConner();
        }
        if (Caiu)
        {
            transform.position = _SpawnLocation_;
            if (_SavePoints.Jogo_Dificuldade > 1)
            {
                vidas--;
                gm.UpdateHearths(vidas, true);
            }
        }
        if (EstaNoChao)
        {
            PuloExtra = 1;
            anim.ResetTrigger("DoubleJump");
            anim.SetTrigger("isOnGround");
            if (_SavePoints.low == false)
            {
                particles_transform.position = new Vector2(transform.position.x, transform.position.y - 1);
                if (particle_left == true)
                {
                    particles_transform.rotation = Quaternion.Euler(90, 0, 0);
                }
                else
                {
                    particles_transform.rotation = Quaternion.Euler(90, 180, 0);
                }
                if (particle_Fall == true && body.velocity.y == 0)
                {
                    FallParticles_Right.transform.position = new Vector2(transform.position.x + 0.2327f, transform.position.y - 1.061004f);
                    FallParticles_Left.transform.position = new Vector2(transform.position.x + 0.2327f, transform.position.y - 1.061004f);
                    FallParticles_Left.Play();
                    FallParticles_Right.Play();
                    particle_Fall = false;
                }
            }
        }
        else
        {
            anim.ResetTrigger("isOnGround");
        }
        if (timer_AttackTwice >= 0) { timer_AttackTwice -= Time.deltaTime; }
        if (coolDown_Attack >= 0) { coolDown_Attack -= Time.deltaTime; }
        if (timer_JumpAfterConner >= 0) { timer_JumpAfterConner -= Time.deltaTime; }
        if (knockback)
        {
            timerKnock += Time.deltaTime;
            if (tr.position.x < xZombie)
            {
                if (timerKnock > 0.2f) { body.velocity = new Vector2(-5, -3); }
                if (timerKnock < 0.1f) { body.velocity = new Vector2(-5, 3); }
            }
            if (tr.position.x > xZombie)
            if (tr.position.x > xZombie)
            {
                if (timerKnock < 0.1f) { body.velocity = new Vector2(5, 3); }
                if (timerKnock > 0.2f) { body.velocity = new Vector2(5, -3); }
            }
            if (timerKnock > 0.3f) { timerKnock = 0; knockback = false; }
        }
        if(vidas == 0)
        {
            anim.SetTrigger("Die");
            isDead = true;
            gm.playerDeath();
            _SavePoints.morto = true;
            Destroy(player_, 2f);
        }
        #endregion
        #region -- Input de botões e comandos --
        if (Input.GetButtonDown("Jump") && PuloExtra > 0 && EstaAtacando == false && timer_JumpAfterConner <= 0) // Verificar botão de pulo
        {
            EstaPulando = true;
            PuloExtra--;
            if (PuloExtra == 0) 
            {
                anim.SetTrigger("DoubleJump");
                particle_Fall = true;
            }
        }
        if (coolDown_Attack < 0) // Verificar condicionais de ataque
        {
            if (EstaNoChao == true && Input.GetButtonDown("Fire1"))
            {
                if (timer_AttackTwice > 0)
                {
                    anim.SetTrigger("Attack_2");
                    timer_AttackTwice = -1;
                    coolDown_Attack = 1f;
                } else {
                    anim.SetTrigger("Attack_1");
                    timer_AttackTwice = 0.5f;
                }
                EstaAtacando = true;
            }
        }
        #endregion
    }
    private void FixedUpdate()
    {
        #region -- Mecanica e fisica da movimentação --
        float move = Input.GetAxis("Horizontal");
        if (knockback == false && isDead == false)
        {
            body.velocity = new Vector2(move * velocidade, body.velocity.y);
            if (EstaPulando == true)
            {
                body.AddForce(new Vector2(0f, ForçaPulo));
                EstaPulando = false;
            }
            if ((move > 0 && sprite.flipX == true) || (move < 0 && sprite.flipX == false))
            {
                Flip();
            }
            if (_SavePoints.low == false)
            {
                if (Mathf.Abs(body.velocity.x) > 0.1 && EstaNoChao == true)
                {
                    if (Mathf.Abs(body.velocity.x) > 10) { EmissionValue = 10; }
                    else { EmissionValue = 3; }
                }
                else
                {
                    EmissionValue = 0;
                }
                var emission = TerrainParticles_Settings.emission;
                emission.rateOverTime = EmissionValue;
            }
        }
        #endregion
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(ChecarSolo.position, ChecarSoloRaio);
        Gizmos.DrawWireSphere(ChecarInimigo.position, ChecarInimigoRaio);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(ChecarInimigo2.position, ChecarInimigoRaio2);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(ChecarCanto.position, ChecarCantoRaio);
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(ChecarCoração.position, ChecarCoraçãoRaio);
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(ChecarSpike2.position, ChecarSoloRaio);
        if (CheckDontFreeze)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(CheckDontFreeze.position, ChecarNoFreezeRaio);
        }

    }
    void Flip()
    {
        particle_left = !particle_left;
        sprite.flipX = !sprite.flipX;
        ChecarInimigo.localPosition = new Vector2(ChecarInimigo.localPosition.x * -1, ChecarInimigo.localPosition.y);
        ChecarInimigo2.localPosition = new Vector2(ChecarInimigo2.localPosition.x * -1, ChecarInimigo2.localPosition.y);
        ChecarCanto.localPosition = new Vector2(ChecarCanto.localPosition.x * -1, ChecarCanto.localPosition.y);
        ChecarSpike1.localPosition = new Vector2(ChecarSpike1.localPosition.x * -1, ChecarSpike1.localPosition.y);
        ChecarSpike2.localPosition = new Vector2(ChecarSpike2.localPosition.x * -1, ChecarSpike2.localPosition.y);
        col.offset = new Vector2(col.offset.x * -1, col.offset.y);  
    }
    public void Attack()
    {
        Collider2D[] InimigosAtacados = Physics2D.OverlapCircleAll(ChecarInimigo.position, ChecarInimigoRaio, CamadaInimigo);
        for (int i = 0; i < InimigosAtacados.Length; i++)
        {
            InimigosAtacados[i].SendMessage("LevarDano");
        }
        EstaAtacando = false;
    }
    public void Attack2()
    {
        Collider2D[] InimigosAtacados = Physics2D.OverlapCircleAll(ChecarInimigo2.position, ChecarInimigoRaio2, CamadaInimigo);
        for (int i = 0; i < InimigosAtacados.Length; i++)
        {
            InimigosAtacados[i].SendMessage("LevarDano");
        }
        EstaAtacando = false;
    }
    public IEnumerator LevarDano(float x)
    {
        xZombie = x;
        if(x != -10000 && x != 10000) 
        {
            _SavePoints.danosofrido++;
            vidas--; 
            gm.UpdateHearths(vidas, true);
            StartCoroutine("losePoints");
        }   
        knockback = true;
        StartCoroutine(cam.Shake(.50f, .15f));
        sprite.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        sprite.color = Color.white;
        yield return new WaitForSeconds(0.2f);
        sprite.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        sprite.color = Color.white;
    }
    public IEnumerator PiscarVerde()
    {
        sprite.color = Color.green;
        yield return new WaitForSeconds(0.2f);
        sprite.color = Color.white;
        yield return new WaitForSeconds(0.2f);
        sprite.color = Color.green;
        yield return new WaitForSeconds(0.2f);
        sprite.color = Color.white;
    }
    void DoNotFreeze()
    {
        body.velocity = new Vector2(body.velocity.x, -20);
    }
    void GrabConner()
    {
        body.velocity = new Vector2(body.velocity.x, 10);
        timer_JumpAfterConner = 0.5f;
    }
    IEnumerator losePoints()
    {
        gm.getTotalScore(-50);
        for (int i = 0; i < 25; i++)
        {
            gm.getScore2();
            gm.getScore2();
            yield return new WaitForSeconds(0.001f);
        }
    }
    public void Errou()
    {
        isDead = true;
    }
    public void ProjectileCollision(float x)
    {
        StartCoroutine("LevarDano", x);
    }
    public void checkpoint()
    {
        _SpawnLocation_ = transform.position;
    }
}