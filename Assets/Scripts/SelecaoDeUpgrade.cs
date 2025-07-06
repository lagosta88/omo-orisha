using UnityEngine;

public class SelecaoDeUpgrade : MonoBehaviour
{

    public delegate void EscolherHabilidade();
    public static event EscolherHabilidade OnUpgrade; //evento que ocorre quando uma habilidade eh escolhida


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        


    }

    //Apos ser criada a funcao de escolher uma habilidade:
        /*
        *  if(OnUpgrade != null)
            OnUpgrade();

        */
}
