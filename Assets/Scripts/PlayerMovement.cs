using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float velocidade = 5f;

    private Rigidbody2D rb;
    private Vector2 direcao;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        direcao = new Vector2(x, y).normalized;
    }

    void FixedUpdate()
    {
        rb.linearVelocity = direcao * velocidade;
    }
}