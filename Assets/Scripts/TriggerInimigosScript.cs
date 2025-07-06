using Unity.VisualScripting;
using UnityEngine;

public class TriggerInimigosScript : MonoBehaviour
{
    public playerC player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (player.PodeAtravessarInimigos())
        {
            if (collision.collider.CompareTag("Inimigo")) // Quando colidir com inimigo perde 10 de vida
            {
                player.EmContatoComInimigo();
            }
        }
            
    }
}
