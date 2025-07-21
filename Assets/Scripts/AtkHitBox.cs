using System;
using System.Collections.Generic;
using UnityEngine;

// Essa linha permite que não seja nescessário atrelar esse script ao GameObject no inspector para ver e manipular dados no inspector
// Basta colocar um atributo AtkHitBox no script controlador (playerC.cs no caso do player) que será possível alterar as informações
[Serializable]
public class AtkHitBox //Não pode derivar do MonoBehaviour
{
    [Header("Definições")]
    public List<Transform> centrosDeAtaque; //Cada elemento da lista é o centro de cada ataque do combo
    public List<Vector2> tamanhoDoAtaque; //Cada elemento da lista é o tamanho da hitbox do elemento selecionado
    public LayerMask layerDoAlvo; //Layer do inimigo caso atrele ao player, layer do player caso atrele ao inimigo
    public int comboAtual;
    public List<string> nomesTrigger; //Nome do trigger que ativará a animação de cada etapa do combo
    private int MaxCombo => centrosDeAtaque.Count - 1;
    //Sempre que MaxCombo é acessado ele se torna o último índice da lista. Evita acesso indevido às listas
    public bool player = false;
    public bool atacando = false;
    public bool ataqueHabilitado = true;

    [Header("Debug Visual")]
    [Space(5)]
    public bool visualizar;
    public int qualDebug; //Altere aqui para visualizar a hitbox correta do ataque
    public void VerificarAtk(float danoCausado)
    {
        MostrarCaixa(comboAtual);
        if (comboAtual > MaxCombo + 1) comboAtual = MaxCombo + 1;
        var hit = Physics2D.OverlapBoxAll(
            (Vector2)centrosDeAtaque[comboAtual].position,
            tamanhoDoAtaque[comboAtual],
            0f,
            layerDoAlvo
        );
 
        foreach (var item in hit)
        {
            Debug.Log($"Atingiu o objeto de nome {item.gameObject.name}");
            // Chamar aqui algo como:

            if (player)
<<<<<<< HEAD
            {
                if (item.gameObject.TryGetComponent( out InimigoGeral componente)){
                    componente.Slider.Dano((int)danoCausado);
                }
            }
            else
            {

=======
                item.gameObject.GetComponent<movimento_inimigo>().Slider.Dano((int)danoCausado);
            else
            {
                
>>>>>>> origin/menuUpdate
                item.gameObject.GetComponent<playerC>().Madd.Dano((int)danoCausado);

            }
            
        }
    }

    public void MostrarCaixa(int indiceDeComboDebug)
    {
        if (indiceDeComboDebug > MaxCombo) indiceDeComboDebug = MaxCombo;

        Vector2 origem = centrosDeAtaque[indiceDeComboDebug].position;
        Vector2 metade = tamanhoDoAtaque[indiceDeComboDebug] * 0.5f;

        Vector2 esqCima = origem + new Vector2(-metade.x, metade.y);
        Vector2 dirCima = origem + new Vector2(metade.x, metade.y);
        Vector2 dirBaixo = origem + new Vector2(metade.x, -metade.y);
        Vector2 esqBaixo = origem + new Vector2(-metade.x, -metade.y);

        Debug.DrawLine(esqCima, dirCima, Color.red);     // Topo
        Debug.DrawLine(dirCima, dirBaixo, Color.red);    // Direita
        Debug.DrawLine(dirBaixo, esqBaixo, Color.red);   // Base
        Debug.DrawLine(esqBaixo, esqCima, Color.red);    // Esquerda
    }

 
}
