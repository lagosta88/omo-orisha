using UnityEngine;
using UnityEngine.UI;

public class UIvida : MonoBehaviour
{
    public Slider Slidervida;
    public Gradient gradiente;
    public Image fill;

    private int vidamaxima = 100;
    private int vidaatual;

    void Start()
    {
        vidaatual = vidamaxima;
        Slidervida.maxValue = vidamaxima;
        Slidervida.value = vidaatual;

        // faz a UI ter o valor indicado como referencia

        fill.color = gradiente.Evaluate(1f);
    }

    public void Dano(int qnt)
    {
        vidaatual -= qnt;
        if (vidaatual < 0)
        {
            vidaatual = 0;
            Debug.Log("VOCE MORREU");
        } // reduz a variavel vidaatual e se for menor que zero volta para zero

        Slidervida.value = vidaatual;
        fill.color = gradiente.Evaluate(Slidervida.normalizedValue);
    } // atualiza a informação na tela de vida

    public void Cura(int qnt)
    {
        vidaatual += qnt;
        if (vidaatual > vidamaxima)
        {
            vidaatual = vidamaxima;
        }// aumenta a variavel vidaatual e se for maior que o maximo, não ultrapasssa

        Slidervida.value = vidaatual;
        fill.color = gradiente.Evaluate(Slidervida.normalizedValue);
    } // atualiza a informação na tela de vida
}
