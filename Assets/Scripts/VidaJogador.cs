using UnityEngine;

public class VidaJogador : MonoBehaviour
{
    public int vidaMaxima = 100;
    public int vidaAtual;

    void Start()
    {
        vidaAtual = vidaMaxima;
    }

    public void ReceberDano(int dano)
    {
        vidaAtual -= dano;
        Debug.Log("Vida do Portaluppi: " + vidaAtual);

        if (vidaAtual <= 0)
        {
            Morrer();
        }
    }

    void Morrer()
    {
        Debug.Log("Portaluppi morreu!");
        gameObject.SetActive(false);
    }
}