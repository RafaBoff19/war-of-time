using UnityEngine;

public class VidaJogador : MonoBehaviour
{
    public int vidaMaxima = 100;
    public int vidaAtual;

    private Animator animator;
    private bool morto = false;

    void Start()
    {
        vidaAtual = vidaMaxima;
        animator = GetComponent<Animator>();
    }

    public void ReceberDano(int dano)
    {
        if (morto) return;
        vidaAtual -= dano;

        SomJogador som = GetComponent<SomJogador>();
        if (som != null) som.TocarDano();

        if (vidaAtual <= 0)
            Morrer();
    }

    void Morrer()
    {
        if (morto) return;
        morto = true;

        SomJogador som = GetComponent<SomJogador>();
        if (som != null) som.TocarMorte();

        if (animator != null)
            animator.SetTrigger("Morrer");

        PlayerMovement mov = GetComponent<PlayerMovement>();
        if (mov != null) mov.enabled = false;

        AtaqueJogador atk = GetComponent<AtaqueJogador>();
        if (atk != null) atk.enabled = false;

        Debug.Log("Portaluppi morreu! Chamando Game Over...");
        Invoke("AcionarGameOver", 1.2f);
    }

    void AcionarGameOver()
    {
        Debug.Log("AcionarGameOver chamado!");
        GameOver gameOver = FindObjectOfType<GameOver>();
        if (gameOver != null)
        {
            Debug.Log("GameOver encontrado!");
            gameOver.MostrarGameOver();
        }
        else
        {
            Debug.Log("GameOver NAO encontrado!");
        }
    }
}