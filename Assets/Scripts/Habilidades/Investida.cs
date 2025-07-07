using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Investida : Habilidade
{
    public Rigidbody2D rigidBody;
    public playerC playerC;
    public AtkHitBox atk;
    public float dano;

    public float velocidade;
    float direcao = 1;
    //tempoCooldown = 1;

    public float tempoTotal;
    float contadorTempo = 0;


    public override void Ativar()
    {
        base.Ativar();

        animator.SetTrigger("Investida");

        if(nivel >= 3)
        {
            //causar dano ao inimigo ao passar por ele
        }

        if(nivel >= 2)
        {
            invulnerabilidade = true;
            atravessarInimigos = true;
        }

        habilidadeAtiva = true;
        Debug.Log("investida ativada!");
        direcao = playerC.getDirecao();

        StartCoroutine(CourotinaInvestida());
        
        atk.VerificarAtk(dano);
    }
    
    //Coroutina da investida (faz ela durar varios frames)
    private IEnumerator CourotinaInvestida()
    {
        while(contadorTempo < tempoTotal)
        {
            //Debug.Log("contador frames = " + contadorTempo + " vel = " + velocidade + "dir = " + direcao);
            //colocar algo pra travar o movimento enquanto a investida ocorre

            rigidBody.linearVelocity = new Vector2(direcao * velocidade, playerC.ridi.linearVelocityY);
            contadorTempo += Time.deltaTime;
            yield return null;
            
        }
        Debug.Log("coroutina finalizada");
        invulnerabilidade = false;
        atravessarInimigos = false;
        habilidadeAtiva = false;
        animator.SetTrigger("FimDaHabilidade");
        contadorTempo = 0;

        FimDaHabilidade();


    }


    void OnDrawGizmos()
    {
        if (atk.visualizar) atk.MostrarCaixa(atk.qualDebug);
    }

}
