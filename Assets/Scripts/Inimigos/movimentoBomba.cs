using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class movimentoBomba : InimigoGeral
{
    public float speed = 2f;
    private Transform target;
    private Rigidbody2D rb;
    public AtkHitBox hitbox;

    public Animator animator;
    public GameObject BombaPrefab;
    public Vector3 deslocamentoSpawnBomba;
    public float tempoEntreAtaques;

    new void Start()
    {
        base.Start();
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
        AudioManager.instance.TocarSom(AudioManager.instance.somBombaAtk1);
    }
    void FixedUpdate()
    {
        ExplodirSeMorrer();
        IndicadorDeDano();
        OlharParaAlvo(target, -1);



    }
    
       new public void IndicadorDeDano()
{
    base.IndicadorDeDano();

    AudioManager.instance.TocarSom(AudioManager.instance.somBombaDano);
}

    /*
    void OnDrawGizmos()
    {
        if (hitbox.visualizar) hitbox.MostrarCaixa(hitbox.qualDebug);
    }
    */
}
