using UnityEngine;

public class RecuperarInimigo : MonoBehaviour
{
    [SerializeField] private LayerMask layerInimigo;
    [SerializeField] private Transform cenario;
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == layerInimigo)
        {
            collision.gameObject.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
            collision.transform.position = cenario.position;
        }
    }
}
