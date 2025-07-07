using UnityEngine;

public class ProjetilEnemy : MonoBehaviour
{
    
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    protected void ApontarParaTrajetoria(Rigidbody2D rigidbody)
    {
        Vector2 posicaoFutura = rigidbody.position + rigidbody.linearVelocity;

        Vector2 vetorAngulo = posicaoFutura - rigidbody.position;

        //Debug.Log("posicao futura: " + posicaoFutura);

        rigidbody.transform.eulerAngles = new Vector3(0, 0, calcularAngulo(vetorAngulo.x, vetorAngulo.y));

    }

    float calcularAngulo(float x, float y)
    {
        float anguloEmRad = Mathf.Atan2(y, x);
        float anguloEmGraus = anguloEmRad * Mathf.Rad2Deg;

        return anguloEmGraus;
    }

}
