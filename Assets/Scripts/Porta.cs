using Unity.VisualScripting;
using UnityEngine;

public class Porta : MonoBehaviour
{
    public playerC player;
    public Gerador gerador;
    public GerenciadorCenario gerenciadorCenario;
    public TipoSalas tipoDeDestino;
    public Transform Destino => transform.GetChild(0);
    public Porta proxPorta;
    public bool ehFimDeAndar = false;
    private bool canPass;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(player.tag))
        {
            if (canPass) gerenciadorCenario.TrocarSala(this);
            // Feedback visual (ex: highlight na porta)
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(player.tag))
        {
            // Remover feedback visual
        }
    }

    void Update()
    {
        bool todosMortos = true;
        foreach (var item in gerador.inimigosSpawnados)
        {
            if (item != null)
                todosMortos = false;
        }
        canPass = todosMortos;
        gerenciadorCenario.spriteRenderer.sprite = canPass ? gerenciadorCenario.salaAtual.portaAberta : gerenciadorCenario.salaAtual.portaFechada;
    }
}