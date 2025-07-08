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
    public playerC player;
    public Gerador geradorDeInimigos;
    public SelecaoDeUpgrade selecaoDeUpgrade;
    public SpriteRenderer background;
    public List<Vector2> offset = new();
    public Sprite bgLobby, bgCyber, bgCobertura;
    public CameraSala2D cameraSala2D;
    public SpriteRenderer spriteRenderer;
    public List<Sala> salasPrincipais;
    public Sala arena;
    public Sala boss;
    public Sala inicioAndFim;
    public Sala salaAtual;
    public CameraConfig salasPrincipaisConfig;
    private Dictionary<TipoSalas, Sala> outrasSalas = new();
    public int andar = 0;
    public int salaIndex = 0;
    void Start()
    {
        andar = 0;
        spriteRenderer.sprite = inicioAndFim.portaAberta[0];
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

        foreach (Sala sala in salasPrincipais)
        {
            (sala.portaFechada[2], sala.portaAberta[2]) = (sala.portaAberta[2], sala.portaFechada[2]);
        }
    }

    public void TrocarSala(Porta destino)
    {
        Debug.Log("Trocando de sala");
        player.Madd.Cura(10000); //recupera vida ao trocar de sala
        if (destino.ehFimDeAndar)
        {
            // Tela de upgrade
            player.gameObject.SetActive(false);
            selecaoDeUpgrade.gameObject.SetActive(true);
            if (andar == 0)
                background.sprite = bgCyber;
            else if (andar == 1)
                background.sprite = bgCobertura;
            else
                background.sprite = bgLobby;
            selecaoDeUpgrade.TelaDeUpgrade();
            andar++;
        }
        else
        {
            selecaoDeUpgrade.gameObject.SetActive(false);
        }
        ProxSala(destino);

        if (destino.proxPorta != null)
        {
            destino.proxPorta.gameObject.SetActive(true);
            destino.gameObject.SetActive(false);
        }
        player.transform.position = destino.Destino.position;
        salaAtual.SetupSpawnPoints();
        geradorDeInimigos.IniciarSpawn();
    }

    void ProxSala(Porta destino)
    {
        int index = Random.Range(0, salasPrincipais.Count);
        background.transform.position = offset[++salaIndex];
        Sala prox_sala = (destino.tipoDeDestino == TipoSalas.SALA_COMUM) ?
            salasPrincipais[index] : outrasSalas[destino.tipoDeDestino];

        if (prox_sala.tipo == TipoSalas.SALA_COMUM)
        {
            cameraSala2D.TeleportarParaNovaSala(
                player.transform,
                salasPrincipaisConfig.tamanhoSala,
                spriteRenderer.gameObject.transform.position,
                prox_sala.tipo
            );
        }
        else
        {
            cameraSala2D.TeleportarParaNovaSala(
                player.transform,
                prox_sala.cameraConfig.tamanhoSala,
                spriteRenderer.gameObject.transform.position,
                prox_sala.tipo
            );
        }

        spriteRenderer.sprite = prox_sala.portaAberta[andar];
        Invoke(nameof(FecharSala), 1f);

        salaAtual.colisores.SetActive(false);
        if (salaAtual.tipo == TipoSalas.SALA_COMUM)
            salaAtual.colisores.transform.parent.gameObject.SetActive(false);

        salaAtual = prox_sala;

        salaAtual.colisores.SetActive(true);
        if (salaAtual.tipo == TipoSalas.SALA_COMUM)
            salaAtual.colisores.transform.parent.gameObject.SetActive(true);
    }

    public void AbrirSala()
    {
        spriteRenderer.sprite = salaAtual.portaAberta[andar];
    }

    void FecharSala()
    {
        spriteRenderer.sprite = salaAtual.portaFechada[andar];
    }
}

[System.Serializable]
public class Sala
{
    public TipoSalas tipo;
    public GameObject colisores;
    public List<Sprite> portaFechada = new(3);
    public List<Sprite> portaAberta = new(3);
    public CameraConfig cameraConfig;
    public List<Transform> spawnPoints = new();
    public Transform spawnPointPai;

    public void SetupSpawnPoints()
    {
        spawnPoints.Clear();
        if (spawnPointPai == null) return;
        for (int i = 0; i < spawnPointPai.childCount; i++)
            spawnPoints.Add(spawnPointPai.GetChild(i));
    }
}