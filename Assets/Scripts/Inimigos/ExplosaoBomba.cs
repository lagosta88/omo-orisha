using UnityEngine;
using static UnityEditor.Progress;

public class ExplosaoBomba : MonoBehaviour
{

    //public float tempoParaSumir; //destruicao agora eh baseada na animacao
    public int danoCausado;
    private bool jaLevouDano = false;
    private bool podeLevarDano = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Destroy(gameObject, tempoParaSumir);
    }

    
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && jaLevouDano == false && podeLevarDano == true)
        {
            collision.gameObject.GetComponent<playerC>().Madd.Dano(danoCausado);
            jaLevouDano = true;
        }
    }
    

    public void AtivarDano() //chamado pela animacao
    {
        podeLevarDano = true;
    }

    public void DesativarDano() //chamado pela animacao
    {
        podeLevarDano = false;
    }

    public void Destruir() //chamado ao final da animacao
    {
        Debug.Log("explosao foi destruida");
        Destroy(gameObject);
    }
}
