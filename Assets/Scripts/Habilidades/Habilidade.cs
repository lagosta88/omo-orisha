using UnityEngine;

public abstract class Habilidade //essa classe servira de base para todas as outras habilidades
{

    public int nivel = 0;
    public Cooldown cooldown;

    public float tempoCooldown; //tempo entre uma ativacao e outra

    public Habilidade()
    {
        cooldown = new Cooldown(tempoCooldown);
    }

    
    public virtual void Ativar() //toda habilidade tem que ter uma forma de ser ativada. eh o que ocorre quando se aperta o botao de usar a habilidade
    {

    }
    
}
