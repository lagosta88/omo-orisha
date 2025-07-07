using UnityEngine;
using System.Collections.Generic;

public class SelecaoDeUpgrade : MonoBehaviour
{

    public List<Habilidade> ListaHab = new(); //lista com todas as habilidades disponiveis no jogo
    private int nivelMaximo = 3;
    //public TamborDeHabilidades tambor;


    public delegate void UpgradeChamado();
    public static event UpgradeChamado OnInicioTelaDeUpgrade; //evento que ocorre quando a tela de upgrade eh chamada

    public delegate void EscolherHabilidade();
    public static event EscolherHabilidade OnUpgrade; //evento que ocorre quando uma habilidade eh escolhida

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    /*void Update()
    {

        //TESTE - REMOVER DEPOIS

        if (Input.GetKeyDown(KeyCode.P))
        {
            TelaDeUpgrade();
        }


    }*/

    public void TelaDeUpgrade()
    {
        AleatorizarUpgrades();
        //OnInicioTelaDeUpgrade();
    }

    void AleatorizarUpgrades()
    {
        RemoverHabilidadesSeOTamborEstiverCheio();
        RemoverHabilidadesMaximizadas();

        for (int i = 0; i < ListaHab.Count; i++)
        {
            Habilidade temp = ListaHab[i];
            int randomIndex = Random.Range(i, ListaHab.Count);
            ListaHab[i] = ListaHab[randomIndex];
            ListaHab[randomIndex] = temp;
        }
    }

    void RemoverHabilidadesMaximizadas()
    {
        for (int i = ListaHab.Count - 1; i >= 0; i--)
        {
            if (ListaHab[i].nivel >= nivelMaximo)
            {
                Debug.Log("SelecaoDeUpgrades: habilidade " + ListaHab[i] + " foi removida da Pool de Habilidades que podem aparecer para receber upgrade");
                ListaHab.RemoveAt(i);
            }
        }
    }

    void RemoverHabilidadesSeOTamborEstiverCheio() //se o tambor estiver cheio, coloca apenas as habilidades disponiveis nele para serem aleatorizadas
    {
        //if(tambor.QuantidadeDeHabilidades() >= 4)
        {

            //ListaHab.Clear();

            //foreach(Habilidade hab in tambor.roda)
            //{
                //ListaHab.Add(hab);
            //}
        }
    }

   
    public void UpgradeHabilidade(Habilidade hab)
    {
        //int qtdHabilidades = tambor.QuantidadeDeHabilidades();

        //adiciona a habilidade na roda caso ela ainda nao estivesse la
        //if (hab.nivel == 0 && qtdHabilidades < 4){
            //tambor.roda[qtdHabilidades] = hab;
        //}

        //sobe o nivel da habilidade
        hab.nivel++;
        OnUpgrade();
        
    }
   
    //Apos ser criada a funcao de escolher uma habilidade:
        /*
        *  if(OnUpgrade != null)
            OnUpgrade();

        */
}
