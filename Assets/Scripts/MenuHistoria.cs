using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuHistoria : MonoBehaviour
{
    public void Voltar()
    {
        Debug.Log("Botão Voltar clicado!");
        SceneManager.LoadScene("MenuInicio");
    }
}