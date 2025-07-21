using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Gerador : MonoBehaviour
{
   public GerenciadorCenario gerenciadorCenario;
   public InimigoGeral[] inimigos;
   public List<InimigoGeral> inimigosSpawnados = new();
   public int minimoDeInimigos = 1, maximoDeInimigos = 10;//ajuste o minimo e o maximo para o sorteio
   public float coolDownEntreSpawn;
   private int quantidade;

    public void IniciarSpawn(bool ehBoss = false)
   {
      quantidade = Random.Range(
         minimoDeInimigos + 3 * gerenciadorCenario.andar,
         maximoDeInimigos + 3 * gerenciadorCenario.andar
      );
      if (ehBoss) quantidade *= 4;
      StartCoroutine(RotinaSpawn());
   }

   IEnumerator RotinaSpawn()
   {
      int j = 0;
      Sala atual = gerenciadorCenario.salaAtual;
      if (atual.spawnPoints.Count > 0)
      {
         for (int i = 0; i < quantidade; i++)
         {
            InimigoGeral novo = Instantiate(
               inimigos[Random.Range(0, inimigos.Length)],
               atual.spawnPoints[j]
            );
            novo.SetupGerador(this);
            inimigosSpawnados.Add(novo);
            yield return new WaitForSeconds(coolDownEntreSpawn);
            j = (j + 1) % atual.spawnPoints.Count;
         }
      }
      else
         yield return null;
   }

   void Update()
   {
      if (Input.GetKeyDown(KeyCode.RightControl))
      {
         StopAllCoroutines();
         foreach (var x in inimigosSpawnados)
         {
            if (x != null)
               Destroy(x.gameObject);
         }
         inimigosSpawnados.Clear();
      }
    }
}
