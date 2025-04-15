using UnityEngine;

public class direcao : MonoBehaviour
{
 
    public static int getDirecao(GameObject objeto)
    {
        if (objeto.transform.localScale.x > 0) { return 1; } else { return -1; }
    }

}
