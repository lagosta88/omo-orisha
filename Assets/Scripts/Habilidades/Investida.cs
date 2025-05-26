using Unity.VisualScripting;
using UnityEngine;

public class Investida : Habilidade
{
    public Rigidbody2D rigidBody;
    public playerC playerC;

    public float velocidade;
    float direcao;
    //tempoCooldown = 1;
    void Start()
    {
        float direcao = playerC.getDirecao();
    }

    public override void Ativar()
    {
        rigidBody.linearVelocity = new Vector2(direcao * velocidade, 0f);
    }
    
}
