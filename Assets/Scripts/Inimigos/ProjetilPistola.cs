using UnityEngine;

public class ProjetilPistola : ProjetilEnemy
{
    Rigidbody2D rigidBody;
    public GameObject alvo; //player
    private Vector2 posicaoAlvo;
    private Vector2 direcao;
    public float velocidade;
    public int danoCausado;

    // Start is called once before the first execution of Update after the MonoBehaviour is created


    ProjetilPistola(GameObject alvo)
    {
        this.alvo = alvo;
        
    }
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();

        posicaoAlvo = alvo.transform.position;
        Vector2 posicaoInicial = rigidBody.position;

        direcao = posicaoAlvo - posicaoInicial;
        direcao.Normalize();

        rigidBody.linearVelocity = direcao * velocidade;

        ApontarParaTrajetoria(rigidBody);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<playerC>().Madd.Dano(danoCausado);
            Destroy(gameObject);
        }

        if(collision.collider.CompareTag("Chao") || collision.collider.CompareTag("parede"))
        {
            Destroy(gameObject);
        }
    }
}
