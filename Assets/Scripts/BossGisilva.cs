using UnityEngine;

public class BossGisilva : MonoBehaviour
{
    [Header("Vida")]
    public int vidaMaxima = 300;
    public int vidaAtual;

    [Header("Movimento")]
    public float velocidade = 1.5f;

    [Header("Fase 2")]
    public GameObject prefabPeixeSombra;
    public int quantidadeInvocacao = 3;
    private bool fase2Ativada = false;

    [Header("Onda de Choque")]
    public GameObject prefabOnda;
    public float intervaloOnda = 3f;
    private float temporizadorOnda = 0f;

    private Transform jogador;
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        vidaAtual = vidaMaxima;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        GameObject obj = GameObject.FindWithTag("Player");
        if (obj != null)
            jogador = obj.transform;
    }

    void Update()
    {
        if (jogador == null) return;

        Vector2 direcao = (jogador.position - transform.position).normalized;
        rb.linearVelocity = direcao * velocidade;

        if (animator != null)
            animator.SetBool("walk", direcao.magnitude > 0.1f);

        if (spriteRenderer != null)
        {
            if (direcao.x > 0.1f) spriteRenderer.flipX = false;
            else if (direcao.x < -0.1f) spriteRenderer.flipX = true;
        }

        temporizadorOnda += Time.deltaTime;
        if (temporizadorOnda >= intervaloOnda)
        {
            LancarOndas();
            temporizadorOnda = 0f;
        }

        if (!fase2Ativada && vidaAtual <= vidaMaxima * 0.5f)
        {
            AtivarFase2();
        }
    }

    void LancarOndas()
    {
        if (prefabOnda == null) return;

        Debug.Log("Chamando attack02 - Lançar Ondas"); // linha temporária de teste

        if (animator != null)
        animator.SetTrigger("attack02");

        Vector2[] direcoes = { Vector2.up, Vector2.down, Vector2.left, Vector2.right };

        foreach (Vector2 dir in direcoes)
        {
        GameObject onda = Instantiate(prefabOnda, transform.position, Quaternion.identity);
        onda.GetComponent<OndaChoque>().Inicializar(dir);
        }
    }

    void AtivarFase2()
    {
        fase2Ativada = true;
        Debug.Log("BOSS - FASE 2! Invocando reforços!");

        // attack02 = invocar
        if (animator != null)
            animator.SetTrigger("attack02");

        for (int i = 0; i < quantidadeInvocacao; i++)
        {
            Vector2 pos = (Vector2)transform.position + Random.insideUnitCircle * 3f;
            Instantiate(prefabPeixeSombra, pos, Quaternion.identity);
        }
    }

    public void ReceberDano(int dano)
    {
        vidaAtual -= dano;

        if (animator != null)
            animator.SetTrigger("damage");

        if (vidaAtual <= 0)
            Morrer();
    }

    void Morrer()
    {
        Debug.Log("Boss derrotado!");
        Destroy(gameObject);
    }

    void OnTriggerStay2D(Collider2D outro)
    {
    if (outro.CompareTag("Player"))
    {
        VidaJogador vida = outro.GetComponent<VidaJogador>();
        if (vida != null)
            vida.ReceberDano(20);

        // animator.SetTrigger("attack"); // comentado temporariamente para teste
    }
    }
}