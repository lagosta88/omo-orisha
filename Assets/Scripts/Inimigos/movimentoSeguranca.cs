using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movimentoSeguranca : InimigoGeral
{
    public float speed = 2f;
    public float jumpForce = 5f;
    public float jumpThreshold = 1.5f; // altura mínima para considerar pulo
    public float attackThreshold; //distancia minima para atacar
    private bool ChaoS;
    private Transform target;
    private Rigidbody2D rb;
    public AtkHitBox hitbox;
<<<<<<< HEAD:Assets/Scripts/movimento_inimigo.cs
    void Awake()
=======
    public Animator animator;
    public float tempoEntreAtaques;
    public float danoCausado;
    new void Start()
>>>>>>> origin/merge-lucca-cezi:Assets/Scripts/Inimigos/movimentoSeguranca.cs
    {
        base.Start(); 
        target = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
<<<<<<< HEAD:Assets/Scripts/movimento_inimigo.cs
    }

    void Start()
    {
        StartCoroutine(nameof(AtkLoop));
=======
        animator = GetComponent<Animator>();
        StartCoroutine("AtkLoop");
>>>>>>> origin/merge-lucca-cezi:Assets/Scripts/Inimigos/movimentoSeguranca.cs
    }

    IEnumerator AtkLoop()
    {
        while (Slider.vidaatual > 0)
        {
            Debug.Log("Loop seguranca! ");
            float distancia = Mathf.Abs(target.position.x - transform.position.x);
            if (distancia <= attackThreshold)
            {
                Atacar();
            }
            
            yield return new WaitForSeconds(tempoEntreAtaques);

        }
    }

    void Atacar()
    {
        animator.SetTrigger("Atacando");
    }


    void AtivarHitboxAtaque()
    {
        hitbox.VerificarAtk(danoCausado);
    }


    void FixedUpdate()
    {
        ExplodirSeMorrer();
        IndicadorDeDano();


        if (target == null) return;


        // Move apenas no eixo X
        float direction = Mathf.Sign(target.position.x - transform.position.x);
        rb.linearVelocity = new Vector2(direction * speed, rb.linearVelocity.y);

        // Verifica se deve pular
        float verticalDistance = target.position.y - transform.position.y;

        if (verticalDistance > jumpThreshold && ChaoS)
        {
            animator.SetTrigger("Pulando");
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0); // zera o Y antes de aplicar pulo
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        OlharParaAlvo(target, 1);

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
        if (hitbox.visualizar) hitbox.MostrarCaixa(hitbox.qualDebug);
    }
    
}
