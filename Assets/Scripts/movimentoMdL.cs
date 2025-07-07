using UnityEngine;

public class movimentoMdL : MonoBehaviour
{
    public float speed = 2f;
    public float deslizeSubidaSpeed = 4f;
    public float tempoSubida = 2f;
    public float alturaMinimaDeslizar = 6f;
    public VidaInimigo Slider;
    public AtkHitBox hitbox;

    public Transform pontoEsquerdo;
    public Transform pontoDireito;
    public Transform pontoAlto;

    private Rigidbody2D rb;
    private SpriteRenderer sr;

    private enum BossState { Subir, Voar, Cair, Parado, deslizar }
    private BossState estado;

    private bool noChao = false;
    private float stateTimer = 0f;
    private float dir;

    private Vector2[] pontosParada = new Vector2[]
    {
        new Vector2(-13.3f, 2.5f),
        new Vector2(1.6f, 2.3f),
        new Vector2(15.4f, 2.5f)
    };

    private Vector2 destinoParada;
    private bool indoParaDestino = false;
    private bool subindoAntesDoDeslize = false;
    private float tempoInicioSubida = 0f;
    private bool esperandoAposDeslizar = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        ChangeState(BossState.Subir);
    }

    void FixedUpdate()
    {
        if (Slider.vidaatual <= 0)
        {
            Destroy(gameObject);
            return;
        }

        float direcao = transform.position.x < 0 ? 1 : -1;

        // Transições automáticas
        if ((estado == BossState.Subir || estado == BossState.Voar) && Time.time >= stateTimer)
        {
            ChangeState(estado == BossState.Subir ? BossState.Voar : BossState.Cair);
        }
        else if (estado == BossState.Parado && Time.time >= stateTimer)
        {
            if (Random.value < 0.5f)
                ChangeState(BossState.Subir);
            else
                ChangeState(BossState.deslizar);
        }
        else if (estado == BossState.deslizar && esperandoAposDeslizar && Time.time >= stateTimer)
        {
            esperandoAposDeslizar = false;
            if (Random.value < 0.5f)
            {
                ChangeState(BossState.Subir);
            }
            else
            {
                ChangeState(BossState.deslizar);
            }
        }

        // Lógica de deslizar
        if (estado == BossState.deslizar)
        {
            if (subindoAntesDoDeslize)
{
            rb.gravityScale = 0;
            rb.linearVelocity = Vector2.up * deslizeSubidaSpeed;

        if (transform.position.y >= alturaMinimaDeslizar)
            {
            destinoParada = pontosParada[Random.Range(0, pontosParada.Length)];
            indoParaDestino = true;
            subindoAntesDoDeslize = false;
            rb.linearVelocity = Vector2.zero;

        }
}

            else if (indoParaDestino)
{
    Vector2 novaPos = Vector2.MoveTowards(rb.position, destinoParada, speed * 3f * Time.fixedDeltaTime);
    rb.MovePosition(novaPos);

    float distancia = Vector2.Distance(transform.position, destinoParada);

    if (distancia < 0.9f)
    {
        rb.position = destinoParada; // força posição exata
        indoParaDestino = false;
        esperandoAposDeslizar = true;
        stateTimer = Time.time + 5f;
        rb.linearVelocity = Vector2.zero;
    }
}

        }

        // Movimentação por estado
        switch (estado)
        {
            case BossState.Subir:
                rb.gravityScale = 0;
                rb.linearVelocity = Vector2.up * speed * 2f;
                dir = direcao;
                break;

            case BossState.Voar:
                rb.gravityScale = 0;
                rb.linearVelocity = new Vector2(dir * speed * 6f, 0);
                break;

            case BossState.Cair:
                rb.gravityScale = 1;
                rb.linearVelocity = Vector2.down * speed * 3f;
                if (noChao)
                    ChangeState(BossState.Parado);
                break;

            case BossState.Parado:
                rb.linearVelocity = Vector2.zero;
                break;

            case BossState.deslizar:
                // movimento já tratado acima
                break;
        }
    }

    void Update()
    {
        var player = GameObject.FindWithTag("Player");
        if (player)
            sr.flipX = player.transform.position.x < transform.position.x;
    }

    private void ChangeState(BossState novoEstado)
    {
        estado = novoEstado;

        switch (novoEstado)
        {
            case BossState.Subir:
                stateTimer = Time.time + 5f;
                break;

            case BossState.Voar:
                stateTimer = Time.time + 5f;
                break;

            case BossState.Cair:
                break;

            case BossState.Parado:
                stateTimer = Time.time + 5f;
                break;

            case BossState.deslizar:
                subindoAntesDoDeslize = true;
                tempoInicioSubida = Time.time;
                rb.linearVelocity = Vector2.zero;
                esperandoAposDeslizar = false;
                break;
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.CompareTag("Chao"))
            noChao = true;
    }

    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.collider.CompareTag("Chao"))
            noChao = false;
    }
}
