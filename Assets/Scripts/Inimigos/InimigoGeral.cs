using UnityEngine;

public class InimigoGeral : MonoBehaviour
{
    public VidaInimigo Slider;
    public Gerador gerador;

    public void SetupGerador(Gerador gerador)
    {
        this.gerador = gerador;
    }
}
