using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [Header("Painéis")]
    public GameObject mainMenuPanel;
    public GameObject optionsPanel;
    public GameObject creditsPanel;

    [Header("Botões")]
    public Button startButton;
    public Button optionsButton;
    public Button closeOptionsButton;
    public Button creditsButton;
    public Button closeCreditsButton;
    public Button quitButton;

    [Header("Controle de Áudio")]
    public Slider volumeSlider;

    [Header("Som de Botão")]
    public AudioSource audioSource;
    public AudioClip clickSound;

    void Start()
    {
        // Mostrar apenas o painel principal
        mainMenuPanel.SetActive(true);
        optionsPanel.SetActive(false);
        creditsPanel.SetActive(false);

        // Conectar botões com som
        if (startButton != null)
            startButton.onClick.AddListener(() => { PlayClickSound(); StartGame(); });

        if (optionsButton != null)
            optionsButton.onClick.AddListener(() => { PlayClickSound(); OpenOptions(); });

        if (closeOptionsButton != null)
            closeOptionsButton.onClick.AddListener(() => { PlayClickSound(); CloseOptions(); });

        if (creditsButton != null)
            creditsButton.onClick.AddListener(() => { PlayClickSound(); OpenCredits(); });

        if (closeCreditsButton != null)
            closeCreditsButton.onClick.AddListener(() => { PlayClickSound(); CloseCredits(); });

        if (quitButton != null)
            quitButton.onClick.AddListener(() => { PlayClickSound(); QuitGame(); });

        // Conectar slider de volume
        if (volumeSlider != null)
        {
            volumeSlider.value = AudioListener.volume;
            volumeSlider.onValueChanged.AddListener(SetVolume);
        }
    }

    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
    }

    public void StartGame()
    {
        Debug.Log("Iniciando o jogo...");
        SceneManager.LoadScene("lores"); // Substitua por sua cena real
    }

    public void OpenOptions()
    {
        optionsPanel.SetActive(true);
    }

    public void CloseOptions()
    {
        optionsPanel.SetActive(false);
    }

    public void OpenCredits()
    {
        creditsPanel.SetActive(true);
    }

    public void CloseCredits()
    {
        creditsPanel.SetActive(false);
    }

    public void QuitGame()
    {
        Debug.Log("Sair do Jogo");
        Application.Quit();
    }

    void PlayClickSound()
    {
        if (audioSource != null && clickSound != null)
            audioSource.PlayOneShot(clickSound);
    }
}
