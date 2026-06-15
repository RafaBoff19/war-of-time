using UnityEngine;

public class NivelJogador : MonoBehaviour
{
    public int nivel = 1;
    public int xpAtual = 0;
    public int xpParaProximoNivel = 50;

    private VidaJogador vidaJogador;
    private AtaqueJogador ataqueJogador;
    private PlayerMovement movimento;

    void Start()
    {
        vidaJogador = GetComponent<VidaJogador>();
        ataqueJogador = GetComponent<AtaqueJogador>();
        movimento = GetComponent<PlayerMovement>();
    }

    public void GanharXP(int quantidade)
    {
        xpAtual += quantidade;
        Debug.Log("XP: " + xpAtual + "/" + xpParaProximoNivel);

        if (xpAtual >= xpParaProximoNivel)
            SubirDeNivel();
    }

    void SubirDeNivel()
    {
        nivel++;
        xpAtual = 0;
        xpParaProximoNivel = Mathf.RoundToInt(xpParaProximoNivel * 1.5f);

        // Aumenta os atributos automaticamente
        if (vidaJogador != null)
        {
            vidaJogador.vidaMaxima += 20;
            vidaJogador.vidaAtual = vidaJogador.vidaMaxima; // cura ao subir de nível
        }

        if (ataqueJogador != null)
        {
            ataqueJogador.dano += 5;
            ataqueJogador.alcanceEspada += 0.2f;
        }

        if (movimento != null)
            movimento.velocidade += 0.3f;

        Debug.Log("NÍVEL " + nivel + "! Atributos aumentados!");
    }
}