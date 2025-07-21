using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movimentoBomba : MonoBehaviour
{
    public float speed = 2f;
    private Transform target;
    private Rigidbody2D rb;
    public VidaInimigo Slider;
    public AtkHitBox hitbox;
    void Awake()
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


        transform.right = target.position - transform.position; 

        
    }

    void OnDrawGizmos()
    {
        if (hitbox.visualizar) hitbox.MostrarCaixa(hitbox.qualDebug);
    }
}
