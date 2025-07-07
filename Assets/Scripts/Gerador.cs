using UnityEngine;
using System.Collections.Generic;

public class Gerador : MonoBehaviour {

   public GameObject LugarDoSpawn;
   public GameObject[] inimigos; // linke inimigos aqui
   public List<GameObject> inimigosSpawnados = new();
   public int minimoDeInimigos = 1, maximoDeInimigos = 10;//ajuste o minimo e o maximo para o sorteio
   private GameObject jogador;
   public float DistanciaParaSpawn = 10;
   private int quantidade;
   private bool realizarSpawn;

   void Start (){
      realizarSpawn = false;
      jogador = GameObject.FindWithTag ("Player");
      quantidade = Random.Range (minimoDeInimigos, maximoDeInimigos); // aqui acontece o sorteio da quantidade de inimigos
   }
   void Update () {
      if(Vector3.Distance (jogador.transform.position,transform.position) <= DistanciaParaSpawn && realizarSpawn == false){
         for (int x = 0; x < quantidade; x++) {
            inimigosSpawnados.Add(Instantiate (inimigos[Random.Range (0,inimigos.Length)],LugarDoSpawn.transform.position,transform.rotation)); // instancia um inimigo aleatorio
         }
         realizarSpawn = true;
      }
   }
}