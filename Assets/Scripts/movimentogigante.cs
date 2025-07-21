using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movimentogigante : MonoBehaviour
{
    public float speed = 2f;
    public float jumpForce = 7f;
    public float jumpThreshold = 1.5f;
    public float minDistancia = 3f;
    private bool ChaoS;
    private Transform target;
    private Rigidbody2D rb;
    public VidaInimigo Slider;
    public AtkHitBox hitbox;
    private SpriteRenderer spriteRenderer;
    private bool jaPulou = false;


    private BossState estadoAtual = BossState.Aproximar;
    private float tempoProximoEstado = 5f;

    enum BossState
    {
        Aproximar,
        Afastar,
        PuloEsmagar,
        Parado
    }

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine("AtkLoop");
    }

    IEnumerator AtkLoop()
    {
        while (Slider.vidaatual > 0)
        {
            hitbox.VerificarAtk(10);
            yield return new WaitForSeconds(1f);
        }
    }

    void Update()
    {
        if (Slider.vidaatual <= 0)
        {
            Destroy(gameObject);
            return;
        }

        // Alternar estados a cada X segundos
        if (Time.time > tempoProximoEstado)
        {
            EscolherNovoEstado();
        }
    }

    void FixedUpdate()
    {

        if (target.position.x < transform.position.x)
        {
            spriteRenderer.flipX = true;  // Vira para a esquerda
        }
        else
        {
            spriteRenderer.flipX = false; // Vira para a direita
        }

        if (target == null) return;

        float distanciaX = target.position.x - transform.position.x;
        float direcao = Mathf.Sign(distanciaX);
        float distanciaAbsoluta = Mathf.Abs(distanciaX);

        float distanciaY = target.position.y - transform.position.y;
        float distanciaYAbsoluta = Mathf.Abs(distanciaY);


        switch (estadoAtual)
        {
            case BossState.Aproximar:
                if (distanciaAbsoluta > minDistancia)
                    rb.linearVelocity = new Vector2(direcao * speed, rb.linearVelocity.y);
                else
                    rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
                break;

            case BossState.Afastar:
                rb.linearVelocity = new Vector2(-direcao * speed, rb.linearVelocity.y);
                break;

            case BossState.PuloEsmagar:
                // Só executa se estiver no chão
                if (!jaPulou && ChaoS)
                {
                    rb.linearVelocity = Vector2.zero;
                    Vector2 direcaoPulo = new Vector2(direcao * speed * 2f, distanciaAbsoluta);
                    rb.AddForce(direcaoPulo, ForceMode2D.Impulse);
                    jaPulou = true;
                }
                break;

            case BossState.Parado:
                rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
                break;
        }
    }


    void EscolherNovoEstado()
    {
        float distancia = Vector2.Distance(transform.position, target.position);
        List<BossState> opcoes = new List<BossState>();
        if (distancia < 5f)
        {
            opcoes.Add(BossState.Afastar);
            opcoes.Add(BossState.Aproximar);
        }
        else if (distancia <= 300f && distancia >= 5f)
        {
            opcoes.Add(BossState.PuloEsmagar);
            //opcoes.Add(BossState.Afastar);
        }
        else
        {
            opcoes.Add(BossState.Aproximar);
            opcoes.Add(BossState.Parado);
        }

        estadoAtual = opcoes[Random.Range(0, opcoes.Count)];
        tempoProximoEstado = Time.time + Random.Range(2f, 4f);
        jaPulou = false;
}

    

    // Verifica se está no chão
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Chao"))
        {
            ChaoS = true;
        }
    }

    // Detecta quando sai do chão
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Chao"))
        {
            ChaoS = false;
        }
    }

    void OnDrawGizmos()
    {
        if (hitbox.visualizar)
            hitbox.MostrarCaixa(hitbox.qualDebug);
    }
}
