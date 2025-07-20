using UnityEngine;

public class PlayerColliderChao : MonoBehaviour
{

    public playerC player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.collider.CompareTag("Player") && !collision.collider.CompareTag("ProjetilAliado") && !collision.collider.CompareTag("ProjetilEnemy"))
        {
            player.ChaoS = true;
        }
    }
}
