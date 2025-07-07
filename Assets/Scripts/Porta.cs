using UnityEngine;

public class Porta : MonoBehaviour
{
    public playerC player;
    public GerenciadorCenario gerenciadorCenario;
    public TipoSalas tipoDeDestino;
    public Transform Destino => transform.GetChild(0);
    public Porta proxPorta;
    public KeyCode teclaTeleporte = KeyCode.L;
    private bool jogadorNaArea;
    public bool ehFimDeAndar = false;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(player.tag))
        {
            jogadorNaArea = true;
            // Feedback visual (ex: highlight na porta)
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(player.tag))
        {
            jogadorNaArea = false;
            // Remover feedback visual
        }
    }

    void Update()
    {
        if (jogadorNaArea && Input.GetKeyDown(teclaTeleporte))
        {
            gerenciadorCenario.TrocarSala(this);
        }
    }
}