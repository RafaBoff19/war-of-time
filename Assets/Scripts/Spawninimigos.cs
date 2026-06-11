using UnityEngine;

public class SpawnInimigos : MonoBehaviour
{
    public GameObject prefabInimigo;
    public float intervaloSpawn = 2f;
    public float raioSpawn = 8f;
    public int maxInimigos = 20;

    private float temporizador = 0f;
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

        temporizador += Time.deltaTime;

        if (temporizador >= intervaloSpawn)
        {
            if (GameObject.FindGameObjectsWithTag("Inimigo").Length < maxInimigos)
            {
                SpawnarInimigo();
            }
            temporizador = 0f;
        }
    }

    void SpawnarInimigo()
    {
        // Spawna sempre ao redor do Portaluppi
        Vector2 posicaoAleatoria = Random.insideUnitCircle.normalized * raioSpawn;
        Vector3 posicaoSpawn = jogador.position + new Vector3(posicaoAleatoria.x, posicaoAleatoria.y, 0);

        Instantiate(prefabInimigo, posicaoSpawn, Quaternion.identity);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        if (jogador != null)
            Gizmos.DrawWireSphere(jogador.position, raioSpawn);
        else
            Gizmos.DrawWireSphere(transform.position, raioSpawn);
    }
}