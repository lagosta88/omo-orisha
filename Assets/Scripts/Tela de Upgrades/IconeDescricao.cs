using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class IconeDescricao : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] private IconeUpgrade esseIcone;
    private Button btt;

    void Awake()
    {
        btt = GetComponent<Button>();
        btt.onClick.AddListener(ButtonFuncao);
    }

    public void ButtonFuncao()
    {
        esseIcone.UpgradeBotao();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (esseIcone.seted)
            esseIcone.descricaoHab.text = esseIcone.habilidadeUpgrade.descricaoHabilidade;
    }

}
