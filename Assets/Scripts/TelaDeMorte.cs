using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TelaDeMorte : MonoBehaviour
{

    public GameObject TelaPreta;
    public GameObject PaiObjetosTela;
    private SpriteRenderer spriteTela;
    private Image[] ImageObjetos;
    public float tempoDeAparecimento;
    private float contadorTempo;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteTela = TelaPreta.GetComponent<SpriteRenderer>();
        ImageObjetos = PaiObjetosTela.GetComponentsInChildren<Image>();

    
        StartCoroutine("EscurecerTela");



        float newAlpha = 0;
        Color alphaChanger = spriteTela.color;
        alphaChanger.a = newAlpha;
        spriteTela.color = alphaChanger;

        foreach (Image image in ImageObjetos)
        {
            newAlpha = 0;
            alphaChanger = image.color;
            alphaChanger.a = newAlpha;
            image.color = alphaChanger;
        }


    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator EscurecerTela()
    {
        while (contadorTempo < tempoDeAparecimento)
        {
            float newAlpha = Mathf.Lerp(0, 1,  contadorTempo / tempoDeAparecimento);
            Color alphaChanger = spriteTela.color;
            alphaChanger.a = newAlpha;
            spriteTela.color = alphaChanger;

            contadorTempo += Time.deltaTime;
            yield return null;
        }
        
        Debug.Log("coroutina finalizada - tela escurecida");
        contadorTempo = 0;
        StartCoroutine("AparecerElementos");

    }

    private IEnumerator AparecerElementos()
    {
        while (contadorTempo < tempoDeAparecimento)
        {
            foreach (Image image in ImageObjetos)
            {
                float newAlpha = Mathf.Lerp(0, 1, contadorTempo / tempoDeAparecimento);
                Color alphaChanger = image.color;
                alphaChanger.a = newAlpha;
                image.color = alphaChanger;
            }

            contadorTempo += Time.deltaTime;
            yield return null;
        }
        Debug.Log("coroutina finalizada - aparecer elementos");
        contadorTempo = 0;

    }

}
    /*
    StartCoroutine(CourotinaInvestida());

//Coroutina da investida (faz ela durar varios frames)
private IEnumerator CourotinaInvestida()
{
    while (contadorTempo < tempoTotal)
    {
        //Debug.Log("contador frames = " + contadorTempo + " vel = " + velocidade + "dir = " + direcao);
        //colocar algo pra travar o movimento enquanto a investida ocorre

        rigidBody.linearVelocity = new Vector2(direcao * velocidade, playerC.ridi.linearVelocityY);
        contadorTempo += Time.deltaTime;
        yield return null;

    }
    Debug.Log("coroutina finalizada");
    invulnerabilidade = false;
    atravessarInimigos = false;
    habilidadeAtiva = false;
    animator.SetTrigger("FimDaHabilidade");
    contadorTempo = 0;

    FimDaHabilidade();

}

    */