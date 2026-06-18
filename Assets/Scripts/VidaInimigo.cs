using UnityEngine;

public class VidaInimigo : MonoBehaviour
{
    public int vidaMaxima = 30;
    public int vidaAtual;
    public int xpAoDarDrop = 10;

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

        if (vidaAtual <= 0)
        {
            Morrer();
        }
    }

    void Morrer()
    {
    morto = true;

    GameObject jogador = GameObject.FindWithTag("Player");
    if (jogador != null)
    {
        NivelJogador nivel = jogador.GetComponent<NivelJogador>();
        if (nivel != null)
            nivel.GanharXP(xpAoDarDrop);
    }

    GetComponent<InimigoPerseguidor>().enabled = false;

    Rigidbody2D rb = GetComponent<Rigidbody2D>();
    rb.linearVelocity = Vector2.zero;
    rb.bodyType = RigidbodyType2D.Static; // trava completamente a física

    GetComponent<Collider2D>().enabled = false;

    if (animator != null)
        animator.Play("die");

    Destroy(gameObject, 1f);
    }
}