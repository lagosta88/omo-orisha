using System;
using UnityEngine;

[Serializable]
public class PlayerCombo : AtkHitBox
{
    [Header("Combo")]
    public float janelaDeCombo; //Quanto tempo você tem até pressionar o botão de ataque novamente
    [NonSerialized] public float momentoUltimoAtaque; //NonSerialized não mostra no inspector (evitar poluição)
    public bool comboDisponivel;
}
