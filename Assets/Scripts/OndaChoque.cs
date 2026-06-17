using UnityEngine;

public class OndaChoque : MonoBehaviour
{
    public float velocidade = 10f;
    public int dano = 40;
    private Vector2 direcao;

    public void Inicializar(Vector2 dir)
    {
        direcao = dir;
    }

    void FixedUpdate()
    {
        GetComponent<Rigidbody2D>().linearVelocity = direcao * velocidade;
    }

    void OnTriggerEnter2D(Collider2D outro)
    {
        if (outro.CompareTag("Player"))
        {
            VidaJogador vida = outro.GetComponent<VidaJogador>();
            if (vida != null)
                vida.ReceberDano(dano);

            Destroy(gameObject);
        }
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}