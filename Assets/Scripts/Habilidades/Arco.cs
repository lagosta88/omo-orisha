using UnityEngine;

public class Arco : Habilidade
{

    public GameObject flecha;
    private Vector3 posicaoInicial;
    public GameObject player;
    public float velocidadeYLvl2;
    public float velocidadeYLvl3;
    public float tempoDeVida;

    public float offsetX;
    public float offsetY;

    new void Start()
    {
        base.Start();

        flecha.GetComponent<flechaDeOxossi>().player = player.GetComponent<playerC>();
        flecha.GetComponent<flechaDeOxossi>().area = player.GetComponentInChildren<AreaDetectora>();
    }
    public override void Ativar()
    {
        base.Ativar();

        habilidadeAtiva = true;

        //animacao
        switch (nivel)
        {
            
            case 1:
                animator.SetTrigger("ArcoLvl1");
                break;
            case 2:
                animator.SetTrigger("ArcoLvl2");
                break;
            case 3:
                animator.SetTrigger("ArcoLvl3");
                break;

            default: animator.SetTrigger("ArcoLvl3");
                break;
        }
   
        //flechas sao disparadas pela animacao ao chamar AtirarFlechas
        
    }

    public void AtirarFlechas()//chamado pela animacao
    {

<<<<<<< HEAD
        posicaoInicial = player.transform.position + new Vector3(offsetX*player.GetComponent<playerC>().getDirecao(),offsetY, 0);
=======
        posicaoInicial = player.transform.position + new Vector3(offsetX, offsetY, 0);
>>>>>>> origin/johnsson2

        if (nivel >= 1)
        {

            GameObject flecha1 = Instantiate(flecha, posicaoInicial, Quaternion.identity);
            Destroy(flecha1, tempoDeVida);
        }

        if (nivel >= 2)
        {

            GameObject flecha2 = Instantiate(flecha, posicaoInicial, Quaternion.identity);

            flecha2.GetComponent<flechaDeOxossi>().velocidadeInicialEmY = velocidadeYLvl2;
            Destroy(flecha2, tempoDeVida);


            GameObject flecha3 = Instantiate(flecha, posicaoInicial, Quaternion.identity);

            flecha3.GetComponent<flechaDeOxossi>().velocidadeInicialEmY = -velocidadeYLvl2;
            Destroy(flecha3, tempoDeVida);


        }

        if (nivel >= 3)
        {

            GameObject flecha4 = Instantiate(flecha, posicaoInicial, Quaternion.identity);

            flecha4.GetComponent<flechaDeOxossi>().velocidadeInicialEmY = velocidadeYLvl3;
            Destroy(flecha4, tempoDeVida);


            GameObject flecha5 = Instantiate(flecha, posicaoInicial, Quaternion.identity);

            flecha5.GetComponent<flechaDeOxossi>().velocidadeInicialEmY = -velocidadeYLvl3;
            Destroy(flecha5, tempoDeVida);


        }
        AudioManager.instance.TocarSom(AudioManager.instance.somArco);

    }

    public void FinalizarArco() //chamado pela animacao
    {
        base.FimDaHabilidade();
    }



}


