using UnityEngine;

public abstract class Habilidade : MonoBehaviour //essa classe servira de base para todas as outras habilidades
{

    [Range(0, 3)] public int nivel = 0;
    public Cooldown cooldown;
    public Animator animator;
    public Sprite icone;
    public bool habilidadeAtiva = false; //sempre que uma habilidade for iniciada, habilidade ativa deve ser true. apos o fim dela, ela deve ser false
    protected bool invulnerabilidade = false;
    protected bool atravessarInimigos = false;



    public float tempoCooldown; //tempo entre uma ativacao e outra


    public void Start()
    {
        animator = GetComponent<Animator>();
    }

    public Habilidade()
    {
        cooldown = new Cooldown(tempoCooldown);
    }


    public virtual void Ativar() //toda habilidade tem que ter uma forma de ser ativada. eh o que ocorre quando se aperta o botao de usar a habilidade
    {

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
  
