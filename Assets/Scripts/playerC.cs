using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class playerC : MonoBehaviour
{
    public PlayerCombo atk;
    public UIvida Madd;

    public GameObject alvoobjeto;
    SpriteRenderer meuspriteRenderer;
    Transform posicao;
    Rigidbody2D ridi;
    private float move;
    public float puloForca = 300f;
    public float velon;
    private int twojump;
    private bool ChaoS;
    private bool Wall;
    public float wallJumpForcaX = 150f;
    public float wallJumpForcaY = 250f;
    private bool facingRight = true;


    // Start is called before the first frame update
    void Start()
    {
        meuspriteRenderer = GetComponent<SpriteRenderer>();
        posicao = GetComponent<Transform>();
        ridi = GetComponent<Rigidbody2D>();
        velon = 10;
        twojump = 1;
        atk.player = true;
        //  posicao.localPosition = new Vector3(0,0,0);

    }
    
    // Update is called once per frame
    void Update()
    {
        SistemaDeAtaque(); //Deixa esse processo separado para fins de organização
        if (ChaoS)
            twojump = 1;
        // zera os jumps sempre que toca o chao

        if (Input.GetAxis("Horizontal") <= 1)
        {
            move = Input.GetAxis("Horizontal");
            Movimento(move);

            //define a direcao que o jogador esta apontando baseado no movimento dele (necessario para definir a direcao do sprite, ataques e habilidades

            facingRight = acharDirecao(move, facingRight);
            alinharDirecao(facingRight);

        }
        // nesse movimento voce pode ajustar a velocidade da direção, menor para esquerda valores maiores menor para direita valores decimais

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (ChaoS || twojump > 0)
            {
                pulou();
            }
        }

        if (Wall && !ChaoS && Input.GetKeyDown(KeyCode.Space))
        {
            wallJ();
        }


    }

    void SistemaDeAtaque()
    {
        //Reseta o tempo de combo
        if (Time.time - atk.momentoUltimoAtaque > atk.janelaDeCombo && atk.comboAtual > 0)
        {
            atk.comboAtual = 0;
            atk.comboDisponivel = true;
        }

        if (Input.GetKeyDown(KeyCode.Mouse0) && atk.ataqueHabilitado)
        {
            if (atk.comboDisponivel && Time.time - atk.momentoUltimoAtaque <= atk.janelaDeCombo)
            {
                //Segue o combo
                atk.atacando = true;
                ExecuteCombo(atk.comboAtual);
                atk.comboAtual = (atk.comboAtual + 1) % atk.nomesTrigger.Count; //Isso torna o combo cíclio, após a última etapa volta à primeira
            }
            else if (Time.time - atk.momentoUltimoAtaque > atk.janelaDeCombo)
            {
                //Novo combo
                atk.comboAtual = 0;
                atk.atacando = true;
                ExecuteCombo(atk.comboAtual);
                atk.comboAtual++;
            }
            atk.momentoUltimoAtaque = Time.time;
        }
    }

    void ExecuteCombo(int etapa)
    {
        if (etapa >= atk.nomesTrigger.Count)
        {
            Debug.LogError("ETAPA DE COMBO FORA DOS DOMÍNIOS");
            return;
        }
        atk.comboDisponivel = false;
        print("Executou o ataque " + etapa);
        atk.VerificarAtk(0); //O parâmetro é o dano do ataque. Está em 0 pq não há inimigos com vida ainda


        //A LINHA DE CÓDIGO ABAIXO DEVE SER REMOVIDA QUANDO AS ANIMAÇÕES DO PLAYER FOREM IMPLEMENTADAS!!!!
        Invoke(nameof(PermitirProxCombo), 0.4f);
        //A LINHA DE CÓDIGO ACIMA DEVE SER REMOVIDA QUANDO AS ANIMAÇÕES DO PLAYER FOREM IMPLEMENTADAS!!!!


        //CHAMADA DAS ANIMAÇÕES (MUITO IMPORTANTE E NECESSÁRIO - PENDENTE)
        //Modelo de chamada:
        //animator.SetTrigger(atk.nomesTrigger[etapa]);
    }

    // Método chamado por Animation Event
    // Assim que implementarmos as animações, ao passar pelo frame específico de fim de atk esse método será chamado
    public void PermitirProxCombo()
    {
        atk.comboDisponivel = true;
        atk.atacando = false;
    }

    void pulou()
    {
        ridi.linearVelocity = new Vector2(ridi.linearVelocity.x, 0); // Reseta velocidade vertical para pulo consistente
        ridi.AddForce(new Vector2(0, puloForca));
        twojump--;
        ChaoS = false;
    }

    void wallJ()
    {
        float direcao = move > 0 ? -1 : 1; // Pula para o lado oposto da parede
        ridi.linearVelocity = Vector2.zero; // Reseta velocidade
        ridi.AddForce(new Vector2(direcao * wallJumpForcaX, wallJumpForcaY));
        Wall = false;
    }


    void Movimento(float move)
    {
        ridi.linearVelocity = new Vector2(move * velon, ridi.linearVelocity.y);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Chao")) // Quando colidir com chao a variavel fica true
            ChaoS = true;



        if (collision.collider.CompareTag("Inimigo")) // Quando colidir com inimigo perde 10 de vida
            Madd.Dano(10);



        if (collision.collider.CompareTag("Cura")) // Quando colidir com cura ganha 10 de vida
            Madd.Cura(10);



        if (collision.collider.CompareTag("parede")) // Quando colidir com parede a variavel fica true
            Wall = true;

    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Chao"))
        {
            ChaoS = false;
        }

        if (collision.collider.CompareTag("parede"))
        {
            Wall = false;
        }
    }
    bool acharDirecao(float move, bool facingRight)
    { //define o lado que o personagem deve estar apontando//define o lado que o personagem deve estar apontando

        if (move > 0) { facingRight = true; }
        if (move < 0) { facingRight = false; }
        //Debug.Log("facingRight = " + facingRight);
        return facingRight;
    }

    void alinharDirecao(bool facingRight)
    {


        if ((facingRight && transform.localScale.x < 0) || (!facingRight && transform.localScale.x > 0))
        {

            Vector3 mudarDirecao = transform.localScale;
            mudarDirecao.x *= -1;
            transform.localScale = mudarDirecao;

        }
    }

    public int getDirecao()
    {

        if (facingRight) { return 1; } else { return -1; }

    }

    void OnDrawGizmos()
    {
        if (atk.visualizar) atk.MostrarCaixa(atk.qualDebug);
    }
}

