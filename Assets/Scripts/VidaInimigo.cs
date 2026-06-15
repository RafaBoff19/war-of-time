using UnityEngine;

public class VidaInimigo : MonoBehaviour
{
    public int vidaMaxima = 5;
    public int vidaAtual;
    public int xpAoDarDrop = 10;

    void Start()
    {
        vidaAtual = vidaMaxima;
    }

    public void ReceberDano(int dano)
    {
        vidaAtual -= dano;

        if (vidaAtual <= 0)
        {
            // Entrega XP ao jogador antes de morrer
            GameObject jogador = GameObject.FindWithTag("Player");
            if (jogador != null)
            {
                NivelJogador nivel = jogador.GetComponent<NivelJogador>();
                if (nivel != null)
                    nivel.GanharXP(xpAoDarDrop);
            }

            Destroy(gameObject);
        }
    }
}
