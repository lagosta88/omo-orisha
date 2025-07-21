using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class playerC : MonoBehaviour
{
    public bool modoImortal;
    public PlayerCombo atk;
    public UIvida Madd;

    public GameObject alvoobjeto;
    SpriteRenderer meuspriteRenderer;
    Transform posicao;
    Animator animator;
    public Rigidbody2D ridi;
    private float move;
    public float puloForca = 300f;
    public float velon;
    private int twojump;
    public bool ChaoS; 
    private bool Wall;
    public float wallJumpForcaX = 150f;
    public float wallJumpForcaY = 250f;
    private bool facingRight = true;
    public TamborDeHabilidades tambor;
    public Habilidade habilidadeAtual;
    public Collider2D colliderChao;
    public bool tocouOChao;
    public Habilidade UltimaHabilidadeUtilizada = null;
    public float atritoHabilidade; //desaceleracao sofrida pelo player ao usar uma habilidades
    public bool emAtaque = false;
    private bool derrotado = false;


    // Start is called before the first frame update
    void Start()
    {
        meuspriteRenderer = GetComponent<SpriteRenderer>();
        posicao = GetComponent<Transform>();
        ridi = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        velon = 10;
        twojump = 1;
        atk.player = true;
        //  posicao.localPosition = new Vector3(0,0,0);

        

      

    }

    private void OnEnable()
    {
        UIvida.OnMorte += Morreu; //quando a vida do jogador chegar a zero (controlado por UIvida), chamar a funcao Morreu()
        UIvida.OnReceberDano += LevouDano;
    }

    private void OnDisable()
    {
        UIvida.OnMorte -= Morreu; //quando a vida do jogador chegar a zero (controlado por UIvida), chamar a funcao Morreu()
        UIvida.OnReceberDano -= LevouDano;
    }

    // Update is called once per frame
    void Update()
    {

        if (!derrotado) { }

        //teste para arrumar deteccao com o chao
        if (ridi.linearVelocityY < -1 && tocouOChao == false)
        {
            //Debug.Log("Velocidade < 1, ChaoS = false");
            ChaoS = false;
        }
        


        //colisoes
        if (PodeAtravessarInimigos())
        {
            //Debug.Log("colisao ignorada!");
            Physics2D.IgnoreLayerCollision(3, 6, true); //ignora a colisao entre o jogador e inimigos
        }
        else
        {
            Physics2D.IgnoreLayerCollision(3, 6, false);
        }

        //SISTEMA DE ATAQUE
            SistemaDeAtaque(); //Deixa esse processo separado para fins de organização

        //PULO - LOGICA
        if (ChaoS)
            twojump = 1;
        // zera os jumps sempre que toca o chao

        //ANDAR
        if (Input.GetAxis("Horizontal") <= 1)
        {
            if (!EstaUsandoHabilidade())
            {

                move = Input.GetAxis("Horizontal");


                //define a direcao que o jogador esta apontando baseado no movimento dele (necessario para definir a direcao do sprite, ataques e habilidades

                facingRight = acharDirecao(move, facingRight);
                alinharDirecao(facingRight);
            }
            else
            {
                if (ChaoS)
                {
                    move += -move * atritoHabilidade * Time.deltaTime; //sofre uma desaceleracao ao usar uma habilidade
                }
            }
            Movimento(move);
        }
        // nesse movimento voce pode ajustar a velocidade da direção, menor para esquerda valores maiores menor para direita valores decimais

       

        //PULAR
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if ((ChaoS || twojump > 0) && !EstaUsandoHabilidade())
            {
                pulou();
            }
        }

        //AGARRAR NA PAREDE E WALLJUMP


        if (Wall && !ChaoS)
        {
            animator.SetTrigger("AgarrarParede");
        }

        if (Wall && !ChaoS && Input.GetKeyDown(KeyCode.Space))
        {
            wallJ();
            animator.SetTrigger("pulou");
        }

        //HABILIDADES

        //Ativar habilidade

        
        if (Input.GetMouseButtonDown(1))
        {
            habilidadeAtual = tambor.roda[tambor.numRodaAtual];
            if (!EstaUsandoHabilidade() && !EstaAtacando() && HabilidadeDisponivel()) { 

            ativarHabilidade();
            }
        }


        //trocar habilidade
        
            if (Input.GetKeyDown(KeyCode.E))
            {
                tambor.RodarParaEsquerda();
            }

            if (Input.GetKeyDown(KeyCode.Q))
            {
                tambor.RodarParaDireita();
            }

        //variaveis do animator

        animator.SetFloat("ModuloMovement", Mathf.Abs(move));

        animator.SetBool("EstaNoChao", ChaoS);

        animator.SetFloat("VelocidadeY", ridi.linearVelocityY);

        animator.SetBool("Wall", Wall);

        tocouOChao = false;
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
            if (atk.comboDisponivel && Time.time - atk.momentoUltimoAtaque <= atk.janelaDeCombo && !EstaUsandoHabilidade())
            {
                //Segue o combo
                atk.atacando = true;
                ExecuteCombo(atk.comboAtual);
                atk.comboAtual = (atk.comboAtual + 1) % atk.nomesTrigger.Count; //Isso torna o combo cíclio, após a última etapa volta à primeira
            }
            else if (Time.time - atk.momentoUltimoAtaque > atk.janelaDeCombo && !EstaUsandoHabilidade())
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
        atk.VerificarAtk(10); //O parâmetro é o dano do ataque. Está em 0 pq não há inimigos com vida ainda

        //CHAMADA DAS ANIMAÇÕES
        animator.SetTrigger(atk.nomesTrigger[etapa]);
        
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
        animator.SetTrigger("pulou");
        ridi.linearVelocity = new Vector2(ridi.linearVelocity.x, 0); // Reseta velocidade vertical para pulo consistente
        ridi.AddForce(new Vector2(0, puloForca), ForceMode2D.Impulse);
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

    public void EmContatoComInimigo()
    {
            if (!EstaInvulneravel())
            {
                Madd.Dano(10);
            }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Chao"))
        { // Quando colidir com chao a variavel fica true
            ChaoS = true;
            tocouOChao = true;
        }
        

        //if (collision.collider.CompareTag("Inimigo")) // Quando colidir com inimigo perde 10 de vida
        //    Madd.Dano(10);

            //DesbugarPulo();
        

        if (collision.collider.CompareTag("Cura"))
        {
            // Quando colidir com cura ganha 10 de vida
            Madd.Cura(10);

            //DesbugarPulo();

        }

        if (collision.collider.CompareTag("parede"))
        {// Quando colidir com parede a variavel fica true
            Wall = true;
            //DesbugarPulo();
        }



    }

    private void OnCollisionStay(Collision collision)
    {
         if (collision.collider.CompareTag("Chao")) // Quando colidir com chao a variavel fica true
              
            tocouOChao = true;

        if (collision.collider.CompareTag("parede"))
        {// Quando colidir com parede a variavel fica true
            Wall = true;
            ;
        }
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
    { //define o lado que o personagem deve estar apontando

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

 
    
    void ativarHabilidade()
    {
        
        Debug.Log("habilidadeAtual: " + habilidadeAtual);

        if(habilidadeAtual != null)
        {
            UltimaHabilidadeUtilizada = habilidadeAtual;
            habilidadeAtual.Ativar();
       
        }
        
    }


    bool HabilidadeDisponivel()
    {
        if(habilidadeAtual != null)
        {
            return habilidadeAtual.habilidadeDisponivel;
        }
        else
        {
            return false;
        }
    }

    void OnDrawGizmos()
    {
        if (atk.visualizar) atk.MostrarCaixa(atk.qualDebug);
    }


    //VERIFICAÇÕES
    //utilizadas para evitar que o jogador possa realizar duas ações ao mesmo tempos

    public bool EstaNoAr()
    {
        return !ChaoS;
    }

    /* IMPLEMENTAR
    bool EstaAtacando()
    {

    }
    */


    public bool EstaEmAcao()
    {
        if(EstaNoAr())              return true;
        if(EstaAtacando())          return true;
        if(EstaUsandoHabilidade())  return true;

        return false;
    }

    public bool EstaInvulneravel()
    {
        if (habilidadeAtual != null)
        {
            return habilidadeAtual.EstaInvulneravel();
        }
        else return false;
    }

    public bool PodeAtravessarInimigos()
    {
        if (habilidadeAtual != null)
        {
            return habilidadeAtual.PodeAtravessarInimigos();
        }
        else return false;
    }

    public void DesbugarPulo() //quando o jogador fica em cima de algum inimigo ou outra entidade, eh entendido que ele esta no chao (aplicar no OnCollision) - ainda em fase de testes
    {
        if (ridi.linearVelocityY == 0)
        {
            ChaoS = true;
        }
    }

    public bool EstaUsandoHabilidade()
    {
        if(UltimaHabilidadeUtilizada != null)
        {
            return UltimaHabilidadeUtilizada.habilidadeAtiva;
        }
        else
        {
            return false;
        }
    }

    public bool EstaAtacando()
    {
        return emAtaque;
    }

    public void InicioAtaque() //chamada no comeco da animacao de ataque
    {
        emAtaque = true;
    }

    public void FimAtaque() //chamada ao fim da animacao de ataque
    {
        PermitirProxCombo();
        emAtaque = false;
    }

    public void InterromperAcao() //NECESSARIO chamar isso em toda animacao que saia de AnyState e nao seja uma habilidade ou ataque para evitar conflitos
    {
        if (UltimaHabilidadeUtilizada != null)
        {
            UltimaHabilidadeUtilizada.habilidadeAtiva = false;
        }

        FimAtaque();
    }

    public void Morreu() //chamado pelo evento UIvida.OnMorte()
    {
        if (!modoImortal)
        {
            derrotado = true;
            Debug.Log("Voce morreu - playerC desativado");
            Invoke("AnimacaoMorte", 0);
            enabled = false;
            
        }
        else
        {
            Debug.Log("Você iria morrer, mas está com o modo imortal ativado");
        }
       

    }

    private void DesativarScript()
    {
        enabled = false; //desativa esse script
    }

    private void AnimacaoMorte()
    {
        animator.SetTrigger("Morreu");
        this.enabled = false;
    }

    public void LevouDano() //chamado pelo evento UIvida.ReceberDano()
    {
        if (!derrotado)
        {
            animator.SetTrigger("RecebeuDano");
        
        }
    }
}

