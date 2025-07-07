using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class IconeTambor : MonoBehaviour
{
    
    protected TamborDeHabilidades tambor;
    public int numIcone; //identifica qual icone eh esse (icone de cima = 0)
    public int posRoda; //identifica a posicao na roda para saber para onde o icone vai quando ele girar (vai de 0 a 15)
   
   


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tambor = GetComponentInParent<TamborDeHabilidades>();
        AtualizarIcone();
        SelecaoDeUpgrade.OnUpgrade += ChamarAtualizacaoDeIcone;
        TamborDeHabilidades.OnGirandoEsquerda += GirarParaEsquerda;
        TamborDeHabilidades.OnGirandoDireita += GirarParaDireita;
        GetComponentInParent<playerC>();

        GetComponent<RectTransform>().anchoredPosition = tambor.pos[posRoda];

    }

    // Update is called once per frame
    void Update()
    {

        


    }


    void ChamarAtualizacaoDeIcone() //assegura que a habilidade ja foi selecionada na tela de upgrade antes de atualizar o icone
    {
        Invoke("AtualizarIcone", 0.3f);
    }
    void AtualizarIcone() //atualiza o sprite do icone baseada na habilidade alocada
    {
        Image icone = GetComponent<Image>();

        if (tambor.roda[numIcone] != null)
        {
            icone.sprite = tambor.roda[numIcone].icone;
        }
        else
        {
            icone.sprite = tambor.iconeVazio;
        }
        
       
    }

 

    void GirarParaEsquerda()
    {
        if (posRoda > 0)
        {
            posRoda--;
        }
        else
        {
            posRoda = 15;
        }

      

        GetComponent<RectTransform>().anchoredPosition = tambor.pos[posRoda];

    }
    void GirarParaDireita()
    {
        if (posRoda < 15)
        {
            posRoda++;
        }
        else
        {
            posRoda = 0;
        }



        GetComponent<RectTransform>().anchoredPosition = tambor.pos[posRoda];

    }

}
