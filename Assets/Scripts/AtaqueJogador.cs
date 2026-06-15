using UnityEngine;

public class AtaqueJogador : MonoBehaviour
{
    public float intervaloAtaque = 0.8f;
    public float alcanceEspada = 1.5f;
    public int dano = 20;

    private float temporizador = 0f;

    void Update()
    {
        temporizador += Time.deltaTime;

        if (temporizador >= intervaloAtaque)
        {
            GameObject inimigo = EncontrarInimigoMaisProximo();

            if (inimigo != null)
            {
                Atacar(inimigo);
                temporizador = 0f;
            }
        }
    }

    GameObject EncontrarInimigoMaisProximo()
    {
        GameObject[] inimigos = GameObject.FindGameObjectsWithTag("Inimigo");
        GameObject maisProximo = null;
        float menorDistancia = alcanceEspada;

        foreach (GameObject inimigo in inimigos)
        {
            float distancia = Vector2.Distance(transform.position, inimigo.transform.position);
            if (distancia < menorDistancia)
            {
                menorDistancia = distancia;
                maisProximo = inimigo;
            }
        }

        return maisProximo;
    }

    void Atacar(GameObject alvo)
    {
    // Tenta acertar inimigo comum
    VidaInimigo vidaInimigo = alvo.GetComponent<VidaInimigo>();
    if (vidaInimigo != null)
    {
        vidaInimigo.ReceberDano(dano);
        return;
    }

    // Tenta acertar o boss
    BossGisilva boss = alvo.GetComponent<BossGisilva>();
    if (boss != null)
        boss.ReceberDano(dano);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, alcanceEspada);
    }
}