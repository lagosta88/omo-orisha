using UnityEngine;
using UnityEngine.SceneManagement;

public class lore : MonoBehaviour
{
    // Nome da cena que será carregada
    public string nomeDaCena = "Cena2";

    void Update()
    {
        // Verifica se qualquer tecla foi pressionada
        if (Input.anyKeyDown)
        {
            SceneManager.LoadScene(nomeDaCena);
        }
    }
}
