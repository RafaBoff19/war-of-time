using UnityEngine;

public class PausaJogo : MonoBehaviour
{
    public GameObject painelPausa;

    private bool pausado = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pausado)
                Retomar();
            else
                Pausar();
        }
    }

    void Pausar()
    {
        pausado = true;
        Time.timeScale = 0f;

        if (GerenciadorMusica.instancia != null)
            GerenciadorMusica.instancia.PararMusica(); // pausa sem resetar

        if (painelPausa != null)
            painelPausa.SetActive(true);
    }

    public void Retomar()
    {
        pausado = false;
        Time.timeScale = 1f;

        if (GerenciadorMusica.instancia != null)
            GerenciadorMusica.instancia.RetomarMusica(); // continua de onde parou

        if (painelPausa != null)
            painelPausa.SetActive(false);
    }
}