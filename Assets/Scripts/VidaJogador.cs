using UnityEngine;

public class VidaJogador : MonoBehaviour
{
    public int vidaMaxima = 100;
    public int vidaAtual;

    private Animator animator;

    void Start()
    {
        vidaAtual = vidaMaxima;
        animator = GetComponent<Animator>();
    }

    public void ReceberDano(int dano)
    {
        vidaAtual -= dano;

        if (vidaAtual <= 0)
        {
            Morrer();
        }
    }

    void Morrer()
    {
        animator.SetTrigger("Morrer");

        GameOver gameOver = FindObjectOfType<GameOver>();
        if (gameOver != null)
            gameOver.MostrarGameOver();

        GetComponent<PlayerMovement>().enabled = false;
        GetComponent<AtaqueJogador>().enabled = false;
    }
}