using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movimento_inimigo : InimigoGeral
{
    public float speed = 2f;
    public float jumpForce = 5f;
    public float jumpThreshold = 1.5f; // altura mínima para considerar pulo
    private bool ChaoS;
    private Transform target;
    private Rigidbody2D rb;
    public AtkHitBox hitbox;
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
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
    void FixedUpdate()
    {

        if (Slider.vidaatual <= 0)
        {

            Destroy(gameObject);

        }



        if (target == null) return;

        // Move apenas no eixo X
        float direction = Mathf.Sign(target.position.x - transform.position.x);
        rb.linearVelocity = new Vector2(direction * speed, rb.linearVelocity.y);

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
