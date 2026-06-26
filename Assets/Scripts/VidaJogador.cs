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

    // Para a música
    if (GerenciadorMusica.instancia != null)
        GerenciadorMusica.instancia.PararMusica();

    // Esconde a barra do boss se estiver visível
    UIBoss uiBoss = FindObjectOfType<UIBoss>();
    if (uiBoss != null)
        uiBoss.EsconderBoss();

    // Toca som de morte
    SomJogador som = GetComponent<SomJogador>();
    if (som != null) som.TocarMorte();

    if (animator != null)
        animator.SetTrigger("Morrer");

    PlayerMovement mov = GetComponent<PlayerMovement>();
    if (mov != null) mov.enabled = false;

    AtaqueJogador atk = GetComponent<AtaqueJogador>();
    if (atk != null) atk.enabled = false;

    Invoke("AcionarGameOver", 1.5f);
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