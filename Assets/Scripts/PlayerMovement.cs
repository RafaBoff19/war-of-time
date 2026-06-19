using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float velocidade = 5f;

    private Rigidbody2D rb;
    private Vector2 direcao;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
    float x = Input.GetAxisRaw("Horizontal");
    float y = Input.GetAxisRaw("Vertical");
    direcao = new Vector2(x, y).normalized;

    animator.SetFloat("Velocidade", direcao.magnitude);

    SomJogador som = GetComponent<SomJogador>();
    if (som != null) som.TocarPassos(direcao.magnitude > 0.1f);

    if (x > 0) spriteRenderer.flipX = false;
    else if (x < 0) spriteRenderer.flipX = true;
    }

    void FixedUpdate()
    {
        rb.linearVelocity = direcao * velocidade;
    }
}