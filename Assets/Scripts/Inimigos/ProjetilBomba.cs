using UnityEngine;

public class ProjetilBomba : ProjetilEnemy
{

    Rigidbody2D rigidBody;
 
    public GameObject alvo; //player
    public float velocidadeY;
    private float velocidadeX;

    private Vector2 posicaoAlvo;
    private Vector2 direcao;
    private float distancia;
    private float gravidade = -9.81f;
    private float sentido;
    public GameObject explosao;
    public Vector3 deslocamentoExplosao;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();

        posicaoAlvo = alvo.transform.position;
        Vector2 posicaoInicial = rigidBody.position;

        direcao = posicaoAlvo - posicaoInicial;
        if (direcao.x > 0)
        {
            sentido = 1;
        }
        else sentido = -1;

        distancia = direcao.magnitude;

        

        float tempoTotal = -2 * velocidadeY / gravidade;
        float velocidadeX = distancia/tempoTotal;


        rigidBody.linearVelocity = new Vector2(velocidadeX*sentido, velocidadeY);

    }

    // Update is called once per frame
    void Update()
    {
        ApontarParaTrajetoria(rigidBody);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player") || collision.collider.CompareTag("Chao") || collision.collider.CompareTag("parede"))
        {
            GameObject explosaoCriada = Instantiate(explosao, rigidBody.transform.position + deslocamentoExplosao, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
