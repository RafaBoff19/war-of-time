using UnityEngine;

public class SpawnInimigos : MonoBehaviour
{
    [Header("Inimigos Comuns")]
    public GameObject prefabInimigo;
    public float intervaloSpawn = 2f;
    public float raioSpawn = 8f;
    public int maxInimigos = 20;

    [Header("Boss")]
    public GameObject prefabBoss;
    public float tempoBoss = 60f; // boss aparece após 60 segundos
    private bool bossSpawnado = false;

    private float temporizador = 0f;
    private float temporizadorBoss = 0f;
    private Transform jogador;

    void Start()
    {
        GameObject obj = GameObject.FindWithTag("Player");
        if (obj != null)
            jogador = obj.transform;
    }

    void Update()
    {
        if (jogador == null) return;

        // Spawn de inimigos comuns — para quando o boss aparecer
        if (!bossSpawnado)
        {
            temporizador += Time.deltaTime;
            if (temporizador >= intervaloSpawn)
            {
                if (GameObject.FindGameObjectsWithTag("Inimigo").Length < maxInimigos)
                    SpawnarInimigo();

                temporizador = 0f;
            }
        }

        // Spawn do boss após tempoBoss segundos
        temporizadorBoss += Time.deltaTime;
        if (!bossSpawnado && temporizadorBoss >= tempoBoss)
        {
            SpawnarBoss();
        }
    }

    void SpawnarInimigo()
    {
        Vector2 posAleatoria = Random.insideUnitCircle.normalized * raioSpawn;
        Vector3 posSpawn = jogador.position + new Vector3(posAleatoria.x, posAleatoria.y, 0);
        Instantiate(prefabInimigo, posSpawn, Quaternion.identity);
    }

    void SpawnarBoss()
    {
    bossSpawnado = true;
    Vector3 posSpawn = jogador.position + new Vector3(10f, 0f, 0f);
    Instantiate(prefabBoss, posSpawn, Quaternion.identity);

    // Troca para a trilha do boss
    if (GerenciadorMusica.instancia != null)
        GerenciadorMusica.instancia.TocarBoss();

    Debug.Log("BOSS APARECEU!");
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        if (jogador != null)
            Gizmos.DrawWireSphere(jogador.position, raioSpawn);
    }
}