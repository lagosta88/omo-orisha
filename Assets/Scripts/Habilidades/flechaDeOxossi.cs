using Unity.VisualScripting;
using UnityEngine;

public class flechaDeOxossi : MonoBehaviour
{

    public Rigidbody2D flecha;
    public AreaDetectora area;
    public playerC player;

    public float velocidadeInicialEmX;
    public float velocidadeInicialEmY;
    public float velocidadeFinal;

    public float aceleracaoY;

    public float tempoDeVida; //IMPLEMENTAR
    public float dano;

    private Vector2 posicaoFutura;

    private GameObject alvo;
    private float distanciaInicial = 1;
    private float distanciaAtual = 1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        flecha.linearVelocity = new Vector2(velocidadeInicialEmX*player.getDirecao() + player.ridi.linearVelocityX, velocidadeInicialEmY);

       alvo = area.GetClosestEnemy();

        if (alvo != null)
        {
            distanciaInicial = Mathf.Abs(alvo.transform.position.x - flecha.transform.position.x);
        }
    }
   

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("alvo = " + alvo);

        if (alvo == null) //nenhum inimigo detectado quando a flecha é spawnada
        {
            //seguira o caminho definido inicialmente pelo arco
        }
        else
        {
            distanciaAtual = Mathf.Abs(alvo.transform.position.x - flecha.transform.position.x);
            Vector2 posAlvo = alvo.transform.position;
            Vector2 posFlecha = flecha.transform.position;

            if (distanciaAtual <= distanciaInicial / 2)
            { //Modo Teleguiado

                Vector2 direcaoAlvo = (posAlvo - posFlecha);
                direcaoAlvo.Normalize();
                flecha.linearVelocity = direcaoAlvo * velocidadeFinal;
            }
            else //modo nao teleguiado
            {

                if (flecha.linearVelocityY < 0) { flecha.linearVelocity += new Vector2(0, aceleracaoY * Time.deltaTime); }
                else if (flecha.linearVelocityY > 0) { flecha.linearVelocity += new Vector2(0, -aceleracaoY * Time.deltaTime); }

            }
            /* ANTIGA LOGICA
            if (posAlvo.y > posFlecha.y) { flecha.linearVelocity += new Vector2(0, aceleracaoY * Time.deltaTime); } //vai pra cima se o alvo estiver acima da flecha
            else if (posAlvo.y < posFlecha.y) { flecha.linearVelocity += new Vector2(0, -aceleracaoY * Time.deltaTime); } //vai pra baixo se o alvo estiver abaixo da flecha
            */
        }

        ApontarParaTrajetoria();
    }


    void ApontarParaTrajetoria()
    {
        posicaoFutura = flecha.position + flecha.linearVelocity;

        Vector2 vetorAngulo = posicaoFutura - flecha.position;

        //Debug.Log("posicao futura: " + posicaoFutura);

        flecha.transform.eulerAngles = new Vector3(0, 0, calcularAngulo(vetorAngulo.x, vetorAngulo.y));

    }

    float calcularAngulo(float x, float y)
    {
        float anguloEmRad = Mathf.Atan2(y, x);
        float anguloEmGraus = anguloEmRad * Mathf.Rad2Deg;

        return anguloEmGraus;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
               
            if (collision.collider.CompareTag("Inimigo")) // Quando colidir com inimigo perde x de vida
            {
            Debug.Log("flecha: colisao com inimigo");
                if (collision.gameObject.TryGetComponent<InimigoGeral>(out InimigoGeral inimigo)) {
                inimigo.Slider.Dano((int)dano);
                Debug.Log("flecha: causou dano!");
            }
            Destroy(gameObject);
            }

        if (collision.collider.CompareTag("parede"))
            { // Quando colidir com chao a variavel fica true

            Destroy(gameObject);

            }
    }
 
}
