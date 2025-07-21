using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class IconeUpgrade : MonoBehaviour
{
    public TextMeshProUGUI descricaoHab;
    public SelecaoDeUpgrade selecaoDeUpgrade;
    public int numIcone;
    public Sprite iconeVazio;
    public GerenciadorCenario gerenciadorCenario;
    public Habilidade habilidadeUpgrade;
    public bool seted = false;

    void OnEnable()
    {
        SelecaoDeUpgrade.OnInicioTelaDeUpgrade += AtualizarIcone;
    }

    void OnDisable()
    {
        SelecaoDeUpgrade.OnInicioTelaDeUpgrade -= AtualizarIcone;
        descricaoHab.text = string.Empty;
        seted = false;
    }

    public void UpgradeBotao()
    {
        Debug.Log("botao apertado!");

        habilidadeUpgrade = selecaoDeUpgrade.ListaHab[numIcone];
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
        habilidadeUpgrade = selecaoDeUpgrade.ListaHab[numIcone];
        seted = true;

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
