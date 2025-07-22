using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Game_over : MonoBehaviour
{

    public string nomeDaCena1 = "Cena1";
    public string nomeDaCena2 = "Cena2";

    [Header("Botões")]
    public Button MenuButton;
    public Button RestartButton;
    
    [Header("Som de Botão")]
    public AudioSource audioSource;
    public AudioClip clickSound;

    void Start()
    {
        // Conectar botões com som
        if (MenuButton != null)
            MenuButton.onClick.AddListener(() => { PlayClickSound(); Voltar(); });
            
            if (RestartButton != null)
            RestartButton.onClick.AddListener(() => { PlayClickSound(); StartGame(); });

    }

    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
    }

    public void Voltar()
    {
        SceneManager.LoadScene(nomeDaCena1);
    }
    public void StartGame()
    {
        SceneManager.LoadScene(nomeDaCena2);
    }

    void PlayClickSound()
    {
        if (audioSource != null && clickSound != null)
            audioSource.PlayOneShot(clickSound);
    }
}
