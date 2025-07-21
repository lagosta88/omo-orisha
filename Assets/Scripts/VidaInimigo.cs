using UnityEngine;
using UnityEngine.UI;

public class VidaInimigo : MonoBehaviour
{
    public Slider Slidervida;
    public Gradient gradiente;
    public Image fill;

    public int vidamaxima = 20;
    public int vidaatual;

    void Awake()
    {
        vidaatual = vidamaxima;
        Slidervida.maxValue = vidamaxima;
        Slidervida.value = vidaatual;

        // faz a UI ter o valor indicado como referencia

        fill.color = gradiente.Evaluate(1f);
    }

    public void Dano(int qnt)
    {
        Debug.Log("inimigo atingido");
        vidaatual -= qnt;
        if (vidaatual < 0)
        {
            vidaatual = 0;
            Debug.Log("inimigo morto");
        } // reduz a variavel vidaatual e se for menor que zero volta para zero

        Slidervida.value = vidaatual;
    } // atualiza a informação na tela de vida

    
}
