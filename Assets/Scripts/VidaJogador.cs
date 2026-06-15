using UnityEngine;

public class VidaJogador : MonoBehaviour
{
    public int vidaMaxima = 100;
    public int vidaAtual;

    void Start()
    {
        vidaAtual = vidaMaxima;
    }

    public void ReceberDano(int dano)
    {
        vidaAtual -= dano;
        Debug.Log("Vida do Portaluppi: " + vidaAtual);

        if (vidaAtual <= 0)
        {
            Morrer();
        }
    }

    void Morrer()
    {
    GameOver gameOver = FindObjectOfType<GameOver>();
    if (gameOver != null)
        gameOver.MostrarGameOver();

    gameObject.SetActive(false);
    }
}