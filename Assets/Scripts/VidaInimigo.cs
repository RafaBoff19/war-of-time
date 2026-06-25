using UnityEngine;

public class VidaInimigo : MonoBehaviour
{
    public int vidaMaxima = 30;
    public int vidaAtual;
    public int xpAoDarDrop = 10;

    public AudioClip somMorte;
    public AudioClip somSpawn;

    private Animator animator;
    private bool morto = false;
    private AudioSource audioSource;

    void Start()
    {
        vidaAtual = vidaMaxima;
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        // Toca som de spawn ao aparecer (volume mais baixo)
        if (audioSource != null && somSpawn != null)
        audioSource.PlayOneShot(somSpawn, 0.1f);
    }

    public void ReceberDano(int dano)
    {
        if (morto) return;

        vidaAtual -= dano;

        if (vidaAtual <= 0)
            Morrer();
    }

    void Morrer()
    {
        morto = true;

        // Entrega XP ao jogador
        GameObject jogador = GameObject.FindWithTag("Player");
        if (jogador != null)
        {
            NivelJogador nivel = jogador.GetComponent<NivelJogador>();
            if (nivel != null)
                nivel.GanharXP(xpAoDarDrop);
        }

        // Para movimento e física
        InimigoPerseguidor perseguidor = GetComponent<InimigoPerseguidor>();
        if (perseguidor != null) perseguidor.enabled = false;

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.bodyType = RigidbodyType2D.Static;
        }

        Collider2D col = GetComponent<Collider2D>();
        if (col != null) col.enabled = false;

        // Toca animação de morte
        if (animator != null)
            animator.Play("die");

        // Toca som de morte
        if (audioSource != null && somMorte != null)
            audioSource.PlayOneShot(somMorte);

        Destroy(gameObject, 1f);
    }
}