using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsRoll : MonoBehaviour
{
    public float scrollSpeed = 50f;
    public float endY = 1000f; // Valor Y onde os créditos terminam
    public string sceneAfterCredits = "MainMenu";

    void Update()
    {
        transform.Translate(Vector3.up * scrollSpeed * Time.deltaTime);

        if (transform.position.y >= endY)
        {
            SceneManager.LoadScene(sceneAfterCredits);
        }
    }
}
