//SCRIPT DE TESTE PARA TESTAR DETECCAO DE INIMIGOS

//Attach this script to your GameObject. This GameObject doesn’t need to have a Collider component
//Set the Layer Mask field in the Inspector to the layer you would like to see collisions in (set to Everything if you are unsure).
//Create a second Gameobject for testing collisions. Make sure your GameObject has a Collider component (if it doesn’t, click on the Add Component button in the GameObject’s Inspector, and go to Physics>Box Collider).
//Place it so it is overlapping your other GameObject.
//Press Play to see the console output the name of your second GameObject

//This script uses the OverlapBox that creates an invisible Box Collider that detects multiple collisions with other colliders. The OverlapBox in this case is the same size and position as the GameObject you attach it to (acting as a replacement for the BoxCollider component).

using UnityEngine;

public class AreaDetectoraDeInimigos : MonoBehaviour
{   
    public playerC player;
    public LayerMask m_LayerMask;

    [Range(-100, 100)]
    public float deslocamentoX;
    [Range(-100, 100)]
    public float deslocamentoY;
    [Range(-100, 100)]
    public float escala;

    public Collider2D[] hitColliders;
    
    void FixedUpdate()
    {
        MyCollisions();
    }

    
    

    void MyCollisions()
    {



        // Use the OverlapBox to detect if there are any other colliders within this box area.
        // Use the GameObject's center, half the size (as a radius), and rotation. This creates an invisible box around your GameObject.
        hitColliders = Physics2D.OverlapBoxAll(gameObject.transform.position + new Vector3(deslocamentoX, deslocamentoY, 0) * player.getDirecao(), transform.localScale * escala / 2, 0 , m_LayerMask);
        int i = 0;
        // Check when there is a new collider coming into contact with the box
        while (i < hitColliders.Length)
        {
            // Output all of the collider names
            //Debug.Log("Hit : " + hitColliders[i].name + i); //printar inimigos detectados na área
            // Increase the number of Colliders in the array
            i++;
        }

       
    }

    // Draw the Box Overlap as a gizmo to show where it currently is testing. Click the Gizmos button to see this.
    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        // Check that it is being run in Play Mode, so it doesn't try to draw this in Editor mode
        if (Application.isPlaying)
            // Draw a cube where the OverlapBox is (positioned where your GameObject is as well as a size)
            Gizmos.DrawWireCube(transform.position + new Vector3(deslocamentoX, deslocamentoY, 0) * player.getDirecao(), transform.localScale*escala);
    }


    public GameObject GetClosestEnemy() //retorna o inimigo mais proximo do jogador na area de detectora de inimigos (fica a frente dele)
    {
        if (hitColliders != null)
        {
            Collider2D bestTarget = null;
            float closestDistanceSqr = Mathf.Infinity;
            Vector3 currentPosition = transform.position;
            foreach (Collider2D potentialTarget in hitColliders)
            {
                Vector3 directionToTarget = potentialTarget.gameObject.transform.position - currentPosition;
                float dSqrToTarget = directionToTarget.sqrMagnitude;
                if (dSqrToTarget < closestDistanceSqr)
                {
                    closestDistanceSqr = dSqrToTarget;
                    bestTarget = potentialTarget;
                }
            }
            if (bestTarget != null)
            {
                return bestTarget.gameObject;
            }
            else return null;
        }
        else return null;
    }


}