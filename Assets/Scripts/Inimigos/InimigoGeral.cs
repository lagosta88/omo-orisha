using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class InimigoGeral : MonoBehaviour
{
    public VidaInimigo Slider;

    public GameObject ExplosaoMorte;
    public Vector3 deslocamentoExplosao;

    protected SpriteRenderer spriteRenderer;
    protected int vidaFrameAnterior;

    public void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        Slider.vidaatual = Slider.vidamaxima;
        vidaFrameAnterior = Slider.vidaatual;
    }
    public void ExplodirSeMorrer()
    {
        if (Slider.vidaatual <= 0)
        {
            Instantiate(ExplosaoMorte, transform.position + deslocamentoExplosao, Quaternion.identity);
            Destroy(gameObject);

        }

    }

    public void IndicadorDeDano()
    {
        //checar se recebeu dano
        if (Slider.vidaatual != vidaFrameAnterior)
        {
            spriteRenderer.color = Color.red;

            //animator.SetTrigger("Danificado");
        }
        else
        {
            spriteRenderer.color = Color.white;
        }
        vidaFrameAnterior = Slider.vidaatual;
    }
   
    public void OlharParaAlvo(Transform target, float direcaoPadrao)
    {
        //olhar para o jogador
        Vector3 vetorParaOInimigo = target.position - transform.position;
        float direcao = Mathf.Sign(vetorParaOInimigo.x);
        transform.right = new Vector3(direcao * direcaoPadrao, 0f, 0f);
    }
    public Gerador gerador;

    public void SetupGerador(Gerador gerador)
    {
        this.gerador = gerador;
    }

}
