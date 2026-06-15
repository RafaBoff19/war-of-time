using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public GameObject painelGameOver;

    void Start()
    {
        painelGameOver.SetActive(false);
    }

    public void MostrarGameOver()
    {
        painelGameOver.SetActive(true);
        Time.timeScale = 0f; // pausa o jogo
    }

    public void Reiniciar()
    {
        Time.timeScale = 1f; // volta o tempo normal
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}