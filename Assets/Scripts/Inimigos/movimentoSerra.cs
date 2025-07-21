using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class movimentoSerra : InimigoGeral
{
    public float speed = 2f;
    public float jumpForce = 5f;
    public float jumpThreshold = 1.5f; // altura m�nima para considerar pulo
    public float attackThreshold; //distancia minima para atacar
    private bool ChaoS;
    private Transform target;
    private Rigidbody2D rb;
    public AtkHitBox hitbox;
    public Animator animator;
    public float tempoEntreAtaques;
    public float danoCausado;
    public Collider2D colisor;

    //variaveis do dash
    public float velocidade;
    float direction = 1;
    public float tempoTotal;
    float contadorTempo = 0;
    bool invulnerabilidade = false;
    bool podeMover = true;

    new void Start()
    {
        base.Start();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        colisor = GetComponent<Collider2D>();
        StartCoroutine("AtkLoop");
    }

    IEnumerator AtkLoop()
    {
        while (Slider.vidaatual > 0)
        {
            Debug.Log("Loop serra! ");
            float distancia = Mathf.Abs(target.position.x - transform.position.x);
            Debug.Log("distancia = " + distancia);
            if (distancia <= attackThreshold)
            {
                Atacar();
            }

            yield return new WaitForSeconds(tempoEntreAtaques);

        }
    }

    void Atacar()
    {
        AudioManager.instance.TocarSom(AudioManager.instance.somSerraAtk);
        Debug.Log("ataque do serra chamado!");
        animator.SetTrigger("PrepararAtaque");
        podeMover = false;
    }


    public void DashSerra() //chamado pela animacao
    {
        StartCoroutine(CourotinaDashSerra());
        //invulnerabilidade = true;
    }

    private IEnumerator CourotinaDashSerra()
    {
        while (contadorTempo < tempoTotal)
        {
            //Debug.Log("contador frames = " + contadorTempo + " vel = " + velocidade + "dir = " + direcao);
            //colocar algo pra travar o movimento enquanto a investida ocorre

            rb.linearVelocity = new Vector2(direction * velocidade, transform.position.y);
            contadorTempo += Time.deltaTime;
            yield return null;

        }
        Debug.Log("coroutina DashSerra finalizada");
        AtivarHitboxAtaque();
        contadorTempo = 0;
    }


    void FimDoAtaque() //chamado pela animacao
    {
        //invulnerabilidade = false;
        podeMover = true;
    }

    void AtivarHitboxAtaque()
    {
        hitbox.VerificarAtk(danoCausado);
    }


    void FixedUpdate()
    {
        ExplodirSeMorrer();
        IndicadorDeDano();


        if (invulnerabilidade)
        {
            colisor.isTrigger = true;
        }
        else
        {
            colisor.isTrigger = false;
        }


        if (target == null) return;

        if (podeMover)
        {
            // Move apenas no eixo X
            direction = Mathf.Sign(target.position.x - transform.position.x);
            rb.linearVelocity = new Vector2(direction * speed, rb.linearVelocity.y);
            //AudioManager.instance.TocarSom(AudioManager.instance.somSerraAnda);

            // Verifica se deve pular
            float verticalDistance = target.position.y - transform.position.y;

            if (verticalDistance > jumpThreshold && ChaoS)
            {
                animator.SetTrigger("Pulando");
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0); // zera o Y antes de aplicar pulo
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }

            OlharParaAlvo(target, -1);
        }
    }

    // Verifica se est� no ch�o
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Chao"))
        {
            ChaoS = true;
        }
    }

    // Detecta quando sai do ch�o
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Chao"))
        {
            ChaoS = false;
        }
    }

    /*
      StartCoroutine(CourotinaInvestida());
        
      
    
    
    //Coroutina da investida (faz ela durar varios frames)
    private IEnumerator CourotinaInvestida()
    {
        while(contadorTempo < tempoTotal)
        {
            //Debug.Log("contador frames = " + contadorTempo + " vel = " + velocidade + "dir = " + direcao);
            //colocar algo pra travar o movimento enquanto a investida ocorre

            rigidBody.linearVelocity = new Vector2(direcao * velocidade, playerC.ridi.linearVelocityY);
            contadorTempo += Time.deltaTime;
            yield return null;
            
        }
        Debug.Log("coroutina finalizada");
        invulnerabilidade = false;
        atravessarInimigos = false;
        habilidadeAtiva = false;
        animator.SetTrigger("FimDaHabilidade");
        contadorTempo = 0;

        FimDaHabilidade();


    }
      
     */


    void OnDrawGizmos()
    {
        if (hitbox.visualizar) hitbox.MostrarCaixa(hitbox.qualDebug);
    }
    

}
