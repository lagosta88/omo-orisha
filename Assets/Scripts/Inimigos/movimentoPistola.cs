using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movimentopistola : InimigoGeral
{
    public float speed = 2f;
    public float jumpForce = 5f;
    public float jumpThreshold = 1.5f; // altura mínima para considerar pulo
    private bool ChaoS;
    private Transform target;
    private Rigidbody2D rb;
    public AtkHitBox hitbox;
    public float tempoEntreAtaques;
    public GameObject projetilPrefab;
    public Vector3 deslocamentoSpawnProjetil;
    public Animator animator;
    public float minDistancia;
    private float direcao = 1;
    new void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        target = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine("AtkLoop");

    }

    IEnumerator AtkLoop()
    {
        while (Slider.vidaatual > 0)
        {
            animator.SetTrigger("Atacando");

            yield return new WaitForSeconds(tempoEntreAtaques);

        }
    }


    public void DispararPistola() //chamado pela animacao de atacar
    {
        AudioManager.instance.TocarSom(AudioManager.instance.somPistolaAtk);
        Vector3 vetorDeslocamento = new Vector3(deslocamentoSpawnProjetil.x * direcao, deslocamentoSpawnProjetil.y, deslocamentoSpawnProjetil.z);
        GameObject projetil = Instantiate(projetilPrefab, transform.position + vetorDeslocamento, Quaternion.identity);
        projetil.GetComponent<ProjetilPistola>().alvo = target.gameObject;

        //hitbox.VerificarAtk(10);
    }
    void FixedUpdate()
    {

        ExplodirSeMorrer();
        IndicadorDeDano();

        if (target == null) return;

        // Move apenas no eixo X
        float distanciaX = target.position.x - transform.position.x;
        float distanciaAbsoluta = Mathf.Abs(distanciaX);
        direcao = Mathf.Sign(distanciaX);



        //diz para onde o inimigo esta apontando
        transform.right = new Vector3(-direcao, 0, 0);

        if (distanciaAbsoluta > minDistancia + 0.5f)
        {
            // Muito longe, aproxima
            rb.linearVelocity = new Vector2(direcao * speed, rb.linearVelocity.y);
            AudioManager.instance.TocarSom(AudioManager.instance.somPistolaAnda);
        }
        else if (distanciaAbsoluta < minDistancia - 0.5f)
        {
            // Muito perto, afasta
            rb.linearVelocity = new Vector2(-direcao * speed, rb.linearVelocity.y);
            AudioManager.instance.TocarSom(AudioManager.instance.somPistolaAnda);
        }
        else
        {
            // Na faixa ideal, para
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
        }


        // Verifica se deve pular
        float verticalDistance = target.position.y - transform.position.y;

        if (verticalDistance > jumpThreshold && ChaoS)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0); // zera o Y antes de aplicar pulo
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    // Verifica se está no chão
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Chao"))
        {
            ChaoS = true;
            animator.SetTrigger("NoChao");
            Debug.Log("no chao!");
        }
    }

    // Detecta quando sai do chão
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Chao"))
        {
            ChaoS = false;
            animator.SetTrigger("Pulou");
        }
    }
    
   

    /*
    void OnDrawGizmos()
    {
        if (hitbox.visualizar) hitbox.MostrarCaixa(hitbox.qualDebug);
    }
    */

}
