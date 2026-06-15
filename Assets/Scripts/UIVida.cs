using UnityEngine;
using UnityEngine.UI;

public class UIVida : MonoBehaviour
{
    public Slider barraVida;
    public VidaJogador vidaJogador;

    void Update()
    {
        if (vidaJogador == null || barraVida == null) return;

        barraVida.maxValue = vidaJogador.vidaMaxima;
        barraVida.value = vidaJogador.vidaAtual;
    }
}