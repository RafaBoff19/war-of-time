using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public GameObject painelGameOver;
    public GameObject painelVitoria;

    void Start()
    {
        painelGameOver.SetActive(false);
        painelVitoria.SetActive(false);
    }

    public void MostrarGameOver()
    {
        painelGameOver.SetActive(true);
        Time.timeScale = 0f;
    }

    public void MostrarVitoria()
    {
        painelVitoria.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Reiniciar()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void IrParaMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MenuInicio");
    }
}