using System;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraSala2D : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float ortoSizeDinamico, ortoSizeEstatico;
    public CameraConfig config;
    public bool camDinamica = true;
    private Camera cam;
    private float alturaCamera, larguraCamera;

    private void Awake()
    {
        cam = GetComponent<Camera>();
        CalcularLimites();
    }
    
    public void TeleportarParaNovaSala(Transform player, Vector2 novoTamanhoSala, Vector3 posicaoSala, TipoSalas tipo)
    {
        camDinamica = tipo != TipoSalas.ARENA;
        cam.orthographicSize = (!camDinamica) ? ortoSizeEstatico : ortoSizeDinamico;
        config.tamanhoSala = novoTamanhoSala;
        
        Vector3 centroSala = posicaoSala;
        
        transform.position = new (centroSala.x, centroSala.y, transform.position.z);
        
        CalcularLimites();
        
        Vector3 posicaoJogador = player.position;
        posicaoJogador.x = Mathf.Clamp(posicaoJogador.x, config.minBounds.x, config.maxBounds.x);
        posicaoJogador.y = Mathf.Clamp(posicaoJogador.y, config.minBounds.y, config.maxBounds.y);
        player.position = posicaoJogador;
        
        Vector3 posicaoCamera = new (
            Mathf.Clamp(player.position.x, config.minBounds.x, config.maxBounds.x),
            Mathf.Clamp(player.position.y, config.minBounds.y, config.maxBounds.y),
            transform.position.z
        );
        
        transform.position = posicaoCamera;
    }

    private void CalcularLimites()
    {
        alturaCamera = cam.orthographicSize;
        larguraCamera = alturaCamera * cam.aspect;

        config.minBounds = new(
            transform.position.x - (config.tamanhoSala.x / 2 - larguraCamera),
            transform.position.y - (config.tamanhoSala.y / 2 - alturaCamera),
            transform.position.z
        );

        config.maxBounds = new(
            transform.position.x + (config.tamanhoSala.x / 2 - larguraCamera),
            transform.position.y + (config.tamanhoSala.y / 2 - alturaCamera),
            transform.position.z
        );
    }

    private void LateUpdate()
    {
        if (camDinamica)
        {
            if (player == null) return;

            Vector3 posicaoAlvo = transform.position;

            if (Mathf.Abs(player.position.x - transform.position.x) > config.margemMovimento.x)
            {
                posicaoAlvo.x = player.position.x;
            }

            float diffY = player.position.y - transform.position.y;
            if (Mathf.Abs(diffY) > config.margemMovimento.y)
            {
                float velocidadeDescida = (diffY < 0) ? 170f : 200f;
                posicaoAlvo.y += diffY * velocidadeDescida * Time.deltaTime * config.suavizacao * 5f;
            }

            posicaoAlvo.x = Mathf.Clamp(posicaoAlvo.x, config.minBounds.x, config.maxBounds.x);
            posicaoAlvo.y = Mathf.Clamp(posicaoAlvo.y, config.minBounds.y, config.maxBounds.y);

            transform.position = Vector3.Lerp(transform.position, posicaoAlvo, 5f * config.suavizacao * Time.deltaTime);
        }
        else
        {
            Vector3 posicaoAlvo = transform.position;
            if (Mathf.Abs(player.position.y - transform.position.y) > config.margemMovimento.y)
            {
                posicaoAlvo.y = player.position.y;
            }
            posicaoAlvo.y = Mathf.Clamp(posicaoAlvo.y, config.minBounds.y, config.maxBounds.y);
            transform.position = Vector3.Lerp(transform.position, posicaoAlvo, 5f * config.suavizacao * Time.deltaTime);
        }
    }

    private void OnDrawGizmos()
    {
        if (!Application.isPlaying && cam != null)
        {
            CalcularLimites();
        }

        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, new Vector3(config.tamanhoSala.x, config.tamanhoSala.y, 0));

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, new Vector3(config.margemMovimento.x * 2, config.margemMovimento.y * 2, 0));
    }
}

[Serializable]
public struct CameraConfig
{
    public Vector2 margemMovimento;
    public float suavizacao;
    public Vector2 tamanhoSala;

    [NonSerialized] public Vector3 minBounds, maxBounds;
}