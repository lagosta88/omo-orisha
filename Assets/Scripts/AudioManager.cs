using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
     [Header("Efeitos Sonoros")]
    public AudioClip somDor;
    public AudioClip somFalha;
    public AudioClip somChoro;
    public AudioClip somErro;
    public AudioClip somEsforcoSoco;
    public AudioClip somEsforco;
    public AudioClip somPulo;
     public AudioClip somPosPulo;
    private AudioSource audioSource;

    void Awake()
    {
        // Singleton para acessar o manager de qualquer lugar
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        audioSource = GetComponent<AudioSource>();
    }

    public void TocarSom(AudioClip som)
    {
        if (som != null)
            audioSource.PlayOneShot(som);
    }
}
