using UnityEngine;

public class CooldownHabilidades : MonoBehaviour
{
    protected TamborDeHabilidades tambor;
    public int numHabilidade;
    float MultiplicadorVelocidadeAnimacao;
    Habilidade habilidade;
    Animator animator;
    public playerC playerC;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tambor = GetComponentInParent<TamborDeHabilidades>();
        animator = GetComponent<Animator>();
       

      
        
        Habilidade.OnHabilidadeFinalizada += AtivarCooldown;

        habilidade = tambor.roda[numHabilidade];

        if (habilidade.cooldown != 0)
        {
            MultiplicadorVelocidadeAnimacao = 1f / habilidade.cooldown;
        }
        else { MultiplicadorVelocidadeAnimacao = 100f; };
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AtivarCooldown() //chamado no OnHabilidadeFinalizada
    {
        Habilidade hab = playerC.UltimaHabilidadeUtilizada;

        if (playerC.UltimaHabilidadeUtilizada == tambor.roda[numHabilidade])
        {

            animator.SetFloat("SpeedMultiplier", MultiplicadorVelocidadeAnimacao);
            animator.SetTrigger("AtivarCooldown");
        }
    }
}
