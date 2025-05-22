using System.Threading;
using UnityEngine;

public class Cooldown
{
    public bool cooldownOver = true; 
    float timer = 0f; //conta o tempo quando o cooldown estiver ativo
    float duration; 
    public Cooldown(float duration) { 
        this.duration = duration; 
    }

    public void Restart()
    {
        timer = 0f;
        cooldownOver = false;
    }

    public void advance() //deve ser colocado no update do objeto
    {
        
        if(timer < duration) //avanca o timer caso o cooldown esteja rodando
        {
            timer = timer + Time.deltaTime;
        }
        else
        {
            cooldownOver = true;
        }
        
    }

}
