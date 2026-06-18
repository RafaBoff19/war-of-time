using UnityEngine;

public class InimigoPerseguidor : MonoBehaviour
{
    public Transform alvo;
    public float velocidade = 2f;
    public float distanciaSeparacao = 1.2f;
    public float forcaSeparacao = 5f;

    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private bool olhandoDireita = true;
    private float temporizadorFlip = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        GameObject jogador = GameObject.FindWithTag("Player");
        if (jogador != null)
            alvo = jogador.transform;
    }

    void FixedUpdate()
    {
        if (alvo == null) return;

        Vector2 direcaoAlvo = (alvo.position - transform.position).normalized;
        Vector2 separacao = CalcularSeparacao();

        Vector2 direcaoFinal = (direcaoAlvo + separacao * forcaSeparacao).normalized;

        rb.linearVelocity = direcaoFinal * velocidade;

        if (animator != null)
        {
            animator.SetFloat("movementX", direcaoFinal.x);
            animator.SetFloat("movementY", direcaoFinal.y);
        }

        AtualizarFlip(direcaoAlvo.x); // usa a direção do ALVO, não a direção final com separação
    }

    void AtualizarFlip(float direcaoX)
    {
        temporizadorFlip += Time.fixedDeltaTime;

        // só permite trocar de lado a cada 0.3 segundos, evitando tremedeira
        if (temporizadorFlip < 0.3f) return;

        if (direcaoX > 0.3f && !olhandoDireita)
        {
            olhandoDireita = true;
            spriteRenderer.flipX = false;
            temporizadorFlip = 0f;
        }
        else if (direcaoX < -0.3f && olhandoDireita)
        {
            olhandoDireita = false;
            spriteRenderer.flipX = true;
            temporizadorFlip = 0f;
        }
    }

    Vector2 CalcularSeparacao()
    {
        Vector2 empurrao = Vector2.zero;
        GameObject[] inimigos = GameObject.FindGameObjectsWithTag("Inimigo");

        foreach (GameObject outro in inimigos)
        {
            if (outro == gameObject) continue;

            float distancia = Vector2.Distance(transform.position, outro.transform.position);

            if (distancia < distanciaSeparacao && distancia > 0)
            {
                Vector2 direcaoFuga = (transform.position - outro.transform.position).normalized;
                empurrao += direcaoFuga / distancia;
            }
        }

        return empurrao;
    }
}