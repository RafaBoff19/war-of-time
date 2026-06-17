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

    GetComponent<PlayerMovement>().enabled = false;
    GetComponent<AtaqueJogador>().enabled = false;

    // Espera a animação de morte tocar antes de mostrar Game Over
    Invoke("AcionarGameOver", 1.2f);
    }

    void AcionarGameOver()
    {
    GameOver gameOver = FindObjectOfType<GameOver>();
    if (gameOver != null)
        gameOver.MostrarGameOver();
    }
}