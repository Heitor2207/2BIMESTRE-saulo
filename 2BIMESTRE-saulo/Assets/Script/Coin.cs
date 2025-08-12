using UnityEngine;

public class Moeda : MonoBehaviour
{
    public int valor = 1; // valor da moeda

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player")) // verifica se foi o jogador
        {
            // Aqui você pode somar pontos se já tiver um sistema de pontuação
            // Exemplo: Pontuacao.instancia.AdicionarPontos(valor);

            Destroy(gameObject); // remove a moeda da cena
        }
    }
}