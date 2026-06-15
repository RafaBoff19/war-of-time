using UnityEngine;
using TMPro;

public class UIJogo : MonoBehaviour
{
    public TextMeshProUGUI textoNivel;
    public TextMeshProUGUI textoTempo;

    private NivelJogador nivelJogador;
    private float tempoDecorrido = 0f;

    void Start()
    {
        GameObject jogador = GameObject.FindWithTag("Player");
        if (jogador != null)
            nivelJogador = jogador.GetComponent<NivelJogador>();
    }

    void Update()
    {
        tempoDecorrido += Time.deltaTime;

        // Atualiza o nível
        if (nivelJogador != null)
            textoNivel.text = "Nível " + nivelJogador.nivel;

        // Atualiza o cronômetro
        int minutos = Mathf.FloorToInt(tempoDecorrido / 60);
        int segundos = Mathf.FloorToInt(tempoDecorrido % 60);
        textoTempo.text = string.Format("{0:00}:{1:00}", minutos, segundos);
    }
}