using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuInicio : MonoBehaviour
{
    public void Jogar()
    {
        // Para a música do menu antes de entrar no jogo
        if (MusicaMenu.instancia != null)
            Destroy(MusicaMenu.instancia.gameObject);

        Time.timeScale = 1f; // garante que o tempo volta ao normal
        SceneManager.LoadScene("SampleScene");

    }

    public void AbrirHistoria()
    {
        SceneManager.LoadScene("Historia");
    }

    public void Sair()
    {
        Application.Quit();
    }
}