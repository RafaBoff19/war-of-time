using UnityEngine;

public class VidaInimigo : MonoBehaviour
{
    public int vidaMaxima = 150;
    public int vidaAtual;

    void Start()
    {
        vidaAtual = vidaMaxima;
    }

    public void ReceberDano(int dano)
    {
        vidaAtual -= dano;
        Debug.Log(gameObject.name + " recebeu dano! Vida: " + vidaAtual);

        if (vidaAtual <= 0)
            Destroy(gameObject);
    }
}
