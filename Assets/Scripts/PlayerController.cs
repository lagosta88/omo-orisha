using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mono : MonoBehaviour
{

    public UIvida Madd;

    public GameObject alvoobject;
    SpriteRenderer meuspriteRenderer;
    Transform posicao;
    Rigidbody2D ridi;
    private float move;
    public float velon;
    private int twojump;
    private bool ChaoS;

    // Start is called before the first frame update
    void Start()
    {
        meuspriteRenderer = GetComponent<SpriteRenderer>();
        posicao = GetComponent<Transform>();
        ridi = GetComponent<Rigidbody2D>();
        velon = 10;
        twojump = 0;
      //  posicao.localPosition = new Vector3(0,0,0);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (ChaoS)
        twojump = 0;
        // zera os jumps sempre que toca o chao

        if(Input.GetAxis("Horizontal") <=1){
        move = Input.GetAxis("Horizontal");
        Movimento(move);}
        // nesse movimento voce pode ajustar a velocidade da direção, menor para esquerda valores maiores menor para direita valores decimais

        if(Input.GetKeyDown("space") && (ChaoS || twojump > 0)){
        if(ChaoS || twojump < 1)
        pulou();}
        //  tem doublejump
        
    }

    void pulou(){
        ridi.AddForce(new Vector2(ridi.linearVelocity.x,100));
        twojump++;
        ChaoS = false;
        Debug.Log("jump!");
    }


    void Movimento(float move){
        ridi.linearVelocity = new Vector2( move * velon, ridi.linearVelocity.y);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Chao"))
            ChaoS = true;
        
        if (collision.collider.CompareTag("Inimigo"))
            Madd.Dano(10);

        if (collision.collider.CompareTag("Cura"))
            Madd.Cura(10);
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Chao"))
        {
            ChaoS = false;
        }
    }

}

