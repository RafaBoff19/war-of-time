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

    void Start()
    {
        vidaAtual = vidaMaxima;
        rb = GetComponent<Rigidbody2D>();

        GameObject obj = GameObject.FindWithTag("Player");
        if (obj != null)
            jogador = obj.transform;
    }

    void Update()
    {
        if (jogador == null) return;

        // Persegue o jogador
        Vector2 direcao = (jogador.position - transform.position).normalized;
        rb.linearVelocity = direcao * velocidade;

        // Onda de choque
        temporizadorOnda += Time.deltaTime;
        if (temporizadorOnda >= intervaloOnda)
        {
            LancarOndas();
            temporizadorOnda = 0f;
        }

        // Ativa fase 2 em 50% de vida
        if (!fase2Ativada && vidaAtual <= vidaMaxima * 0.5f)
        {
            AtivarFase2();
        }
    }

    void LancarOndas()
    {
        if (prefabOnda == null) return;

        // Lança 4 ondas em cruz
        Vector2[] direcoes = {
            Vector2.up, Vector2.down, Vector2.left, Vector2.right
        };

        foreach (Vector2 dir in direcoes)
        {
            GameObject onda = Instantiate(prefabOnda, transform.position, Quaternion.identity);
            onda.GetComponent<OndaChoque>().Inicializar(dir);
        }
    }

    void AtivarFase2()
    {
        fase2Ativada = true;
        Debug.Log("Gisilva - FASE 2! Invocando Peixe-Sombra!");

        for (int i = 0; i < quantidadeInvocacao; i++)
        {
            Vector2 pos = (Vector2)transform.position + Random.insideUnitCircle * 3f;
            Instantiate(prefabPeixeSombra, pos, Quaternion.identity);
        }
    }

    public void ReceberDano(int dano)
    {
        vidaAtual -= dano;
        Debug.Log("Gisilva HP: " + vidaAtual);

        if (vidaAtual <= 0)
            Morrer();
    }

    void Morrer()
    {
        Debug.Log("Gisilva derrotado!");
        // Aqui futuramente abriremos o portal para a próxima camada
        Destroy(gameObject);
    }

    void OnTriggerStay2D(Collider2D outro)
    {
        if (outro.CompareTag("Player"))
        {
            VidaJogador vida = outro.GetComponent<VidaJogador>();
            if (vida != null)
                vida.ReceberDano(20);
        }
    }
}