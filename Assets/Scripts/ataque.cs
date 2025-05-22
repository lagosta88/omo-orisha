using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

//estudar melhor como funciona o timer
//https://discussions.unity.com/t/simple-timer/56201/2


/*
 * iniciar - definir o timer
 * criar - precisa ser criado em uma posicao especifica ligada ao objeto original (personagem)  e manter essa posicao.
 * expandir - aumentar o tamanho do glpe
 * dar o tempo - contar o tempo ate o golpe acabar
 * destruir - destroi a hitbox
 */

public class ataque : MonoBehaviour
{
    public GameObject hitbox;
    public GameObject personagem;
    private GameObject instancia;
    private bool instanciaexiste = false;


    public float cooldown = 1f;
    private float timer = 0f;
    private bool cooldownOver;

    public float hitbox_time_alive = 0.5f;

    public float hitbox_x;
    public float hitbox_y;
    public float hitbox_z;

    // Start is called before the first frame update
    void Start()
    {
        timer = cooldown;
        cooldownOver = true;
    }

    // Update is called once per frame
    void Update()
    {


        //posso tornar isso uma fun��o pr�pria depois
        //timer pra checar o cooldown
        if (timer < cooldown)
        {
            timer = timer + Time.deltaTime;
            cooldownOver = false;

            //mostra o timer no console
            //Debug.Log($"timer = {timer}");
        }
        else
        {
            cooldownOver = true;

        }

        //ataque em si (checa se o botao foi apertado e se o cooldown acabou)
        if (Input.GetKeyDown(KeyCode.Mouse0) == true)
        {
            if (cooldownOver == true)
            {
                //reinicia o cooldown ao iniciar o golpe
                timer = 0;

                instanciaexiste = true;

                //cria a instancia da hitbox
                instancia = Instantiate(hitbox, transform.position, transform.rotation);

                //Debug.Log("hitbox do ataque criada (jogador)");

                //coloca a instancia posicao correta
                instancia.transform.SetParent(transform, true);

                instancia.transform.position = instancia.transform.position + new Vector3(hitbox_x * direcao.getDirecao(personagem), hitbox_y, hitbox_z);



            }
        }

        if(instanciaexiste == true) //a hitbox acompanha o movimento do jogador
        {
            instancia.transform.position = personagem.transform.position + new Vector3(hitbox_x * direcao.getDirecao(personagem), hitbox_y, hitbox_z);

        }


        if ((timer >= hitbox_time_alive) && (instancia != null)) //destroi a hitbox ap�s seu tempo terminar
        {
            //Debug.Log("hitbox do ataque destruida (jogador)");
            instanciaexiste = false;
            Destroy(instancia);

        }
    }
}

