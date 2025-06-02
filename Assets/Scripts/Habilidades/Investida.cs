using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Investida : Habilidade
{
    public Rigidbody2D rigidBody;
    public playerC playerC;

    public float velocidade;
    float direcao = 1;
    //tempoCooldown = 1;

    public int totalDeFrames = 30;
    int contadorFrames = 0;
    void Start()
    {
        
    }

    public override void Ativar()
    {
        Debug.Log("investida ativada!");
        direcao = playerC.getDirecao();
        
        StartCoroutine(CourotinaInvestida());
    }
    
    private IEnumerator CourotinaInvestida()
    {
        while(contadorFrames < totalDeFrames)
        {
            Debug.Log("contador frames = " + contadorFrames + " vel = " + velocidade + "dir = " + direcao);
            //colocar algo pra travar o movimento enquanto a investida ocorre

            rigidBody.linearVelocity = new Vector2(direcao * velocidade, playerC.ridi.linearVelocityY);
            contadorFrames++;
            yield return null;
            
        }
        Debug.Log("coroutina finalizada");
        contadorFrames = 0;


    }

}
