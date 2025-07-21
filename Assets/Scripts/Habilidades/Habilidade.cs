using UnityEngine;

public abstract class Habilidade : MonoBehaviour //essa classe servira de base para todas as outras habilidades
{
    [TextArea]
    public string descricaoHabilidade;

    [Range(0, 3)] public int nivel = 0;
   
    public Animator animator;
    public Sprite icone;
    public bool habilidadeAtiva = false; //sempre que uma habilidade for iniciada, habilidade ativa deve ser true. apos o fim dela, ela deve ser false
    protected bool invulnerabilidade = false;
    protected bool atravessarInimigos = false;

    //variaveis do cooldown
    public float cooldown;
    private float tempoAtual;
    public bool habilidadeDisponivel = true;

    public delegate void HabilidadeFinalizada();
    public static event HabilidadeFinalizada OnHabilidadeFinalizada;


    public void Start()
    {
        animator = GetComponent<Animator>();
        tempoAtual = cooldown;
    }


    public virtual void Ativar() //toda habilidade tem que ter uma forma de ser ativada. eh o que ocorre quando se aperta o botao de usar a habilidade
    {
        habilidadeAtiva = true;
        
        
      
    }

    public void FimDaHabilidade()
    {
        if(OnHabilidadeFinalizada != null)
        {
            OnHabilidadeFinalizada();
        }

        tempoAtual = 0;
        habilidadeDisponivel = false;
        habilidadeAtiva = false;

    }
    public void Update()
    {
        if (tempoAtual < cooldown) //avanca o tempoAtual se a habilidade estiver em cooldown
        {
            tempoAtual += Time.deltaTime;
        }
        else
        {
            habilidadeDisponivel = true;
        }
    }

    //verificacoes (para passar para o playerC)

    public bool EstaInvulneravel()
    {
        return invulnerabilidade;
    }

    public bool PodeAtravessarInimigos()
    {
        return invulnerabilidade;
    }


   
}
  
