using UnityEngine;

public class VidaJogador : MonoBehaviour
{
    public int vidaMaxima = 100;
    public int vidaAtual;

    private Animator animator;

    void Start()
    {
        vidaAtual = vidaMaxima;
        animator = GetComponent<Animator>();
    }

    public void ReceberDano(int dano)
    {
    if (morto) return;
    vidaAtual -= dano;

    SomJogador som = GetComponent<SomJogador>();
    if (som != null) som.TocarDano();

    if (vidaAtual <= 0)
        Morrer();
    }

    void Morrer()
    {
    if (morto) return;
    morto = true;

    SomJogador som = GetComponent<SomJogador>();
    if (som != null) som.TocarMorte();

    animator.SetTrigger("Morrer");
    GetComponent<PlayerMovement>().enabled = false;
    GetComponent<AtaqueJogador>().enabled = false;
    Invoke("AcionarGameOver", 1.2f);
    }
}