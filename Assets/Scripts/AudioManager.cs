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
     public AudioClip somArco;
     public AudioClip somInvestida;
     public AudioClip somTerremoto;
     public AudioClip somTridente;
     public AudioClip somBombaAtk1;
     public AudioClip somBombaAtk2;
     public AudioClip somBombaAtk3;
     public AudioClip somBombaDano;
     public AudioClip somPistolaAtk;
     public AudioClip somPistolaDano;
    public AudioClip somPistolaAnda;
     public AudioClip somSegurançaAtk;
     public AudioClip somSegurançaDano;
     public AudioClip somSegurançaAnda;
     public AudioClip somSerraAtk;
    public AudioClip somSerraDano;
    public AudioClip somSerraAnda;


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
