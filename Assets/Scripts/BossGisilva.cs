using UnityEngine;

public class BossGisilva : MonoBehaviour
{
    [Header("Vida")]
    public int vidaMaxima = 300;
    public int vidaAtual;

    [Header("Movimento")]
    public float velocidade = 1.5f;

    [Header("Ataque Corpo a Corpo")]
    public float alcanceAtaque = 2f;
    public int danoAtaque = 20;
    public float intervaloAtaqueCaC = 2f;
    private float temporizadorAtaque = 0f;
    private bool atacando = false;

    [Header("Onda de Choque")]
    public GameObject prefabOnda;
    public float intervaloOnda = 5f;
    private float temporizadorOnda = 0f;

    [Header("Fase 2")]
    public GameObject prefabPeixeSombra;
    public int quantidadeInvocacao = 3;
    private bool fase2Ativada = false;

    private Transform jogador;
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    // duração da animação de attack = 0.867s
    private float duracaoAnimAtaque = 0.867f;
    private float temporizadorAnimAtaque = 0f;
    private bool animAtaqueRodando = false;

    void Start()
    {
    vidaAtual = vidaMaxima;
    rb = GetComponent<Rigidbody2D>();
    animator = GetComponent<Animator>();
    spriteRenderer = GetComponent<SpriteRenderer>();

    // Começa o temporizador já "pronto" para atacar imediatamente
    temporizadorAtaque = intervaloAtaqueCaC;

    GameObject obj = GameObject.FindWithTag("Player");
    if (obj != null)
        jogador = obj.transform;
    }

    void Update()
    {
        if (jogador == null) return;

        float distancia = Vector2.Distance(transform.position, jogador.position);
        Vector2 direcao = (jogador.position - transform.position).normalized;

        // Vira o sprite
        if (spriteRenderer != null)
        {
            if (direcao.x > 0.1f) spriteRenderer.flipX = false;
            else if (direcao.x < -0.1f) spriteRenderer.flipX = true;
        }

        // Controla o temporizador da animação de ataque
        if (animAtaqueRodando)
        {
            temporizadorAnimAtaque += Time.deltaTime;
            if (temporizadorAnimAtaque >= duracaoAnimAtaque)
            {
                animAtaqueRodando = false;
                temporizadorAnimAtaque = 0f;
                atacando = false;

                // Volta para walk ou idle depois que a animação terminar
                if (animator != null)
                    animator.SetBool("walk", distancia > alcanceAtaque);
            }
            return; // não faz mais nada enquanto a animação de ataque roda
        }

        // Onda de choque (projéteis) — só lança se não estiver atacando
        temporizadorOnda += Time.deltaTime;
        if (temporizadorOnda >= intervaloOnda && !atacando)
        {
            LancarOndas();
            temporizadorOnda = 0f;
        }

        // Fase 2
        if (!fase2Ativada && vidaAtual <= vidaMaxima * 0.5f)
        {
            AtivarFase2();
        }

        // Ataque corpo a corpo
        if (distancia <= alcanceAtaque)
        {
            rb.linearVelocity = Vector2.zero;

            if (animator != null)
                animator.SetBool("walk", false);

            temporizadorAtaque += Time.deltaTime;
            if (temporizadorAtaque >= intervaloAtaqueCaC && !atacando)
            {
                atacando = true;
                animAtaqueRodando = true;
                temporizadorAnimAtaque = 0f;
                temporizadorAtaque = 0f;

                if (animator != null)
                    animator.Play("attack");

                // Dano no meio da animação (cajado batendo no chão ~0.45s)
                Invoke("CausarDanoCaC", 0.45f);
            }
        }
        else
        {
            // Persegue o jogador
            atacando = false;
            rb.linearVelocity = direcao * velocidade;

            if (animator != null)
                animator.SetBool("walk", true);
        }
    }

    void CausarDanoCaC()
    {
        if (jogador == null) return;

        float distancia = Vector2.Distance(transform.position, jogador.position);
        if (distancia <= alcanceAtaque)
        {
            VidaJogador vida = jogador.GetComponent<VidaJogador>();
            if (vida != null)
                vida.ReceberDano(danoAtaque);
        }
    }

    void LancarOndas()
    {
    if (prefabOnda == null) return;

    if (animator != null)
        animator.Play("attack_02");

    Invoke("SpawnarOndas", 0.4f);
    }

    void SpawnarOndas()
    {
    int quantidadeOndas = fase2Ativada ? 8 : 6;
    float velocidadeOnda = fase2Ativada ? 10f : 6f;

    for (int i = 0; i < quantidadeOndas; i++)
    {
        float angulo = i * (360f / quantidadeOndas);
        float rad = angulo * Mathf.Deg2Rad;
        Vector2 direcao = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));

        GameObject onda = Instantiate(prefabOnda, transform.position, Quaternion.identity);
        OndaChoque script = onda.GetComponent<OndaChoque>();
        script.Inicializar(direcao);
        script.velocidade = velocidadeOnda;
    }
    }

    void AtivarFase2()
    {
        fase2Ativada = true;

        if (animator != null)
            animator.Play("attack_02");

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

    [Header("Efeito de Morte")]
    public GameObject prefabEfeitoMorte;

    void Morrer()
    {
    // Spawna o efeito de partículas na posição do boss
    if (prefabEfeitoMorte != null)
        Instantiate(prefabEfeitoMorte, transform.position, Quaternion.identity);

    Destroy(gameObject);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, alcanceAtaque);
    }
}