using UnityEngine;

public class Terremoto : Habilidade
{
    public AtkHitBox atk;
    public float[] danoCausado = new float[3];


    public override void Ativar()
    {
        base.Ativar();

        habilidadeAtiva = true;

        atk.comboAtual = nivel - 1;

        animator.SetTrigger(atk.nomesTrigger[nivel - 1]);

        
    }

    public void AtivarHitbox() //chamado pela animacao no momento que o ataque conectar
    {
        atk.VerificarAtk(danoCausado[nivel - 1]);
    }


    public void FinalizarTerremoto() //chamado pela animacao
    {
        base.FimDaHabilidade();
    }
    
    void OnDrawGizmos()
    {
        if (atk.visualizar) atk.MostrarCaixa(atk.qualDebug);
    }
}
