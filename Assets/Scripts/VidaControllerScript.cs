using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VidaControllerScript : MonoBehaviour
{
    public UIvida uiVida;
    public Sprite vidaCheia;
    public Sprite vidaVazia;
    public float valorCoracao;

    private Image[] vidaImages;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        vidaImages = GetComponentsInChildren<Image>();

    }

    // Update is called once per frame
    void Update()
    {
        for(int i=0; i<vidaImages.Length; i++)
        {
            if(uiVida.vidaatual >= valorCoracao * (i + 1))
            {
                vidaImages[i].sprite = vidaCheia;
            }
            else
            {
                vidaImages[i].sprite = vidaVazia;
            }
        }
    }
}
