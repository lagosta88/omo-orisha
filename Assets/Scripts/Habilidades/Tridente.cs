using System.Collections;
using UnityEngine;

public class Tridente : Habilidade
{
    public Rigidbody2D rigidBody;
    public playerC playerC;
    public AtkHitBox atk;
    
    public float velocidade;
    public float tempoTotal;
    float direcao = 1;
    //tempoCooldown = 1;
    public float[] danoGolpes = new float[3];

    
    float contadorTempo = 0;


    public override void Ativar()
    {
        base.Ativar();

        habilidadeAtiva = true;

        direcao = playerC.getDirecao();

        animator.SetTrigger(atk.nomesTrigger[nivel - 1]);

        StartCoroutine(CourotinaDash());

        
        
    }


    //ataques chamados pela animacao
    public void AtaqueTridente(int numAtaque)
    {
        atk.comboAtual = numAtaque;
        atk.VerificarAtk(danoGolpes[numAtaque]);
    }

    private IEnumerator CourotinaDash()
    {
        while (contadorTempo < tempoTotal)
        {

            rigidBody.linearVelocity = new Vector2(direcao * velocidade, playerC.ridi.linearVelocityY);
            contadorTempo += Time.deltaTime;
            yield return null;

        }
        Debug.Log("coroutina finalizada");
        contadorTempo = 0;

    }


    public void FinalizarTridente() //chamado pela animacao
    {
        base.FimDaHabilidade();
    }


     void OnDrawGizmos()
    {
        if (atk.visualizar) atk.MostrarCaixa(atk.qualDebug);
    }

}