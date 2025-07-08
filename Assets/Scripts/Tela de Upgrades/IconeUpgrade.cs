using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;

public class IconeUpgrade : MonoBehaviour
{

    public SelecaoDeUpgrade selecaoDeUpgrade;
    public int numIcone;
    public Sprite iconeVazio;
    public GerenciadorCenario gerenciadorCenario;

    void OnEnable()
    {
        SelecaoDeUpgrade.OnInicioTelaDeUpgrade += AtualizarIcone;
    }

    void OnDisable()
    {
        SelecaoDeUpgrade.OnInicioTelaDeUpgrade -= AtualizarIcone;
    }

    public void UpgradeBotao()
    {
        Debug.Log("botao apertado!");

        Habilidade habilidadeUpgrade = selecaoDeUpgrade.ListaHab[numIcone];
        if (habilidadeUpgrade != null)
        {
            selecaoDeUpgrade.UpgradeHabilidade(habilidadeUpgrade);
        }

        gerenciadorCenario.selecaoDeUpgrade.gameObject.SetActive(false);
        gerenciadorCenario.player.gameObject.SetActive(true);
    }
    void AtualizarIcone()
    {
        Image icone = GetComponent<Image>();

        if (selecaoDeUpgrade.ListaHab.Count > numIcone)
        {
            icone.sprite = selecaoDeUpgrade.ListaHab[numIcone].icone;
        }
        else
        {
            icone.sprite = iconeVazio;
        }

    }
}
