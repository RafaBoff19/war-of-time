using UnityEngine;

public class InimigoPerseguidor : MonoBehaviour
{
    public Transform alvo;
    public float velocidade = 2f;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Encontra o Portaluppi automaticamente na cena
        GameObject jogador = GameObject.FindWithTag("Player");
        if (jogador != null)
            alvo = jogador.transform;
    }

    void FixedUpdate()
    {
        if (alvo == null) return;

        Vector2 direcao = (alvo.position - transform.position).normalized;
        rb.linearVelocity = direcao * velocidade;
    }
}