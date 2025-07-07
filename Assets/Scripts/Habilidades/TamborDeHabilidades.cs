using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;

public class TamborDeHabilidades : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public Habilidade[] roda = new Habilidade[4];
    public int numRodaAtual; //identifica qual o numero da habilidade que esta selecionada na roda
    public Sprite iconeVazio;
    public Animator animator;
    public bool EmRotacao;
 

    public Vector2[] pos = new Vector2[16];


    //eventos para avisar os Icones para trocar de posicao quando a roda estiver girando
    public delegate void GirandoEsquerda();
    public static event GirandoEsquerda OnGirandoEsquerda;

    public delegate void GirandoDireita();
    public static event GirandoDireita OnGirandoDireita;

    void Start()
    {
        SelecaoDeUpgrade.OnUpgrade += UpgradeHabilidade; //faz a funcao AdicionarHabilidade ser chamada após uma habilidade receber upgrade]s

        EmRotacao = false;



    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpgradeHabilidade()
    {

    }


    //Funcoes para animacao

    //Funcionamento: botao é apertado: RodarParaEsquerda. Animacao troca de frame: TrocaDeFrameEsquerda. TrocaDeFrameEsquerda chama OnGirandoEsquerda, avisando aos icones para trocar de posicao

    //Essas servem para avisar quando a animacao de rodar deve acontecer
    public void RodarParaEsquerda()
    {
        if (!EmRotacao)
        {
            EmRotacao = true;

            Debug.Log("Girando para esquerda!");

            animator.SetTrigger("RodarParaEsquerda");



            if (numRodaAtual < 3)
            {
                numRodaAtual++;
            }
            else
            {
                numRodaAtual = 0;
            }
        }
  
    }

    public void RodarParaDireita()
    {

        if (!EmRotacao)
        {
            EmRotacao = true;

            Debug.Log("Girando para direita!");

            animator.SetTrigger("RodarParaDireita");

            if (numRodaAtual > 0)
            {
                numRodaAtual--;
            }
            else
            {
                numRodaAtual = 3;
            }
        }
        
    }

    public void ChecarSeVaiRodarNovamenteEsquerda() //chamada no fim da animacao de rodar para esquerda
    {
        EmRotacao = false;

        if (HabilidadeEhNula())
        {
            RodarParaEsquerda();
        }
    }

    public void ChecarSeVaiRodarNovamenteDireita() //chamada no fim da animacao de rodar para direita
    {
        EmRotacao = false;
        if (HabilidadeEhNula())
        {
            RodarParaDireita();
        }
    }


    public bool HabilidadeEhNula()
    {
        if (roda[0] != null && roda[numRodaAtual] == null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //essas servem para quando ocorrer uma troca de frame na animacao de girar a animacao avisar o script
    public void TrocaDeFrameEsquerda()
    {
        OnGirandoEsquerda();
    }

    public void TrocaDeFrameDireita()
    {
        OnGirandoDireita();
    }

    public int QuantidadeDeHabilidades()
    {
        int qtd = 0;
        foreach (Habilidade hab in roda)
        {
            qtd++;
        }
        return qtd;
    }



}
