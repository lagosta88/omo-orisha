using UnityEngine;

public class Porta : MonoBehaviour
{
    [SerializeField] private bool todosMortos;
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
            if (tipoDeDestino == TipoSalas.ARENA && gerenciadorCenario.andar != 0)
                tipoDeDestino = TipoSalas.SALA_COMUM;
            if (canPass) gerenciadorCenario.TrocarSala(this);
        }
    }

    void Update()
    {
        todosMortos = true;
        foreach (var item in gerador.inimigosSpawnados)
        {
            if (item != null)
            {
                todosMortos = false;
                break;
            }
        }
        if (Input.GetKey(KeyCode.RightControl)) // Cheat de debug
            todosMortos = true;

        if (todosMortos)
            gerenciadorCenario.AbrirSala();
        else
            gerenciadorCenario.FecharSala();
        canPass = todosMortos;
    }
}