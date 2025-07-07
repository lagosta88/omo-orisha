using System.Collections.Generic;
using UnityEngine;

public enum TipoSalas
{
    INICIO_FIM,
    SALA_COMUM,
    ARENA,
    BOSS
}
public class GerenciadorCenario : MonoBehaviour
{
    public SpriteRenderer background;
    public List<Vector2> offset = new();
    public Sprite bgLobby, bgCyber;
    public CameraSala2D cameraSala2D;
    public Transform playerTransform;
    public SpriteRenderer spriteRenderer;
    public List<Sala> salasPrincipais;
    public Sala arena;
    public Sala boss;
    public Sala inicioAndFim;
    public Sala salaAtual;
    public CameraConfig salasPrincipaisConfig;
    private Dictionary<TipoSalas, Sala> outrasSalas = new();
    public int salaIndex = 0;
    void Start()
    {
        spriteRenderer.sprite = inicioAndFim.portaAberta;
        outrasSalas = new Dictionary<TipoSalas, Sala>()
        {
            { TipoSalas.BOSS, boss },
            { TipoSalas.ARENA, arena },
            { TipoSalas.INICIO_FIM, inicioAndFim }
        };

        foreach (Sala item in salasPrincipais)
        {
            item.colisores.SetActive(false);
        }
        foreach (var item in outrasSalas)
        {
            item.Value.colisores.SetActive(false);
        }
        salaAtual = inicioAndFim;
        salaAtual.colisores.SetActive(true);
        background.sprite = bgLobby;
        background.transform.position = offset[0];
    }

    public void TrocarSala(Porta destino)
    {
        Debug.Log("Trocando de sala");

        if (salaIndex == 5)
        {
            // Tela de upgrade
            background.sprite = bgCyber;
        }
        else
        {
            int index = Random.Range(0, salasPrincipais.Count);
            background.transform.position = offset[++salaIndex];
            Sala prox_sala = (destino.tipoDeDestino == TipoSalas.SALA_COMUM) ?
                salasPrincipais[index] : outrasSalas[destino.tipoDeDestino];

            if (prox_sala.tipo == TipoSalas.SALA_COMUM)
            {
                cameraSala2D.TeleportarParaNovaSala(
                    playerTransform,
                    salasPrincipaisConfig.tamanhoSala,
                    spriteRenderer.gameObject.transform.position,
                    prox_sala.tipo
                );
            }
            else
            {
                cameraSala2D.TeleportarParaNovaSala(
                    playerTransform,
                    prox_sala.cameraConfig.tamanhoSala,
                    spriteRenderer.gameObject.transform.position,
                    prox_sala.tipo
                );
            }

            spriteRenderer.sprite = prox_sala.portaAberta;
            Invoke(nameof(FecharSala), 1f);

            salaAtual.colisores.SetActive(false);
            if (salaAtual.tipo == TipoSalas.SALA_COMUM)
                salaAtual.colisores.transform.parent.gameObject.SetActive(false);

            salaAtual = prox_sala;

            salaAtual.colisores.SetActive(true);
            if (salaAtual.tipo == TipoSalas.SALA_COMUM)
                salaAtual.colisores.transform.parent.gameObject.SetActive(true);
        }

        if (destino.proxPorta != null)
        {
            destino.proxPorta.gameObject.SetActive(true);
            Destroy(destino.gameObject);
        }
        playerTransform.position = destino.Destino.position;
    }

    public void AbrirSala()
    {
        spriteRenderer.sprite = salaAtual.portaAberta;
    }

    void FecharSala()
    {
        spriteRenderer.sprite = salaAtual.portaFechada;
    }

    public void TrocarNivel()
    {

    }
}

[System.Serializable]
public struct Sala
{
    public TipoSalas tipo;
    public GameObject colisores;
    public Sprite portaFechada;
    public Sprite portaAberta;
    public CameraConfig cameraConfig;
}