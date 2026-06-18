using UnityEngine;

public class DanoInimigo : MonoBehaviour
{
    public int dano = 10;
    public float intervaloDano = 1f;

    private float temporizador = 0f;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void OnTriggerStay2D(Collider2D outro)
    {
        if (outro.CompareTag("Player"))
        {
            temporizador += Time.deltaTime;

            if (temporizador >= intervaloDano)
            {
                VidaJogador vida = outro.GetComponent<VidaJogador>();
                if (vida != null)
                    vida.ReceberDano(dano);

                if (animator != null)
                    animator.Play("attack");

                temporizador = 0f;
            }
        }
    }
}