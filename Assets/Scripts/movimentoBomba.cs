using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movimentoBomba : InimigoGeral
{
    public float speed = 2f;
    private Transform target;
    private Rigidbody2D rb;
    public AtkHitBox hitbox;

    public Animator animator;
    public Vector3 deslocamentoExplosao;
    private SpriteRenderer spriteRenderer;
    public GameObject ExplosaoMorte;
    public GameObject BombaPrefab;
    public Vector3 deslocamentoSpawnBomba;
    public float tempoEntreAtaques;
    private int vidaFrameAnterior;
    void Start()
    {

        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine("AtkLoop");
    }

    IEnumerator AtkLoop()
    {
        while (Slider.vidaatual > 0)
        {

            //hitbox.VerificarAtk(10);
            animator.SetTrigger("Atacando");
            yield return new WaitForSeconds(tempoEntreAtaques);

        }
    }

    public void DispararBomba()
    {
        GameObject projetil = Instantiate(BombaPrefab, transform.position + deslocamentoSpawnBomba, Quaternion.identity);
        projetil.GetComponent<ProjetilBomba>().alvo = target.gameObject;
    }
    void FixedUpdate()
    {

        
        //checar se recebeu dano
        if (Slider.vidaatual != vidaFrameAnterior)
        {
            spriteRenderer.color = Color.red;
            //animator.SetTrigger("Danificado");
        }
        else
        {
            spriteRenderer.color = Color.white;
        }
        vidaFrameAnterior = Slider.vidaatual;

        if (Slider.vidaatual <= 0)
        {
            Instantiate(ExplosaoMorte, transform.position + deslocamentoExplosao, Quaternion.identity);
            Destroy(gameObject);

        }

        float direcao = Mathf.Sign(Vector3.Magnitude(target.position - transform.position));
        transform.right = new Vector3(direcao, 0f, 0f);

        
    }

    void OnDrawGizmos()
    {
        if (hitbox.visualizar) hitbox.MostrarCaixa(hitbox.qualDebug);
    }
}
